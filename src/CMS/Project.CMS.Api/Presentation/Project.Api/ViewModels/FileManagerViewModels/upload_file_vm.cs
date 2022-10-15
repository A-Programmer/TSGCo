using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Project.Api.ViewModels.FileManagerViewModels
{
    public class upload_file_vm
    {
        public upload_file_vm()
        {
        }

        [Required]
        public IFormFile file { get; set; }
        public string path_to_upload { get; set; } = "/";
        //public bool Archive { get; set; } = false;
        //public ArchiveTypes ArchiveBy { get; set; } = ArchiveTypes.ByMonth;
        public bool compress { get; set; } = false;
        [Required]
        public RenameTypes rename_file { get; set; } = RenameTypes.AddRandomNameToFileName;
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
