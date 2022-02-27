using Project.Api.ViewModels.TicketViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project.Api.ViewModels.TicketCategoryViewModels
{
    public class ticket_category_vm
    {
        public ticket_category_vm(Guid id, string title, List<ticket_vm> tickets)
        {
            this.id = id;
            this.title = title;
            this.tickets = tickets.Select(x => new ticket_vm(x.id, x.ticket_category_name, x.content,
                x.phone_number, x.created_date, null, x.ticket_category_id, null, x.creator_id,
                x.creator_name, null, x.ticket_attachments)).ToList();
        }
        public Guid id { get; private set; }
        public string title { get; private set; }

        public List<ticket_vm> tickets { get; private set; }



    }
}
