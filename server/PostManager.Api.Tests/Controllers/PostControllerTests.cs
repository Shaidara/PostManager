using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PostManager.Controllers;
using PostManager.Core.Interfaces.Services;
using PostManager.Core.Models;
using PostManager.Core.Payload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostManager.Api.Tests.Controllers
{
    public class PostControllerTests
    {
        private readonly Mock<IPostService> _postService;
        private readonly PostController _sut;
        public PostControllerTests()
        {
            _postService = new Mock<IPostService>();
            _sut = new PostController(_postService.Object);
        }

        [Fact]
        public async Task GetPostByFilter_WhenPostsFound_ShouldReturnValidResponse()
        {
            //Arrange
            string tags = "tech,culture,science";
            string sortBy = "reads";
            string direction = "asc";
            var mockedPosts = GetDefaultPosts().ToList();

            _postService
                .Setup(x => x.GetPostsByQueryParams(tags, sortBy, direction))
                .ReturnsAsync(mockedPosts)
                .Verifiable();

            //Act
            var response = await _sut.GetPostByFilter(tags, sortBy, direction);
            
            var okResponse = (OkObjectResult)response;

            //Assert
            okResponse.Value.Should().NotBeNull();
            okResponse.Value.Should().BeAssignableTo<ResponsePayload<IList<Post>>>();

            ResponsePayload<IList<Post>> value = (ResponsePayload<IList<Post>>) okResponse.Value;
            value.Count.Should().Be(mockedPosts.Count);
            value.Data.Should().BeEquivalentTo(mockedPosts);

            _postService.VerifyAll();
        }

        [Fact]
        public async Task GetPostByFilter_WhenArgumentExceptionOnService_ShouldReturnBadRequestResponse()
        {
            //Arrange
            string tags = "tech,culture,science";
            string sortBy = "id";
            string direction = "asc";
            var mockedPosts = GetDefaultPosts().ToList();
            string mockedMessage = "Invalid param";

            _postService
                .Setup(x => x.GetPostsByQueryParams(tags, sortBy, direction))
                .ThrowsAsync(new ArgumentException(mockedMessage))
                .Verifiable();

            //Act
            var response = await _sut.GetPostByFilter(tags);
            
            var badRequestResponse = (ObjectResult)response;
            //Assert
            badRequestResponse.Value.Should().NotBeNull();
            badRequestResponse.Value.Should().BeAssignableTo<ErrorPayload>();

            var error = (ErrorPayload)badRequestResponse.Value;
            error.ErrorMessage.Should().Be(mockedMessage);
            error.Status.Should().Be(StatusCodes.Status400BadRequest);

            _postService.VerifyAll();
        }

        
        [Fact]
        public async Task GetPostByFilter_WhenExceptionOnService_ShouldReturnBadRequestResponse()
        {
            //Arrange
            string tags = "tech,culture,science";
            string sortBy = "id";
            string direction = "asc";
            var mockedPosts = GetDefaultPosts().ToList();
            string mockedMessage = "Unknown error";

            _postService
                .Setup(x => x.GetPostsByQueryParams(tags, sortBy, direction))
                .ThrowsAsync(new Exception(mockedMessage))
                .Verifiable();

            //Act
            var response = await _sut.GetPostByFilter(tags);
            
            var badRequestResponse = (ObjectResult)response;
            //Assert
            badRequestResponse.Value.Should().NotBeNull();
            badRequestResponse.Value.Should().BeAssignableTo<ErrorPayload>();

            var error = (ErrorPayload)badRequestResponse.Value;
            error.ErrorMessage.Should().Be(mockedMessage);
            error.Status.Should().Be(StatusCodes.Status500InternalServerError);

            _postService.VerifyAll();
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
    }
}
