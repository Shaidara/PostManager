using FluentAssertions;
using PostManager.Bussiness.Helpers;
using PostManager.Core.Interfaces.Services;
using PostManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostManager.Bussiness.Tests.Helpers
{
    public class PostServiceHelperTests
    {
        private readonly IPostServiceHelper _sut;
        public PostServiceHelperTests()
        {
            _sut = new PostServiceHelper();
        }

        [Fact]
        public void SanitizePost_ShouldRemoveDuplicate()
        {
            //Arrange
            List<Post> posts = GetDefaultPosts().ToList();
            int initialCount = 5;
            int expectedCount = 3;

            posts.Add(new Post
                {
                    Id = 2,
                    Author = "Rylee Paul",
                    AuthorId = 9,
                    Likes = 256,
                    Popularity = new decimal(0.26),
                    Reads = 23658,
                    Tags = new string[] {"science", "health"}

                });
            posts.Add(
                new Post
                {
                    Id = 3,
                    Author = "Jimmy Carter",
                    AuthorId = 8,
                    Likes = 658,
                    Popularity = new decimal(0.35),
                    Reads = 25639,
                    Tags = new string[] {"design", "culture", "tech"}
                });

            posts.Count.Should().Be(initialCount);

            //Act
            var result = _sut.SanitizePost(posts);


            //Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(expectedCount);
        }


        [Fact]
        public void SortBy_WhenSortByReads_WithDirectionAsc_ShouldReturnSortedPost()
        {
            //Arrange
            var posts = GetDefaultPosts();
            var query = posts.AsQueryable();

            //Act
            _sut.SortBy(ref query, "reads", "asc");
            var result = query.ToList();

            //Assert
            result[0].Id.Should().Be(posts.ToList()[1].Id);
            result[1].Id.Should().Be(posts.ToList()[2].Id);
            result[2].Id.Should().Be(posts.ToList()[0].Id);
            
        }

        [Fact]
        public void SortBy_WhenSortByReads_WithDirectionDesc_ShouldReturnSortedPost()
        {
            //Arrange
            var posts = GetDefaultPosts();
            var query = posts.AsQueryable();

            //Act
            _sut.SortBy(ref query, "reads", "desc");
            var result = query.ToList();

            //Assert
            result[0].Id.Should().Be(posts.ToList()[0].Id);
            result[1].Id.Should().Be(posts.ToList()[2].Id);
            result[2].Id.Should().Be(posts.ToList()[1].Id);
            
        }

        [Fact]
        public void SortBy_WhenSortById_WithDirectionAsc_ShouldReturnSortedPost()
        {
            //Arrange
            var posts = GetDefaultPosts();
            var query = posts.AsQueryable();

            //Act
            _sut.SortBy(ref query, "id", "asc");
            var result = query.ToList();

            //Assert
            result[0].Id.Should().Be(posts.ToList()[0].Id);
            result[1].Id.Should().Be(posts.ToList()[1].Id);
            result[2].Id.Should().Be(posts.ToList()[2].Id);
            
        }

        [Fact]
        public void SortBy_WhenSortById_WithDirectionDesc_ShouldReturnSortedPost()
        {
            //Arrange
            var posts = GetDefaultPosts();
            var query = posts.AsQueryable();

            //Act
            _sut.SortBy(ref query, "id", "desc");
            var result = query.ToList();

            //Assert
            result[0].Id.Should().Be(posts.ToList()[2].Id);
            result[1].Id.Should().Be(posts.ToList()[1].Id);
            result[2].Id.Should().Be(posts.ToList()[0].Id);
            
        }

        
        [Fact]
        public void SortBy_WhenSortByLikes_WithDirectionAsc_ShouldReturnSortedPost()
        {
            //Arrange
            var posts = GetDefaultPosts();
            var query = posts.AsQueryable();

            //Act
            _sut.SortBy(ref query, "likes", "asc");
            var result = query.ToList();

            //Assert
            result[0].Id.Should().Be(posts.ToList()[1].Id);
            result[1].Id.Should().Be(posts.ToList()[2].Id);
            result[2].Id.Should().Be(posts.ToList()[0].Id);
            
        }

        [Fact]
        public void SortBy_WhenSortByLikes_WithDirectionDesc_ShouldReturnSortedPost()
        {
            //Arrange
            var posts = GetDefaultPosts();
            var query = posts.AsQueryable();

            //Act
            _sut.SortBy(ref query, "likes", "desc");
            var result = query.ToList();

            //Assert
            result[0].Id.Should().Be(posts.ToList()[0].Id);
            result[1].Id.Should().Be(posts.ToList()[2].Id);
            result[2].Id.Should().Be(posts.ToList()[1].Id);
            
        }

        
        [Fact]
        public void SortBy_WhenSortByPopularity_WithDirectionAsc_ShouldReturnSortedPost()
        {
            //Arrange
            var posts = GetDefaultPosts();
            var query = posts.AsQueryable();

            //Act
            _sut.SortBy(ref query, "popularity", "asc");
            var result = query.ToList();

            //Assert
            result[0].Id.Should().Be(posts.ToList()[0].Id);
            result[1].Id.Should().Be(posts.ToList()[1].Id);
            result[2].Id.Should().Be(posts.ToList()[2].Id);
            
        }

        [Fact]
        public void SortBy_WhenSortByPopularity_WithDirectionDesc_ShouldReturnSortedPost()
        {
            //Arrange
            var posts = GetDefaultPosts();
            var query = posts.AsQueryable();

            //Act
            _sut.SortBy(ref query, "popularity", "desc");
            var result = query.ToList();

            //Assert
            result[0].Id.Should().Be(posts.ToList()[2].Id);
            result[1].Id.Should().Be(posts.ToList()[1].Id);
            result[2].Id.Should().Be(posts.ToList()[0].Id);
            
        }

        private IEnumerable<Post> GetDefaultPosts()
        {
            
            IEnumerable<Post> defaultPosts = new List<Post>
            {
                new Post
                {
                    Id = 1,
                    Author = "Chaka Zulu",
                    AuthorId = 2,
                    Likes = 960,
                    Popularity = new decimal(0.13),
                    Reads = 50361,
                    Tags = new string[] {"tech", "health"}

                },
                new Post
                {
                    Id = 2,
                    Author = "Rylee Paul",
                    AuthorId = 9,
                    Likes = 256,
                    Popularity = new decimal(0.26),
                    Reads = 23658,
                    Tags = new string[] {"science", "health"}

                },
                new Post
                {
                    Id = 3,
                    Author = "Jimmy Carter",
                    AuthorId = 8,
                    Likes = 658,
                    Popularity = new decimal(0.35),
                    Reads = 25639,
                    Tags = new string[] {"design", "culture", "tech"}
                },
            };

            return defaultPosts;
        }

        private IDictionary<int, Post> BuildHashMap(List<Post> posts)
        {
            IDictionary<int, Post> hashMap = new Dictionary<int, Post>();

            foreach (Post post in posts)
            {
                hashMap[post.Id] = post;
            }

            return hashMap;
        }
    }
}
