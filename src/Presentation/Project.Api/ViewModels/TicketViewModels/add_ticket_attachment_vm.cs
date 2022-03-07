using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Api.ViewModels.TicketViewModels
{
    public class add_ticket_attachment_vm
    {
        [Required]
        public string attachment_url { get; set; }

        public Guid ticket_id { get; set; }
    }
}
