using Microsoft.AspNetCore.Components.Forms;

namespace Kwetter_Front_end_WASM.Shared.Models;

public class CreateTweet
{
    public Post Post { get; set; }
    public MultipartFormDataContent Files { get; set; }
}