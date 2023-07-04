using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter_Post_API.DAL.Models
{
    public class Retweet
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Post Post { get; set; }
        public DateTime RetweetedAt { get; set; }
        public DateTime? DeletedRetweetAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
