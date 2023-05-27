using PostManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostManager.Core.Interfaces.Services
{
    public interface IPostService
    {
        Task<IList<Post>> GetPostsByQueryParams(string tags, string sortBy, string direction);
    }
}
