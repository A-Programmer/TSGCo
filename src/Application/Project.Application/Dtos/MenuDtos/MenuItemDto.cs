using System;
using System.Collections.Generic;

namespace Project.Application.Dtos.MenuDtos
{
    public class MenuItemDto
    {
        public MenuItemDto(Guid id, string title, string url, int order, Guid? parentId)
        {
            Id = id;
            Title = title;
            Url = url;
            Order = order;
            ParentId = parentId;
        }


        public Guid Id { get; private set; }

        public string Title { get; private set; }

        public string Url { get; private set; }

        public int Order { get; private set; }

        public Guid? ParentId { get; private set; }

    }
}
