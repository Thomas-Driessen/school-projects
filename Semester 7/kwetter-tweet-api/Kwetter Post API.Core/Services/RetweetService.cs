using Kwetter_Post_API.Core.Interfaces;
using Kwetter_Post_API.DAL.Interfaces;
using Kwetter_Post_API.DAL.Models;
using Kwetter_Post_API.DAL.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static MassTransit.MessageHeaders;

namespace Kwetter_Post_API.Core.Services
{
    public class RetweetService : IRetweetService
    {
        private readonly IRetweetAccess _retweetAccess;
        private readonly IPublishEndpoint _publishEndpoint;

        public RetweetService(IRetweetAccess retweetAccess, IPublishEndpoint publishEndpoint)
        {
            _retweetAccess = retweetAccess;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<int> CreateRetweet(ClaimsPrincipal user, Retweet retweet)
        {
            retweet.UserId = new Guid(user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            retweet.RetweetedAt = DateTime.UtcNow;
            retweet.IsDeleted = false;
            return await _retweetAccess.CreateRetweet(retweet);
        }

        public async Task<int> DeleteRetweet(Retweet retweet)
        {
            Retweet newRetweet = retweet;
            newRetweet.DeletedRetweetAt = DateTime.UtcNow;
            newRetweet.IsDeleted = true;
            return await _retweetAccess.UpdateRetweet(retweet, newRetweet);
        }

        public async Task<Retweet> GetRetweet(Guid id)
        {
            return await _retweetAccess.GetRetweet(id);
        }

        public async Task<List<Retweet>> GetRetweets()
        {
            return await _retweetAccess.GetRetweets();
        }

        public async Task<int> UpdateRetweet(Retweet oldRetweet, Retweet newRetweet)
        {
            return await _retweetAccess.UpdateRetweet(oldRetweet, newRetweet);
        }
    }
}
