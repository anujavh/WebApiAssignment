using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAssignemnt.Models
{
    [Table("MessageDetails")]
    [PrimaryKey("MessageId")]
    public class MessageDetails
    {
        public int MessageId { get; set; }
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public string MessageContent { get; set; }
        public DateTime MessageTimestamp { get; set; }

    }
}
