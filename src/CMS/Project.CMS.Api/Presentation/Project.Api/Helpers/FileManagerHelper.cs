using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Project.Api.ViewModels.FileManagerViewModels;
using TinifyAPI;
using Exception = System.Exception;

namespace Project.Api.Helpers
{
    public interface IFileManager
    {
        Task<file_manager_result_message> Upload(upload_file_vm model);
        file_manager_result_message DeleteSingleFile(string relativeFilePath);
        Task<directory_vm> GetDirectory(string path, bool addBaseAddress = false);
        file_manager_result_message CreateDirectory(string path);
        file_manager_result_message RemoveDirectory(string relativeDirectoryPath);
        file_manager_result_message Rename(string path, string newPath);

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


        public async Task<file_manager_result_message> Upload(upload_file_vm model)
        {
            var rm = new file_manager_result_message()
            {
                is_success = false,
                message = ""
            };
            try
            {
                var physicallyPath = Path.Combine(Directory.GetCurrentDirectory(), BaseAddress);
                var relativePath = "/" + BaseAddress + "/";

                if (string.IsNullOrEmpty(model.path_to_upload))
                {
                    physicallyPath = Path.Combine(Directory.GetCurrentDirectory(), BaseAddress + "/Uploads");
                }
                else
                {

                    if (!model.path_to_upload.EndsWith('/'))
                    {
                        //model.PathToUpload = model.PathToUpload.Remove(model.PathToUpload.Length - 1);
                        model.path_to_upload += "/";
                    }

                    if (model.path_to_upload.StartsWith('/'))
                    {
                        model.path_to_upload = model.path_to_upload.Remove(0, 1);
                    }

                    physicallyPath = Path.Combine(physicallyPath, model.path_to_upload);
                    relativePath = Path.Combine(relativePath, model.path_to_upload);
                }


                Directory.CreateDirectory(physicallyPath);


                //if (model.Archive)
                //{
                //    var archiveFolderName = Archive(model.ArchiveBy);
                //    model.PathToUpload = Path.Combine(model.PathToUpload, archiveFolderName);
                //    Directory.CreateDirectory(model.PathToUpload);
                //}


                var cleanFileName = RenameFile(model.file, model.rename_file);

                //Set Save Original Size Path
                relativePath = Path.Combine(relativePath, cleanFileName);


                physicallyPath = Path.Combine(physicallyPath, cleanFileName);

                using (var stream = new FileStream(physicallyPath, FileMode.Create, FileAccess.ReadWrite))
                {
                    await model.file.CopyToAsync(stream);
                }

                var fileExtension = GetFileExtension(model.file);

                if (model.compress && (fileExtension == "jpg" || fileExtension == "jpeg" || fileExtension == "png"))
                {
                    try
                    {
                        await Compress(physicallyPath);
                    }
                    catch (TinifyException ex)
                    {
                        var exMessage = ex.Message;
                        if (ex.InnerException != null)
                        {
                            exMessage = ex.InnerException.Message;
                        }

                        _log.LogError(ex, "CompressImage");
                    }
                }

                rm.is_success = true;
                rm.message = relativePath;
            }
            catch (System.Exception ex)
            {
                var exMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    exMessage = ex.InnerException.Message;
                }

                _log.LogError(ex, "Upload");

                rm.is_success = false;
                rm.message = exMessage;
            }
            return rm;
        }

        //Delete Single File
        public file_manager_result_message DeleteSingleFile(string relativeFilePath)
        {
            var rm = new file_manager_result_message()
            {
                is_success = false,
                message = "",
            };
            try
            {



                var physicallyPath = Path.Combine(Directory.GetCurrentDirectory(), BaseAddress);

                if (string.IsNullOrEmpty(relativeFilePath))
                {
                    return new file_manager_result_message
                    {
                        message = "پوشه مورد نظر یافت نشد",
                        is_success = false
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
                rm.is_success = true;
                rm.message = "عملیات با موفقیت انجام شد";
            }
            catch (System.Exception ex)
            {
                var exMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    exMessage = ex.InnerException.Message;
                }

                _log.LogError(ex, "DeleteSignleFile");

                rm.is_success = false;
                rm.message = exMessage;
            }
            return rm;
        }

        public file_manager_result_message RemoveDirectory(string relativeDirectoryPath)
        {
            var result = new file_manager_result_message
            {
                is_success = false,
                message = ""
            };


            var physicallyPath = Path.Combine(Directory.GetCurrentDirectory(), BaseAddress);

            if (string.IsNullOrEmpty(relativeDirectoryPath))
            {
                return new file_manager_result_message
                {
                    message = "پوشه مورد نظر یافت نشد",
                    is_success = false
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
                result.message = "پوشه مورد نظر یافت نشد";
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


            result.is_success = true;
            result.message = "پوشه و فایلهای آن حذف شدند.";

            return result;
        }

        public file_manager_result_message CreateDirectory(string path)
        {
            try
            {
                path = Path.Combine(BaseAddress, path);
                var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), path);
                Directory.CreateDirectory(path);
                return new file_manager_result_message
                {
                    is_success = true,
                    message = path
                };
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message, "CreateDirectory");
                return new file_manager_result_message
                {
                    is_success = false,
                    message = ex.Message,
                };
            }
        }

        public file_manager_result_message Rename(string path, string newPath)
        {
            var result = new file_manager_result_message
            {
                is_success = false,
                message = ""
            };

            var physicallyPath = Path.Combine(Directory.GetCurrentDirectory(), BaseAddress);
            var newPhysicallyPath = Path.Combine(Directory.GetCurrentDirectory(), BaseAddress);

            if (string.IsNullOrEmpty(path))
            {
                return new file_manager_result_message
                {
                    message = "پوشه مورد نظر یافت نشد",
                    is_success = false
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
                return new file_manager_result_message
                {
                    is_success = true,
                    message = "پوشه مورد نظر با موفقیت تغییر نام داده شد"
                };
            }
            else
            {
                result.message = "پوشه مورد نظر یافت نشد";
            }

            if (File.Exists(physicallyPath))
            {
                File.Move(physicallyPath, newPhysicallyPath);
                return new file_manager_result_message
                {
                    is_success = true,
                    message = "فایل مورد نظر با موفقیت تغییر نام داده شد"
                };
            }
            else
            {
                result.message += " -- فایل مورد نظر یافت نشد";
            }

            return result;
        }

        public async Task<directory_vm> GetDirectory(string path, bool addBaseAddress = false)
        {
            var result = new directory_vm();

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

            result.created_date = directoryInfo.CreationTime;
            result.modified_date = directoryInfo.LastWriteTime;
            result.name = directoryInfo.Name;
            result.full_name = directoryInfo.FullName;
            result.relative_path = relativePath;


            var directories = directoryInfo.GetDirectories();

            var files = directoryInfo.GetFiles();

            long directorySize = 0;

            foreach (var file in files)
            {
                var fileVm = new file_vm
                {
                    created_date = file.CreationTime,
                    modified_date = file.LastWriteTime,
                    name = file.Name,
                    full_name = file.FullName,
                    relative_path = Path.Combine(path, file.Name),
                    size = file.Length

                };
                directorySize += file.Length;

                if (fileVm != null)
                    result.files.Add(fileVm);
            }

            result.size = directorySize;

            foreach (var dir in directories)
            {
                var subRelativePath = Path.Combine(relativePath, dir.Name);
                var subDirectory = await GetDirectory(subRelativePath);
                if (subDirectory != null)
                    result.directories.Add(subDirectory);
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
