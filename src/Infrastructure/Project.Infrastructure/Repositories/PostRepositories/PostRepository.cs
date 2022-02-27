using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Common;
using Project.Common.Exceptions;
using Project.Common.Utilities;
using Project.Domain.Contracts.Repositories.PostRepositories;
using Project.Domain.Models.PostEntities;

namespace Project.Infrastructure.Repositories.BlogRepositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {

        public PostRepository(ProjectDbContext db)
            : base(db)
        {
        }



        //public async Task<Post> GetPostByIdWithCategoryAsync(Guid id)
        //{
        //    return await Entity.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        //}

        public async ValueTask<Post> GetByIdAsync(Guid id, bool includeCategories = false, bool includeComments = false, bool includeKeywords = false)
        {
            var post = Entity.AsQueryable();
            post = post.Include(x => x.Views).Include(x => x.Votes);

            if (includeCategories)
                post = post.Include(x => x.Categories);
            if (includeComments)
                post = post.Include(x => x.Comments);
            if (includeKeywords)
                post = post.Include(x => x.Keywords);

            return await post
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Post>> GetAllPosts(string status = null, bool includeCategories = false, bool includeComments = false, bool includeKeywords = false)
        {
            var posts = GetAllPosts();

            posts = posts.Include(x => x.Views).Include(x => x.Votes);

            if (includeCategories)
                posts = posts.Include(x => x.Categories);
            if (includeComments)
                posts = posts.Include(x => x.Comments);
            if (includeKeywords)
                posts = posts.Include(x => x.Keywords);

            switch (status)
            {
                case null:
                    {
                        return await posts.Where(x => x.Status).ToListAsync();
                    }
                case "all":
                    {
                        return await posts.ToListAsync();
                    }
                case "active":
                    {
                        return await posts.Where(x => x.Status).ToListAsync();
                    }
                case "deactive":
                    {
                        return await posts.Where(x => !x.Status).ToListAsync();
                    }
                default :
                    {
                        return await posts.Where(x => x.Status).ToListAsync();
                    }
            }

        }

        public async Task<IEnumerable<Post>> GetPostsByCategoryId(Guid categoryId, bool includeCategories = false, bool includeComments = false, bool includeKeywords = false)
        {
            var posts = GetAllPosts();

            posts = posts.Include(x => x.Views).Include(x => x.Votes);

            if (includeCategories)
                posts = posts.Include(x => x.Categories);
            if (includeComments)
                posts = posts.Include(x => x.Comments);
            if (includeKeywords)
                posts = posts.Include(x => x.Keywords);

            return await posts
                .Where(x => x.Categories.Any(x => x.CategoryId == categoryId))
                .ToListAsync();
        }

        public async Task<Post> GetBySlug(string slug, bool includeCategories = false, bool includeComments = false, bool includeKeywords = false)
        {
            var post = Entity.AsQueryable();

            post = post.Include(x => x.Views).Include(x => x.Votes);

            if (includeCategories)
                post = post.Include(x => x.Categories);
            if (includeComments)
                post = post.Include(x => x.Comments);
            if (includeKeywords)
                post = post.Include(x => x.Keywords);

            return await post
                .FirstOrDefaultAsync(x => x.Slug.ToLower() == slug.ToLower());
        }

        //public async Task<Post> GetByPostBySlugAndComments(string slug, string commentsStatus = "approved")
        //{
        //    var post = Entity.AsQueryable();

        //    switch (commentsStatus)
        //    {
        //        case "all":
        //            {
        //                post = post.Include(x => x.User).Include(x => x.Views).Include(x => x.Votes).Include(x => x.Comments).ThenInclude(x => x.Replies);
        //                break;
        //            }

        //        case "approved":
        //            {
        //                post = post.Include(x => x.User).Include(x => x.Views).Include(x => x.Votes).Include(x => x.Comments.Where(x => x.IsApproved)).ThenInclude(x => x.Replies.Where(x => x.IsApproved));
        //                break;
        //            }
        //        case "not_approved":
        //            {
        //                post = post.Include(x => x.User).Include(x => x.Views).Include(x => x.Votes).Include(x => x.Comments.Where(x => !x.IsApproved)).ThenInclude(x => x.Replies.Where(x => !x.IsApproved));
        //                break;
        //            }
        //        default:
        //            {
        //                post = post.Include(x => x.User).Include(x => x.Views).Include(x => x.Votes).Include(x => x.Comments.Where(x => x.IsApproved)).ThenInclude(x => x.Replies.Where(x => x.IsApproved));
        //                break;
        //            }
        //    }

        //    return await post
        //        .FirstOrDefaultAsync(x => x.Slug.ToLower() == slug.ToLower());
        //}

        public async Task<IEnumerable<Post>> GetPostsByKeyword(string keywordName, bool includeCategories = false, bool includeComments = false, bool includeKeywords = false)
        {
            var posts = GetAllPosts();

            posts = posts.Include(x => x.Views).Include(x => x.Votes);

            if (includeCategories)
                posts = posts.Include(x => x.Categories);
            if (includeComments)
                posts = posts.Include(x => x.Comments);
            if (includeKeywords)
                posts = posts.Include(x => x.Keywords);

            return await posts
                .Where(x => x.Keywords.Any(x => x.Name.ToLower() == keywordName.ToLower()))
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetSlides()
        {
            return await Entity.Where(x => x.ShowInSlides).ToListAsync();
        }

        public async Task<IEnumerable<PostView>> GetPostViews(Guid postId)
        {
            var post = await Entity.Include(x => x.Views).FirstOrDefaultAsync(x => x.Id == postId);
            return post.Views;
        }

        public async Task<IEnumerable<PostVote>> GetPostVotes(Guid postId)
        {
            var post = await Entity.Include(x => x.Votes).FirstOrDefaultAsync(x => x.Id == postId);
            return post.Votes;
        }

        public async Task RemovePostViews(Guid postId)
        {
            var views = (await Entity.Include(x => x.Views).FirstOrDefaultAsync(x => x.Id == postId)).Views;
            foreach (var view in views)
                DbContext.Set<PostView>().Remove(view);
        }

        public async Task RemovePostVotes(Guid postId)
        {
            var votes = (await Entity.Include(x => x.Votes).FirstOrDefaultAsync(x => x.Id == postId)).Votes;
            foreach (var vote in votes)
                DbContext.Set<PostVote>().Remove(vote);
        }

        //public async Task RemovePostsCategories(Guid postId)
        //{
        //    var postsCategories = (await Entity.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == postId)).Categories;
        //    foreach (var postCategory in postsCategories)
        //        DbContext.Set<PostsCategories>().Remove(postCategory);
        //}

        //public async Task RemovePostsKeywords(Guid postId)
        //{
        //    var postsKeywords = (await Entity.Include(x => x.Keywords).FirstOrDefaultAsync(x => x.Id == postId)).Keywords;
        //    foreach (var postKeyword in postsKeywords)
        //        DbContext.Set<PostsKeywords>().Remove(postKeyword);
        //}

        public async Task RemovePostComments(Guid postId)
        {
            var postComments = (await Entity.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == postId)).Comments;
            foreach (var comment in postComments)
                DbContext.Set<PostComment>().Remove(comment);
        }



        private IQueryable<Post> GetAllPosts()
        {
            return Entity
                .AsQueryable();
        }



        #region PostComments

        public async Task<List<PostComment>> GetPostComments(string slug, string commentsStatus = "approved")
        {
            var post = await Entity.Include(x => x.Comments.Where(x => x.ParentId == null)).FirstOrDefaultAsync(x => x.Slug.ToLower() == slug.ToLower());

            if (post == null)
                throw new AppException(ApiResultStatusCode.NotFound, "مطلب مورد نظر یافت نشد", HttpStatusCode.NotFound);

            var comments = post.Comments;

            switch (commentsStatus)
            {
                case "all":
                    {
                        comments = comments.ToList();
                        break;
                    }

                case "approved":
                    {
                        comments = comments.Where(x => x.IsApproved).ToList();
                        break;
                    }
                case "not_approved":
                    {
                        comments = comments.Where(x => !x.IsApproved).ToList();
                        break;
                    }
                default:
                    {
                        comments = comments.Where(x => x.IsApproved).ToList();
                        break;
                    }
            }

            foreach (var parent in comments)
            {
                var child = await GetChildren(parent.Id, commentsStatus);
                parent.SetReplies(child.ToList());
            }

            return comments.ToList();
        }

        public async Task<PostComment> GetPostCommentByIdAsync(Guid id, bool isAdmin, bool loadReplies)
        {

            if (isAdmin)
            {

                var comment = (await Entity.Include(x => x.Comments.Where(x => x.Id == id)).FirstOrDefaultAsync()).Comments.FirstOrDefault();
                if (loadReplies)
                {
                    comment.SetReplies((await GetChildren(comment.Id, "all")).ToList());
                }
                return comment;
            }
            else
            {

                var comment = (await Entity.Include(x => x.Comments.Where(x => x.Id == id && x.IsApproved)).FirstOrDefaultAsync()).Comments.FirstOrDefault();
                if (loadReplies)
                {
                    comment.SetReplies((await GetChildren(comment.Id, "approved")).ToList());
                }
                return comment;
            }
        }

        public async Task<IEnumerable<PostComment>> GetChildren(Guid parentId, string status)
        {
            switch (status)
            {
                case "all":
                    {
                        var replies = await DbContext.Set<PostComment>().Where(x => x.ParentId == parentId).ToListAsync();
                        foreach (var reply in replies)
                        {
                            reply.SetReplies((await GetChildren(reply.Id, status)).ToList());
                        }
                        return replies;
                    }
                case "approved":
                    {
                        var replies = await DbContext.Set<PostComment>().Where(x => x.ParentId == parentId && x.IsApproved).ToListAsync();
                        foreach (var reply in replies)
                        {
                            reply.SetReplies((await GetChildren(reply.Id, status)).ToList());
                        }
                        return replies;
                    }
                case "not_approved":
                    {
                        var replies = await DbContext.Set<PostComment>().Where(x => x.ParentId == parentId && !x.IsApproved).ToListAsync();
                        foreach (var reply in replies)
                        {
                            reply.SetReplies((await GetChildren(reply.Id, status)).ToList());
                        }
                        return replies;
                    }
                default:
                    {
                        var replies = await DbContext.Set<PostComment>().Where(x => x.ParentId == parentId && x.IsApproved).ToListAsync();
                        foreach (var reply in replies)
                        {
                            reply.SetReplies((await GetChildren(reply.Id, status)).ToList());
                        }
                        return replies;
                    }
            }
        }

        public async Task RemoveCommentRecursively(Guid id)
        {
            var comment = (await Entity.Include(x => x.Comments.Where(x => x.Id == id)).ThenInclude(x => x.Replies).FirstOrDefaultAsync()).Comments.FirstOrDefault(x => x.Id == id);
            if (comment != null)
            {
                if (comment.Replies.Any())
                {
                    foreach (var item in comment.Replies)
                    {
                        await RemoveCommentRecursively(item.Id);
                    }
                }
                DbContext.Set<PostComment>().Remove(comment);
            }
        }


        #endregion



        private ProjectDbContext DbContext
        {
            get
            {
                return Context as ProjectDbContext;
            }
        }
    }
}
