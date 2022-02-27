using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Api.ViewModels.TicketCategoryViewModels
{
    public class edit_ticket_category_vm
    {
        [Required]
        public Guid id { get; set; }

        [Required]
        public string title { get; set; }
        
    }
}
