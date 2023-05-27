using PostManager.Core.Interfaces.Services;
using PostManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PostManager.Bussiness.Helpers
{
    public class PostServiceHelper : IPostServiceHelper
    {
        public PostServiceHelper() { }

        
        public IDictionary<int, Post> SanitizePost(List<Post> posts)
        {
            IDictionary<int, Post> uniquePosts = new Dictionary<int, Post>();

            foreach (var post in posts) 
            {
                if(!uniquePosts.ContainsKey(post.Id))
                {
                    uniquePosts.Add(post.Id, post);
                }
            }

            return uniquePosts;
        }


        public void SortBy(ref IQueryable<Post> postsQuery, string sortBy, string direction)
        {
            switch (sortBy.ToLower())
            {
                case "reads":
                    postsQuery = direction == "asc" ? postsQuery.OrderBy(x => x.Reads) : postsQuery.OrderByDescending(x => x.Reads);
                    break;
                case "likes":
                    postsQuery = direction == "asc" ? postsQuery.OrderBy(x => x.Likes) : postsQuery.OrderByDescending(x => x.Likes);
                    break;
                case "popularity":
                    postsQuery = direction == "asc" ? postsQuery.OrderBy(x => x.Popularity) : postsQuery.OrderByDescending(x => x.Popularity);
                    break;
                case "id":
                default:
                    postsQuery = direction == "asc" ? postsQuery.OrderBy(x => x.Id) : postsQuery.OrderByDescending(x => x.Id);
                    break;
            }

        }
    }
}
