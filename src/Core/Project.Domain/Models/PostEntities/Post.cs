using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Common;
using Project.Common.Exceptions;
using Project.Common.Utilities;

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

        public void AddKeyword(PostKeyword keyword)
        {
            _keywords.Add(keyword);
        }

        public void RemoveKeyword(PostKeyword keyword)
        {
            _keywords.Remove(keyword);
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

        public void AddComment(PostComment comment)
        {
            _comments.Add(comment);
        }

        public void RemoveComment(PostComment comment)
        {
            _comments.Remove(comment);
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

        public List<PostsCategories> _categories = new List<PostsCategories>();
        [NotMapped]
        public virtual IReadOnlyCollection<PostsCategories> Categories
        {
            get { return _categories.AsReadOnly(); }
        }


        private List<PostKeyword> _keywords = new List<PostKeyword>();
        [NotMapped]
        public IReadOnlyCollection<PostKeyword> Keywords
        {
            get { return _keywords.AsReadOnly(); }
        }

        private List<PostComment> _comments = new List<PostComment>();
        [NotMapped]
        public IReadOnlyCollection<PostComment> Comments
        {
            get { return _comments.AsReadOnly(); }
        }

        private List<PostView> _views = new List<PostView>();
        [NotMapped]
        public IReadOnlyCollection<PostView> Views
        {
            get { return _views.AsReadOnly(); }
        }

        private List<PostVote> _votes = new List<PostVote>();
        [NotMapped]
        public IReadOnlyCollection<PostVote> Votes
        {
            get { return _votes.AsReadOnly(); }
        }

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


        }
    }
}
