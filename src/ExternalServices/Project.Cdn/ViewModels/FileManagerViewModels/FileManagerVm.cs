using System;
using System.Collections.Generic;

namespace Project.Cdn.ViewModels.FileManagerViewModels
{
    public class FileManagerVm
    {
        public List<DirectoryVm> Directories { get; set; }
        public List<FileVm> Files { get; set; }
    }
}
