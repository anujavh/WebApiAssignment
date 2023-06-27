using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApiAssignemnt.Models
{
    [Table("UserDeatails")]
    [PrimaryKey("Id")]
    public class UserDetail
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [ForeignKey("senderId")]
        public List<MessageDetails> sentMessages { get; set; }

        [ForeignKey("receiverId")]
        public List<MessageDetails> receivedMessages { get; set; }


    }
}
