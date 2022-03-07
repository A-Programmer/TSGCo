using Project.Api.ViewModels.ReplyViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Api.ViewModels.TicketViewModels
{
    public class ticket_vm
    {
        public ticket_vm(Guid id, string title, string content, int phone_number, string created_date
            , string status, Guid ticket_category_id, string ticket_category_name, Guid creator_id
            , string creator_name, List<ticket_reply_vm> replies, string[] ticket_attachments)
        {
            this.id = id;
            this.title = title;
            this.content = content;
            this.phone_number = phone_number;
            this.created_date = created_date;
            this.status = status;
            this.ticket_category_id = ticket_category_id;
            this.ticket_category_name = ticket_category_name;
            this.creator_id = creator_id;
            this.creator_name = creator_name;

            this.replies = replies.ToList();
            this.ticket_attachments = ticket_attachments;
        }

        public Guid id { get; private set; }
        public string title { get; private set; }
        public string content { get; private set; }
        public int phone_number { get; private set; }
        public string created_date { get; private set; }
        public string status { get; private set; }

        public Guid ticket_category_id { get; private set; }
        public string ticket_category_name { get; private set; }

        public Guid creator_id { get; private set; }
        public string creator_name { get; private set; }
        public List<ticket_reply_vm> replies { get; private set; }
        public string[] ticket_attachments { get; private set; } 

    }
}
