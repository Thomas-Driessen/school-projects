namespace Kwetter_Post_API.DAL.Models;

public class Comment
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Post Post { get; set; }
    public DateTime CommentPlacedAt { get; set; }
    public DateTime? CommentDeletedAt { get; set; }
    public bool IsDeleted { get; set; }
}