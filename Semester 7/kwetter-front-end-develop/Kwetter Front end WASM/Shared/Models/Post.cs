using Microsoft.AspNetCore.Components.Forms;

namespace Kwetter_Front_end_WASM.Shared.Models;

public class Post
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public virtual IBrowserFile MediaFile { get; set; }
    public Uri? MediaImage { get; set; }
    public DateTime CreatedDate { get; set; }
    // User info
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string UserThumbnail { get; set; }
}