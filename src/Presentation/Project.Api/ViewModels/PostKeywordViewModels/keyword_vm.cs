using System;
using System.Collections.Generic;
using System.Linq;
using Project.Api.ViewModels.PostViewModels;
using Project.Api.ViewModels.UserViewModels;
using Project.Application.Dtos.Posts;

namespace Project.Api.ViewModels.PostKeywordViewModels
{
    public class keyword_vm
    {
        public keyword_vm(Guid id, string title, string name)
        {
            this.id = id;
            this.title = title;
            this.name = name;
        }

        public Guid id { get; private set; }
        public string title { get; private set; }
        public string name { get; private set; }
    }
}
