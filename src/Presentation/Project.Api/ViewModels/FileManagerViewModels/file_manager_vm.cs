using System;
using System.Collections.Generic;

namespace Project.Api.ViewModels.FileManagerViewModels
{
    public class file_manager_vm
    {
        public List<directory_vm> directories { get; set; }
        public List<file_vm> files { get; set; }
    }
}
