using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Dtos.SlideDtos
{
    public class AddSlideResponse
    {
        public AddSlideResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
