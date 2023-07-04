using Kwetter_Post_API.Core.Interfaces;
using Kwetter_Post_API.DAL.Interfaces;
using Kwetter_Post_API.DAL.Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter_Post_API.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentAccess _commentAccess;
        private readonly IPublishEndpoint _publishEndpoint;

        public CommentService(ICommentAccess commentAccess, IPublishEndpoint publishEndpoint)
        {
            _commentAccess = commentAccess;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<int> CreateComment(ClaimsPrincipal user, Comment comment)
        {
            comment.UserId = new Guid(user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            comment.CommentPlacedAt = DateTime.UtcNow;
            comment.IsDeleted = false;
            return await _commentAccess.CreateComment(comment);
        }

        public async Task<int> DeleteComment(Comment comment)
        {
            Comment newComment = comment;
            newComment.CommentDeletedAt = DateTime.UtcNow;
            newComment.IsDeleted = true;
            return await _commentAccess.UpdateComment(comment, newComment);
        }

        public async Task<Comment> GetComment(Guid id)
        {
            return await _commentAccess.GetComment(id);
        }

        public async Task<List<Comment>> GetComments()
        {
            return await _commentAccess.GetComments();
        }

        public async Task<int> UpdateComment(Comment oldComment, Comment newComment)
        {
            return await _commentAccess.UpdateComment(oldComment, newComment);
        }
    }
}
