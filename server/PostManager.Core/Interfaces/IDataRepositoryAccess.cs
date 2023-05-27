using PostManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostManager.Core.Interfaces
{
    public interface IDataRepositoryAccess
    {
        /// <summary>
        /// Get all posts related to the same tag 
        /// </summary>
        /// <param name="tag">The provided tag</param>
        /// <returns>List of posts</returns>
        Task<IEnumerable<Post>> GetPosts(string tag);
    }
}
