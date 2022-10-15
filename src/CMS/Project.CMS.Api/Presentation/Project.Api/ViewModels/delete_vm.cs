using System;
namespace Project.Api.ViewModels
{
    public class DeleteVm
    {
        public DeleteVm(Guid id)
        {
            this.id = id;
        }

        public Guid id { get; set; }
    }
}
