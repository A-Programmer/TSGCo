using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Api.ViewModels.ReplyViewModels
{
    public class ticket_reply_vm
    {
        public ticket_reply_vm(Guid id, string content, string created_date, Guid ticket_id, Guid replied_by_id)
        {
            this.id = id;
            this.content = content;
            this.created_date = created_date;
            this.ticket_id = ticket_id;
            this.replied_by_id = replied_by_id;
        }

        public Guid id { get; private set; }
        public string content { get; private set; }
        public string created_date { get; private set; }

        public Guid ticket_id { get; private set; }
        public Guid replied_by_id { get; private set; }

    }
}
