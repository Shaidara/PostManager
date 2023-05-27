using Microsoft.Extensions.Logging;
using PostManager.Bussiness.Services;
using PostManager.Core.Interfaces.Services;
using PostManager.Core.Interfaces;
using Moq;
using FluentAssertions;
using PostManager.Core.Models;

namespace PostManager.Bussiness.Tests
{
    public class PostServiceTests
    {
        private readonly Mock<IDataRepositoryAccess> _dataRepositoryAccess;
        private readonly Mock<IPostServiceHelper> _postServiceHelper;
        private readonly Mock<ILogger<PostService>> _logger; 
        private readonly PostService _sut;

        public PostServiceTests()
        {
            _dataRepositoryAccess = new Mock<IDataRepositoryAccess>();
            _postServiceHelper = new Mock<IPostServiceHelper>();
            _logger = new Mock<ILogger<PostService>>();

            _sut = new PostService(_postServiceHelper.Object, _dataRepositoryAccess.Object, _logger.Object);
        }

        [Fact]
        public async Task GetPostsByQueryParams_WhenValidQueryParamProvided_ShouldReturnPosts()
        {
            //Arrange
            string tags = "tech,health";
            string[] tagsSplitted = tags.Split(',');
            string sortBy = "id";
            string direction = "asc";
            var mockedPost = GetDefaultPosts().ToList();
            var sanitized = BuildHashMap(mockedPost);

            _dataRepositoryAccess
                .Setup(x => x.GetPosts("tech"))
                .ReturnsAsync(new List<Post> { mockedPost[0], mockedPost[2] })
                .Verifiable();
            _dataRepositoryAccess
                .Setup(x => x.GetPosts("health"))
                .ReturnsAsync(new List<Post> { mockedPost[0], mockedPost[1] })
                .Verifiable();

            _postServiceHelper
                .Setup(x => x.SanitizePost(It.IsAny<List<Post>>()))
                .Returns(sanitized)
                .Verifiable();

            //Act
            IList<Post> result = await _sut.GetPostsByQueryParams(tags, sortBy, direction);

            //Assert
            result.Should().BeAssignableTo<List<Post>>();
            _postServiceHelper.VerifyAll();
            
            _postServiceHelper
                .Verify(x => x.SortBy(ref It.Ref<IQueryable<Post>>.IsAny, sortBy, direction));
                

             _dataRepositoryAccess
                .Verify(x => x.GetPosts("tech"), Times.Once);

            _dataRepositoryAccess
                .Verify(x => x.GetPosts("health"), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void TryValidateQueryParam_WhenTagNotProvided_ShouldThrow(string tags)
        {
            //Arrange            

            //Act
            Func<Task> action = async() => await _sut.GetPostsByQueryParams(tags, "id", "desc");

            //Assert
            action.Should().ThrowAsync<ArgumentException>().WithMessage("tags parameter is required");
        }

        
        [Theory]
        [InlineData("tag")]
        [InlineData("author")]
        [InlineData("authorId")]
        [InlineData("unknown")]
        public void TryValidateQueryParam_WhenInvalidsortProvided_ShouldThrow(string sortBy)
        {
            //Arrange
            string tags = "tech";
            

            //Act
            Func<Task> action = async() => await _sut.GetPostsByQueryParams(tags, sortBy, "desc");

            //Assert
            action.Should().ThrowAsync<ArgumentException>().WithMessage("sortBy parameter is invalid");
        }

        [Theory]
        [InlineData("id")]
        [InlineData("likes")]
        [InlineData("reads")]
        [InlineData("popularity")]
        public void TryValidateQueryParam_WhenValidSortByProvided_ShouldNotThrow(string sortBy)
        {
            //Arrange
            string tags = "tech";
            

            //Act
            Func<Task> action = async() => await _sut.GetPostsByQueryParams(tags, sortBy, "desc");

            //Assert
            action.Should().NotThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData("left")]
        [InlineData("right")]
        public void TryValidateQueryParam_WhenInvalidDirectionProvided_ShouldThrow(string sortBy)
        {
            //Arrange
            string tags = "tech";
            

            //Act
            Func<Task> action = async() => await _sut.GetPostsByQueryParams(tags, sortBy, "desc");

            //Assert
            action.Should().ThrowAsync<ArgumentException>().WithMessage("sortBy parameter is invalid");
        }

        
        [Theory]
        [InlineData("asc")]
        [InlineData("desc")]
        public void TryValidateQueryParam_WhenValidDirectionProvided_ShouldNotThrow(string direction)
        {
            //Arrange
            string tags = "tech";
            

            //Act
            Func<Task> action = async() => await _sut.GetPostsByQueryParams(tags, "id", direction);

            //Assert
            action.Should().NotThrowAsync<ArgumentException>();
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
