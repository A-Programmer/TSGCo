using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Api.ViewModels.TicketViewModels
{
    public class ticket_attachment_vm
    {
        public ticket_attachment_vm(Guid id, string attachment_url, Guid ticket_id)
        {
            this.id = id;
            this.attachment_url = attachment_url;
            this.ticket_id = ticket_id;
        }

        public Guid id { get; private set; }
        public string attachment_url { get; set; }

        public Guid ticket_id { get; set; }
    }
}
