using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Project.Cdn.ViewModels.FileManagerViewModels;
using TinifyAPI;
using Exception = System.Exception;

namespace Project.Cdn.Helpers
{

    public interface IFileManager
    {
        Task<FileManagerResultMessage> Upload(UploadFileVm model);
        FileManagerResultMessage DeleteSingleFile(string relativeFilePath);
        Task<DirectoryVm> GetDirectory(string path, bool addBaseAddress = false);
        FileManagerResultMessage CreateDirectory(string path);
        FileManagerResultMessage RemoveDirectory(string relativeDirectoryPath);
        FileManagerResultMessage Rename(string path, string newPath);

    }

    public class FileManagerHelper : IFileManager
    {

        private const string BaseAddress = "Resources";

        private readonly ILogger<FileManagerHelper> _log;

        public FileManagerHelper(
        ILogger<FileManagerHelper> log)
        {
            Tinify.Key = "6o4SkhFjaLdH3vYl1sXcvkE6IUvHrARI";
            _log = log;
        }


        public async Task<FileManagerResultMessage> Upload(UploadFileVm model)
        {
            var rm = new FileManagerResultMessage()
            {
                IsSuccess = false,
                Message = ""
            };
            try
            {
                var physicallyPath = Path.Combine(Directory.GetCurrentDirectory(), BaseAddress);
                var relativePath = "/" + BaseAddress + "/";

                if (string.IsNullOrEmpty(model.PathToUpload))
                {
                    physicallyPath = Path.Combine(Directory.GetCurrentDirectory(), BaseAddress + "/Uploads");
                }
                else
                {

                    if (!model.PathToUpload.EndsWith('/'))
                    {
                        //model.PathToUpload = model.PathToUpload.Remove(model.PathToUpload.Length - 1);
                        model.PathToUpload += "/";
                    }

                    if (model.PathToUpload.StartsWith('/'))
                    {
                        model.PathToUpload = model.PathToUpload.Remove(0, 1);
                    }

                    physicallyPath = Path.Combine(physicallyPath, model.PathToUpload);
                    relativePath = Path.Combine(relativePath, model.PathToUpload);
                }


                Directory.CreateDirectory(physicallyPath);


                //if (model.Archive)
                //{
                //    var archiveFolderName = Archive(model.ArchiveBy);
                //    model.PathToUpload = Path.Combine(model.PathToUpload, archiveFolderName);
                //    Directory.CreateDirectory(model.PathToUpload);
                //}


                var cleanFileName = RenameFile(model.File, model.RenameFile);

                //Set Save Original Size Path
                relativePath = Path.Combine(relativePath, cleanFileName);


                physicallyPath = Path.Combine(physicallyPath, cleanFileName);

                using (var stream = new FileStream(physicallyPath, FileMode.Create, FileAccess.ReadWrite))
                {
                    await model.File.CopyToAsync(stream);
                }

                var fileExtension = GetFileExtension(model.File);

                if (model.Compress && (fileExtension == "jpg" || fileExtension == "jpeg" || fileExtension == "png"))
                {
                    try
                    {
                        await Compress(physicallyPath);
                    }
                    catch (TinifyAPI.Exception ex)
                    {
                        var exMessage = ex.Message;
                        if (ex.InnerException != null)
                        {
                            exMessage = ex.InnerException.Message;
                        }

                        _log.LogError(ex, "CompressImage");
                    }
                }

                rm.IsSuccess = true;
                rm.Message = relativePath;
            }
            catch (System.Exception ex)
            {
                var exMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    exMessage = ex.InnerException.Message;
                }

                _log.LogError(ex, "Upload");

                rm.IsSuccess = false;
                rm.Message = exMessage;
            }
            return rm;
        }

        //Delete Single File
        public FileManagerResultMessage DeleteSingleFile(string relativeFilePath)
        {
            var rm = new FileManagerResultMessage()
            {
                IsSuccess = false,
                Message = "",
            };
            try
            {



                var physicallyPath = Path.Combine(Directory.GetCurrentDirectory(), BaseAddress);

                if (string.IsNullOrEmpty(relativeFilePath))
                {
                    return new FileManagerResultMessage
                    {
                        Message = "پوشه مورد نظر یافت نشد",
                        IsSuccess = false
                    };
                }
                else
                {

                    if (!relativeFilePath.EndsWith('/'))
                    {
                        //model.PathToUpload = model.PathToUpload.Remove(model.PathToUpload.Length - 1);
                        relativeFilePath.Remove(relativeFilePath.Length - 1);
                        //relativeFilePath += "/";
                    }

                    if (relativeFilePath.StartsWith('/'))
                    {
                        relativeFilePath = relativeFilePath.Remove(0, 1);
                    }

                    physicallyPath = Path.Combine(physicallyPath, relativeFilePath);
                }




                //var root = Path.Combine(Directory.GetCurrentDirectory(), "");
                //var fileAddress = Path.Combine(root, relativeFilePath);

                if (File.Exists(physicallyPath))
                {
                    File.Delete(physicallyPath);
                }
                rm.IsSuccess = true;
                rm.Message = "عملیات با موفقیت انجام شد";
            }
            catch (System.Exception ex)
            {
                var exMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    exMessage = ex.InnerException.Message;
                }

                _log.LogError(ex, "DeleteSignleFile");

                rm.IsSuccess = false;
                rm.Message = exMessage;
            }
            return rm;
        }

        public FileManagerResultMessage RemoveDirectory(string relativeDirectoryPath)
        {
            var result = new FileManagerResultMessage
            {
                IsSuccess = false,
                Message = ""
            };


            var physicallyPath = Path.Combine(Directory.GetCurrentDirectory(), BaseAddress);

            if (string.IsNullOrEmpty(relativeDirectoryPath))
            {
                return new FileManagerResultMessage
                {
                    Message = "پوشه مورد نظر یافت نشد",
                    IsSuccess = false
                };
            }
            else
            {

                if (!relativeDirectoryPath.EndsWith('/'))
                {
                    //model.PathToUpload = model.PathToUpload.Remove(model.PathToUpload.Length - 1);
                    relativeDirectoryPath += "/";
                }

                if (relativeDirectoryPath.StartsWith('/'))
                {
                    relativeDirectoryPath = relativeDirectoryPath.Remove(0, 1);
                }

                physicallyPath = Path.Combine(physicallyPath, relativeDirectoryPath);
            }


            if (!Directory.Exists(physicallyPath))
            {
                result.Message = "پوشه مورد نظر یافت نشد";
                return result;
            }
            var directoryInfo = new DirectoryInfo(physicallyPath);
            var directories = directoryInfo.GetDirectories();
            var files = directoryInfo.GetFiles();
            if (files.Any())
            {
                foreach (var file in files)
                {
                    DeleteSingleFile(Path.Combine(relativeDirectoryPath, file.Name));
                }
            }
            if (directories.Any())
            {
                foreach (var directory in directories)
                {
                    RemoveDirectory(directory.FullName);
                    Directory.Delete(directory.FullName);
                }
            }

            Directory.Delete(directoryInfo.FullName);


            result.IsSuccess = true;
            result.Message = "پوشه و فایلهای آن حذف شدند.";
            return result;
        }

        public FileManagerResultMessage CreateDirectory(string path)
        {
            try
            {
                path = Path.Combine(BaseAddress, path);
                var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), path);
                Directory.CreateDirectory(path);
                return new FileManagerResultMessage
                {
                    IsSuccess = true,
                    Message = path
                };
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, "CreateDirectory");
                return new FileManagerResultMessage
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public FileManagerResultMessage Rename(string path, string newPath)
        {
            var result = new FileManagerResultMessage
            {
                IsSuccess = false,
                Message = ""
            };

            var physicallyPath = Path.Combine(Directory.GetCurrentDirectory(), BaseAddress);
            var newPhysicallyPath = Path.Combine(Directory.GetCurrentDirectory(), BaseAddress);

            if (string.IsNullOrEmpty(path))
            {
                return new FileManagerResultMessage
                {
                    Message = "پوشه مورد نظر یافت نشد",
                    IsSuccess = false
                };
            }
            else
            {

                if (path.EndsWith('/'))
                {
                    //model.PathToUpload = model.PathToUpload.Remove(model.PathToUpload.Length - 1);
                    //path += "/";
                    path = path.Remove(path.Length - 1);
                }

                if (path.StartsWith('/'))
                {
                    path = path.Remove(0, 1);
                }

                physicallyPath = Path.Combine(physicallyPath, path);





                if (newPath.EndsWith('/'))
                {
                    //model.PathToUpload = model.PathToUpload.Remove(model.PathToUpload.Length - 1);
                    //newPath += "/";
                    newPath = newPath.Remove(newPath.Length - 1);
                }

                if (newPath.StartsWith('/'))
                {
                    newPath = newPath.Remove(0, 1);
                }

                newPhysicallyPath = Path.Combine(newPhysicallyPath, newPath);
            }


            if (Directory.Exists(physicallyPath))
            {
                Directory.Move(physicallyPath, newPhysicallyPath);
                return new FileManagerResultMessage
                {
                    IsSuccess = true,
                    Message = "پوشه مورد نظر با موفقیت تغییر نام داده شد"
                };
            }
            else
            {
                result.Message = "پوشه مورد نظر یافت نشد";
            }

            if (File.Exists(physicallyPath))
            {
                File.Move(physicallyPath, newPhysicallyPath);
                return new FileManagerResultMessage
                {
                    IsSuccess = true,
                    Message = "فایل مورد نظر با موفقیت تغییر نام داده شد"
                };
            }
            else
            {
                result.Message += " -- فایل مورد نظر یافت نشد";
            }

            return result;
        }

        public async Task<DirectoryVm> GetDirectory(string path, bool addBaseAddress = false)
        {
            var result = new DirectoryVm();

            if (addBaseAddress)
                path = BaseAddress + path;

            if (path.StartsWith("/"))
                path = path.Remove(0, 1);
            if (!path.EndsWith("/"))
                path += "/";

            var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), path);

            var relativePath = path;

            //if (!Directory.Exists(physicalPath))
            //    return null;

            var directoryInfo = new DirectoryInfo(physicalPath);

            result.CreatedDate = directoryInfo.CreationTime;
            result.ModifiedDate = directoryInfo.LastWriteTime;
            result.Name = directoryInfo.Name;
            result.FullName = directoryInfo.FullName;
            result.RelativePath = relativePath;


            var directories = directoryInfo.GetDirectories();

            var files = directoryInfo.GetFiles();

            long directorySize = 0;

            foreach (var file in files)
            {
                var fileVm = new FileVm
                {
                    CreatedDate = file.CreationTime,
                    ModifiedDate = file.LastWriteTime,
                    Name = file.Name,
                    FullName = file.FullName,
                    RelativePath = Path.Combine(path, file.Name),
                    Size = file.Length

                };
                directorySize += file.Length;

                if (fileVm != null)
                    result.Files.Add(fileVm);
            }

            result.Size = directorySize;

            foreach (var dir in directories)
            {
                var subRelativePath = Path.Combine(relativePath, dir.Name);
                var subDirectory = await GetDirectory(subRelativePath);
                if (subDirectory != null)
                    result.Directories.Add(subDirectory);
            }

            return result;

        }

        public string Archive(ArchiveTypes archiveBy, string customArchivePath = "/")
        {
            var d = DateTime.Now;
            var pc = new PersianCalendar();
            var archiveFolderName = pc.GetMonth(d) + "/";
            switch (archiveBy)
            {
                case ArchiveTypes.ByYear:
                    {
                        archiveFolderName = pc.GetYear(d) + "/";
                        break;
                    }
                case ArchiveTypes.ByMonth:
                    {
                        archiveFolderName = pc.GetYear(d) + "-" + pc.GetMonth(d) + "/";
                        break;
                    }
                case ArchiveTypes.ByDay:
                    {
                        archiveFolderName = pc.GetYear(d) + "-" + pc.GetMonth(d) + "-" + pc.GetDayOfMonth(d) + "/";
                        break;
                    }
                default:
                    {
                        archiveFolderName = customArchivePath;
                        break;
                    }
            }

            return archiveFolderName;
        }

        public string RenameFile(IFormFile file, RenameTypes renameType)
        {
            //Save file on server
            //var file = uploadFile;
            var fileName = file.FileName;

            string extension = Path.GetExtension(file.FileName);

            string cleanFileName = fileName.Replace(extension, "") + "-" + Guid.NewGuid().ToString().Substring(0, 8) + extension;

            //FileName Format ::
            switch (renameType)
            {
                case RenameTypes.AddRandomNameToFileName:
                    {
                        cleanFileName = fileName.Replace(extension, "") + "-" + Guid.NewGuid().ToString().Substring(0, 8) + extension;
                        break;
                    }
                case RenameTypes.KeepOriginalName:
                    {
                        cleanFileName = fileName;
                        break;
                    }
                case RenameTypes.RenameToRandomName:
                    {
                        cleanFileName = Guid.NewGuid().ToString().Substring(0, 8) + extension;
                        break;
                    }
                default:
                    {
                        cleanFileName = fileName;
                        break;
                    }
            }
            return cleanFileName;
        }

        public string GetFileExtension(IFormFile file)
        {
            var fileName = file.FileName;

            string extension = Path.GetExtension(file.FileName).Replace(".", "");

            return extension;
        }

        public async Task Compress(string fileAddress)
        {
            //Set key in constructor of this class
            if (File.Exists(fileAddress))
            {
                var operation = Tinify.FromFile(fileAddress);
                await operation.ToFile(fileAddress);
            }
        }

        private string GetRelativePath(string physicalPath)
        {

            var rootDirectory = Directory.GetCurrentDirectory();
            physicalPath = physicalPath.Replace(@"\", "/");
            var relativePath = physicalPath.Replace(rootDirectory, "");
            return relativePath;
        }

    }
}
