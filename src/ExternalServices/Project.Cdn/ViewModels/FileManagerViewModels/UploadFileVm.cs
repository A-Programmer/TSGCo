using System;
using Microsoft.AspNetCore.Http;

namespace Project.Cdn.ViewModels.FileManagerViewModels
{
    public class UploadFileVm
    {
        public UploadFileVm()
        {
        }

        public IFormFile File { get; set; }
        public string PathToUpload { get; set; } = "/";
        //public bool Archive { get; set; } = false;
        //public ArchiveTypes ArchiveBy { get; set; } = ArchiveTypes.ByMonth;
        public bool Compress { get; set; } = false;
        public RenameTypes RenameFile { get; set; } = RenameTypes.AddRandomNameToFileName;
    }

    public enum RenameTypes
    {
        KeepOriginalName,
        AddRandomNameToFileName,
        RenameToRandomName
    }

    public enum ArchiveTypes
    {
        ByYear,
        ByMonth,
        ByDay,
        Custom
    }
}
