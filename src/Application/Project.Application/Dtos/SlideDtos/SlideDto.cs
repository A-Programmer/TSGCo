using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Dtos.SlideDtos
{
    public class SlideDto
    {
        public SlideDto(Guid id, int viewOrder, string imageUrl, string title, string description, string buttunText, string buttunUrl, bool status)
        {
            Id = id;
            ViewOrder = viewOrder;
            ImageUrl = imageUrl;
            Title = title;
            Description = description;
            ButtunText = buttunText;
            ButtunUrl = buttunUrl;
            Status = status;
        }
        
        public Guid Id { get; private set; }
        public int ViewOrder { get; private set; }
        public string ImageUrl { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ButtunText { get; private set; }
        public string ButtunUrl { get; private set; }
        public bool Status { get; private set; }




    }
}
