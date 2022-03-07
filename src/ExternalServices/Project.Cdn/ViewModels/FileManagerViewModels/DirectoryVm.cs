using System;
using System.Collections.Generic;

namespace Project.Cdn.ViewModels.FileManagerViewModels
{
    public class DirectoryVm
    {
        public DirectoryVm()
        {
            Directories = new List<DirectoryVm>();
            Files = new List<FileVm>();
        }

        public string Name { get; set; }
        public string RelativePath { get; set; }
        public string FullName { get; set; }
        public long Size { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public List<DirectoryVm> Directories { get; set; }
        public List<FileVm> Files { get; set; }
    }
}
