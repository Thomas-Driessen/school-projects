using Kwetter_Post_API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter_Post_API.Core.Interfaces
{
    public interface IRetweetService
    {
        public Task<List<Retweet>> GetRetweets();
        public Task<Retweet> GetRetweet(Guid id);
        public Task<int> CreateRetweet(ClaimsPrincipal user, Retweet Retweet);
        public Task<int> UpdateRetweet(Retweet oldRetweet, Retweet newRetweet);
        public Task<int> DeleteRetweet(Retweet Retweet);
    }
}
