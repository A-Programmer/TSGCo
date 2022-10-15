using System;
namespace Project.Api.ViewModels.FileManagerViewModels
{
    public class file_vm
    {
        public string name { get; set; }
        public string relative_path { get; set; }
        public string full_name { get; set; }
        public long size { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }
    }
}
