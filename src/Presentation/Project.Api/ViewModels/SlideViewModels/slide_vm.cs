using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Api.ViewModels.SlideViewModels
{
    public class slide_vm
    {
        public slide_vm(Guid id, int viewOrder, string imageUrl, string title, string description, string buttunText, string buttunUrl, bool status)
        {
            this.id = id;
            this.view_order = viewOrder;
            this.image_url = imageUrl;
            this.title = title;
            this.description = description;
            this.buttun_text = buttunText;
            this.buttun_url = buttunUrl;
            this.status = status;
        }

        public Guid id { get; private set; }
        public int view_order { get; private set; }
        public string image_url { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }
        public string buttun_text { get; private set; }
        public string buttun_url { get; private set; }
        public bool status { get; private set; }


    }
}
