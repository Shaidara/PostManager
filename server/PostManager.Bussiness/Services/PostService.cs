using Microsoft.Extensions.Logging;
using PostManager.Core.Interfaces;
using PostManager.Core.Interfaces.Services;
using PostManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PostManager.Bussiness.Services
{
    public class PostService : IPostService
    {
        private readonly IDataRepositoryAccess _dataRepositoryAccess;
        private readonly IPostServiceHelper _postServiceHelper;
        private readonly ILogger<PostService> _logger;

        public PostService(
            IPostServiceHelper postServiceHelper, 
            IDataRepositoryAccess dataRepositoryAccess, 
            ILogger<PostService> logger)
        {
            _postServiceHelper = postServiceHelper;
            _dataRepositoryAccess = dataRepositoryAccess;
            _logger = logger;
        }

        public async Task<IList<Post>> GetPostsByQueryParams(string tags, string sortBy, string direction)
        {
            TryValidateQueryParam(tags, sortBy, direction);

            List<Post> allPosts = new List<Post>();
            string[] tagSplitted = tags.Split(',');

            foreach (string tag in tagSplitted)
            {
                if (!string.IsNullOrWhiteSpace(tag))
                {
                    try
                    {
                        var postsForTag = (await _dataRepositoryAccess.GetPosts(tag.Trim())).ToList();
                        allPosts.AddRange(postsForTag);
                    }
                    catch (InvalidOperationException ex)
                    {
                        _logger.LogError("Error while retrieving posts", ex);
                        throw;
                    }
                }
            }

            var uniquePost = _postServiceHelper.SanitizePost(allPosts);

            var postQuery = uniquePost.Values.AsQueryable();
            _postServiceHelper.SortBy(ref postQuery, sortBy, direction);

            return postQuery.ToList();
        }

        private void TryValidateQueryParam(string tags, string sortBy, string direction)
        {

            string[] validSortItems = new string[] { "id", "reads", "likes", "popularity" };
            string[] validDirection = new string[] { "asc", "desc" };

            if (string.IsNullOrWhiteSpace(tags)) throw new ArgumentException("tags parameter is required");

            bool isValidSortItem = validSortItems.Contains(sortBy.ToLower());

            if (!isValidSortItem) throw new ArgumentException("sortBy parameter is invalid");

            bool isValidDirection = validDirection.Contains(direction.ToLower());

            if (!isValidDirection) throw new ArgumentException("direction parameter is invalid");
        }

    }
}
