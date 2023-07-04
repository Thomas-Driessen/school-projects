using Kwetter_Post_API.DAL.Context;
using Kwetter_Post_API.DAL.Interfaces;
using Kwetter_Post_API.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter_Post_API.DAL.Services
{
    public class RetweetAccess : IRetweetAccess
    {
        private readonly KwetterContext _kwetterContext;

        public RetweetAccess(KwetterContext context)
        {
            _kwetterContext = context;
        }

        public async Task<int> CreateRetweet(Retweet retweet)
        {
            await _kwetterContext.Retweets.AddAsync(retweet);
            int result = await _kwetterContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteRetweet(Retweet retweet)
        {
            _kwetterContext.Retweets.Remove(retweet);
            int result = await _kwetterContext.SaveChangesAsync();
            return result;
        }

        public async Task<Retweet> GetRetweet(Guid id)
        {
            return await _kwetterContext.Retweets.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Retweet>> GetRetweets()
        {
            return await _kwetterContext.Retweets.ToListAsync();
        }

        public async Task<int> UpdateRetweet(Retweet oldRetweet, Retweet newRetweet)
        {
            Retweet foundRetweet = await _kwetterContext.Retweets.FindAsync(oldRetweet);
            foundRetweet = newRetweet;
            _kwetterContext.Retweets.Update(foundRetweet);
            int result = await _kwetterContext.SaveChangesAsync();
            return result;
        }
    }
}
