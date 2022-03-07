using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Shared;
using Project.Domain.Shared.Exceptions;
using Project.Domain.Shared.Utilities;

namespace Project.Domain.Models.PostEntities
{
    [Serializable]
    public class Post : BaseEntityWithDetails<Guid>, IAggregateRoot, ISerializable
    {

        public Post(string title, string seoTitle, string description, string seoDescription, string content, string imageUrl, bool status, bool showInSlides)
        {
            if (string.IsNullOrEmpty(title))
                throw new AppException(ApiResultStatusCode.BadRequest, "عنوان الزامی است", HttpStatusCode.BadRequest);

            if (string.IsNullOrEmpty(description))
                throw new AppException(ApiResultStatusCode.BadRequest, "توضیحات الزامی است", HttpStatusCode.BadRequest);

            if (string.IsNullOrEmpty(content))
                throw new AppException(ApiResultStatusCode.BadRequest, "متن الزامی است", HttpStatusCode.BadRequest);

            if (string.IsNullOrEmpty(imageUrl))
                throw new AppException(ApiResultStatusCode.BadRequest, "تصویر الزامی است", HttpStatusCode.BadRequest);

            Title = title;
            Description = description;
            Content = content;
            ImageUrl = imageUrl;
            Status = status;
            ShowInSlides = showInSlides;
            Slug = title.CreateSlug();

            if (!string.IsNullOrEmpty(seoTitle))
                SeoTitle = seoTitle;
            else
                SeoTitle = title;

            if (!string.IsNullOrEmpty(seoDescription))
                SeoDescription = seoDescription;
            else
                SeoDescription = description;
        }


        protected Post()
        {
        }


        public void Update(string title, string seoTitle, string description, string seoDescription, string content, string imageUrl, bool status, bool showInSlides)
        {
            if (string.IsNullOrEmpty(title))
                throw new AppException(ApiResultStatusCode.BadRequest, "عنوان الزامی است", HttpStatusCode.BadRequest);

            if (string.IsNullOrEmpty(description))
                throw new AppException(ApiResultStatusCode.BadRequest, "توضیحات الزامی است", HttpStatusCode.BadRequest);

            if (string.IsNullOrEmpty(content))
                throw new AppException(ApiResultStatusCode.BadRequest, "متن الزامی است", HttpStatusCode.BadRequest);

            if (string.IsNullOrEmpty(imageUrl))
                throw new AppException(ApiResultStatusCode.BadRequest, "تصویر الزامی است", HttpStatusCode.BadRequest);

            Title = title;
            Description = description;
            Content = content;
            ImageUrl = imageUrl;
            Status = status;
            ShowInSlides = showInSlides;
            ModifiedAt = DateTimeOffset.Now;

            if (!string.IsNullOrEmpty(seoTitle))
                SeoTitle = seoTitle;
            else
                SeoTitle = title;

            if (!string.IsNullOrEmpty(seoDescription))
                SeoDescription = seoDescription;
            else
                SeoDescription = description;
        }

        public void SetCategories(Guid[] categoriesIds)
        {
            _categories.Clear();

            foreach (var categoryId in categoriesIds)
                _categories.Add(new PostsCategories(this.Id, categoryId));
        }

        public void UpdateSlug(string slug)
        {
            Slug = slug.CreateSlug();
        }

        public void ChangeStatus()
        {
            Status = !Status;
        }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

        public void SetComments(List<PostComment> comments)
        {
            _comments.Clear();
            _comments = comments;
        }

        public void AddComment(PostComment comment)
        {
            _comments.Add(comment);
        }

        public void RemoveComment(PostComment comment)
        {
            _comments.Remove(comment);
        }

        public void SetKeywords(Guid[] keywordsIds)
        {
            _keywords.Clear();
            foreach (var keywordId in keywordsIds)
                _keywords.Add(new PostsKeywords(this.Id, keywordId));
        }

        public void SetVotes(List<PostVote> votes)
        {
            _votes.Clear();
            _votes = votes;
        }
        public void AddVote(PostVote vote)
        {
            if (!_votes.Contains(vote))
                _votes.Add(vote);
            else
                throw new AppException(ApiResultStatusCode.BadRequest, "قبلا به این پست رای داده اید", HttpStatusCode.BadRequest);
        }

        public void RemoveVote(PostVote vote)
        {
            if (_votes.Contains(vote))
                _votes.Remove(vote);
        }

        public void SetViews(List<PostView> views)
        {
            _views.Clear();
            _views = views;
        }
        public void AddView(PostView view)
        {
            _views.Add(view);
        }

        public void ChangeShowInSlideStatus(bool newStatus)
        {
            ShowInSlides = newStatus;
        }

        public string Title { get; private set; }
        public string SeoTitle { get; private set; }
        public string Description { get; private set; }
        public string SeoDescription { get; private set; }
        public string Content { get; private set; }
        public string ImageUrl { get; private set; }
        public bool Status { get; private set; }
        public string Slug { get; private set; }
        public bool ShowInSlides { get; private set; }
        public Guid UserId { get; private set; }


        public virtual IReadOnlyCollection<PostsCategories> Categories => _categories;
        protected List<PostsCategories> _categories = new List<PostsCategories>();


        public virtual IReadOnlyCollection<PostsKeywords> Keywords => _keywords;
        protected List<PostsKeywords> _keywords = new List<PostsKeywords>();

        public IReadOnlyCollection<PostComment> Comments => _comments;
        protected List<PostComment> _comments = new List<PostComment>();

        public IReadOnlyCollection<PostView> Views => _views;
        protected List<PostView> _views = new List<PostView>();
        public long AllViewsCount
        {
            get
            {
                return _views.Count;
            }
        }
        public long UniqueViewsCount
        {
            get
            {
                return _views.Distinct().Count(); ;
            }
        }

        public IReadOnlyCollection<PostVote> Votes => _votes;
        protected List<PostVote> _votes = new List<PostVote>();

        #region Serialization

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(Title), Id);
            info.AddValue(nameof(SeoTitle), Id);
            info.AddValue(nameof(Description), Id);
            info.AddValue(nameof(SeoDescription), Id);
            info.AddValue(nameof(Content), Id);
            info.AddValue(nameof(ImageUrl), Id);
            info.AddValue(nameof(Status), Id);
            info.AddValue(nameof(Slug), Id);
            info.AddValue(nameof(ShowInSlides), Id);
            info.AddValue(nameof(Id), Id);

            info.AddValue(nameof(Categories), Categories);
            info.AddValue(nameof(Keywords), Keywords);
            info.AddValue(nameof(Comments), Comments);
            info.AddValue(nameof(Views), Views);
            info.AddValue(nameof(Votes), Votes);

        }
        #endregion

    }

    public class PostConfigurations : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ShowInSlides).HasDefaultValue(false);


            builder.OwnsMany(x => x.Categories, c =>
            {
                c.ToTable("PostsCategories");
            });
            builder.OwnsMany(x => x.Keywords, c =>
            {
                c.ToTable("PostsKeywords");
            });
            builder.OwnsMany(x => x.Views, c =>
            {
                c.ToTable("PostViews");
            });
            builder.OwnsMany(x => x.Votes, c =>
            {
                c.ToTable("PostVotes");
            });

        }
    }
}
