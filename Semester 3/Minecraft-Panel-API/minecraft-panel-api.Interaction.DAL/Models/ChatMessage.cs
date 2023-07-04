using System;

namespace minecraft_panel_api.Interaction.DAL.Models
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public string SenderName { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}