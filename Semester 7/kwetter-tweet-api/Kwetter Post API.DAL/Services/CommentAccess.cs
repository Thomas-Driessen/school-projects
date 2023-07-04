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
    public class CommentAccess : ICommentAccess
    {
        private readonly KwetterContext _kwetterContext;

        public CommentAccess(KwetterContext context)
        {
            _kwetterContext = context;
        }

        public async Task<int> CreateComment(Comment comment)
        {
            await _kwetterContext.Comments.AddAsync(comment);
            int result = await _kwetterContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteComment(Comment comment)
        {
            _kwetterContext.Comments.Remove(comment);
            int result = await _kwetterContext.SaveChangesAsync();
            return result;
        }

        public async Task<Comment> GetComment(Guid id)
        {
            return await _kwetterContext.Comments.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Comment>> GetComments()
        {
            return await _kwetterContext.Comments.ToListAsync();
        }

        public async Task<int> UpdateComment(Comment oldComment, Comment newComment)
        {
            Comment foundComment = await _kwetterContext.Comments.FindAsync(oldComment);
            foundComment = newComment;
            _kwetterContext.Comments.Update(foundComment);
            int result = await _kwetterContext.SaveChangesAsync();
            return result;
        }
    }
}
