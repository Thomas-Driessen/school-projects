using Kwetter_Post_API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter_Post_API.DAL.Interfaces
{
    public interface IRetweetAccess
    {
        public Task<List<Retweet>> GetRetweets();
        public Task<Retweet> GetRetweet(Guid id);
        public Task<int> CreateRetweet(Retweet Retweet);
        public Task<int> UpdateRetweet(Retweet oldRetweet, Retweet newRetweet);
        public Task<int> DeleteRetweet(Retweet Retweet);
    }
}
