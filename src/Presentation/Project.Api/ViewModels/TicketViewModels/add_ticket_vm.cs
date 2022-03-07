using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Api.ViewModels.TicketViewModels
{
    public class add_ticket_vm
    {
        [Required]
        public string title { get; set; }

        [Required]
        public string content { get; set; }
        public int phone_number { get; set; }
        public Guid ticket_category_id { get; set; }

        public string[] ticket_attachments { get; set; }
    }
}
