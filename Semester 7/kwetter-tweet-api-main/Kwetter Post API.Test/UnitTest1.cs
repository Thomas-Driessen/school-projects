
using Kwetter_Post_API.Controllers;
using Kwetter_Post_API.Core.Interfaces;
using Kwetter_Post_API.Core.Services;
using Kwetter_Post_API.DAL.Interfaces;
using Kwetter_Post_API.DAL.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Kwetter_Post_API.Test
{
    public class UnitTest1
    {
        [Theory]
        [MemberData(nameof(TestGetTweetsDataSet))]
        public async void TestGetTweets(IEnumerable<Post> chatMessages)
        {
            //Arange
            PostServiceMock postServiceMock = new PostServiceMock();
            PostController postController = new PostController(postServiceMock);

            //Act
            IEnumerable<Post> result = await postController.GetFeed();

            //Assert
            Assert.Equal(chatMessages, result);
        }

        public static IEnumerable<object[]> TestGetTweetsDataSet()
        {
            List<object[]> result = new List<object[]>();

            List<Post> posts = new List<Post>();

            for (int i = 0; i < 10; i++)
            {
                posts.Add(new Post
                {
                    Title = "TEST",
                    Content = "TEST CONTENT"
                });
            }

            result.Add(new object[] { posts });

            return result;
        }

        public class PostServiceMock : IPostService
        {
            private PostDbAccessMock postDbAccessMock;

            public PostServiceMock() 
            { 
                postDbAccessMock = new PostDbAccessMock();
            }

            public Task<Post> CreatePost(ClaimsPrincipal user, Post post, IFormFile media)
            {
                throw new NotImplementedException();
            }

            public async Task<List<Post>> GetPosts()
            {
                return await postDbAccessMock.GetPosts();
            }

            public Task<List<Post>> GetTweetsFromUser(Guid id)
            {
                throw new NotImplementedException();
            }
        }

        public class PostDbAccessMock : IPostAccess
        {
            public async Task<List<Post>> GetPosts()
            {
                List<Post> posts = new List<Post>();

                for (int i = 0; i < 10; i++)
                {
                    posts.Add(new Post
                    {
                        Title = "TEST",
                        Content = "TEST CONTENT"
                    });
                }

                return posts;
            }

            public Task<Post> CreatePost(Post post)
            {
                throw new NotImplementedException();
            }

            public Task<List<Post>> GetTweetsFromUser(Guid id)
            {
                throw new NotImplementedException();
            }
        }
    }
}