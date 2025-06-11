using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImmigrationAI.Models
{
    public class User
    {   
        [Key]
        public Guid UserId { get; set; }

        [Required]
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        public ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();
    }

    public class Conversation
    {
        [Required]
        public Guid UserId { get; set; }

        [Key]
        public Guid ConversationID { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public User? User { get; set; }

        public ICollection<Message> Messages { get; set; } = new List<Message>();

    }
    public class Message
    {
        [Key]
        public Guid MessageID { get; set; }

        [Required]
        public Guid ConversationID { get; set; }

        [Required]
        public required string Sender { get; set; } = string.Empty;

        [Required]
        public required string Content { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; } = DateTime.Now;

        public Conversation? Conversation { get; set; }
    }
}
