using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter_Security_API.Dal.Models
{
    public class UserFollow
    {
        public Guid ID { get; set; }
        public Guid UserId { get; set; }
        public Guid FollowingUserId { get; set; }
        public DateTime FollowDate { get; set; }
        public DateTime? UnfollowDate { get; set;}
        public bool IsFollowing { get; set; }
    }
}
