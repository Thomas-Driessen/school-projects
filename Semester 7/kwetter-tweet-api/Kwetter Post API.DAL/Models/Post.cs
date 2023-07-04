using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kwetter_Post_API.DAL.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [NotMapped]
        [FromForm]
        public virtual IFormFile MediaFile { get; set; } 
        public Uri? MediaImage { get; set; }
        public DateTime CreatedDate { get; set; }
        // User info
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string UserThumbnail { get; set; }
        // Comments
        public List<Comment> Comments { get; set; }
        // Retweets
        public List<Retweet> Retweets { get; set; } 
    }
}