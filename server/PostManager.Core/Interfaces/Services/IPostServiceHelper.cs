using PostManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostManager.Core.Interfaces.Services
{
    public interface IPostServiceHelper
    {
        IDictionary<int, Post> SanitizePost(List<Post> posts);
        void SortBy(ref IQueryable<Post> postsQuery, string sortBy, string direction);
    }
}
