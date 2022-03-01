using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Api.ViewModels.ReplyViewModels
{
    public class add_ticket_reply_vm
    {
        [Required]
        public string content { get; set; }

        public Guid ticket_id { get; set; }
        public Guid replied_by_id { get; set; }

    }
}
