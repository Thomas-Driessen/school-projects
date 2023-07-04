using Kwetter_Post_API.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter_Post_API.Core.Interfaces
{
    public interface ICommentService
    {
        public Task<List<Comment>> GetComments();
        public Task<Comment> GetComment(Guid id);
        public Task<int> CreateComment(ClaimsPrincipal user, Comment comment);
        public Task<int> UpdateComment(Comment oldComment, Comment newComment);
        public Task<int> DeleteComment(Comment comment);
    }
}
