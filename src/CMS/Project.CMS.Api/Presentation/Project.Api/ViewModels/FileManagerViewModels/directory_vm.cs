using System;
using System.Collections.Generic;

namespace Project.Api.ViewModels.FileManagerViewModels
{
    public class directory_vm
    {
        public directory_vm()
        {
            directories = new List<directory_vm>();
            files = new List<file_vm>();
        }

        public string name { get; set; }
        public string relative_path { get; set; }
        public string full_name { get; set; }
        public long size { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }

        public List<directory_vm> directories { get; set; }
        public List<file_vm> files { get; set; }
    }
}
