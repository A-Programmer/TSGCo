using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Api.ViewModels.TicketCategoryViewModels
{
    public class add_ticket_category_vm
    {
        [Required]
        public string title { get; set; }
    }
}
