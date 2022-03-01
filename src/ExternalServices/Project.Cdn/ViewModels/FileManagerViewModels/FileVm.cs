using System;
namespace Project.Cdn.ViewModels.FileManagerViewModels
{
    public class FileVm
    {
        public string Name { get; set; }
        public string RelativePath { get; set; }
        public string FullName { get; set; }
        public long Size { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
