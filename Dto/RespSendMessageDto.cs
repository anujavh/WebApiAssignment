using NuGet.Packaging.Signing;

namespace WebApiAssignemnt.Dto
{
    public class RespSendMessageDto
    {
        public int MessageId { get; set; }
        public int senderId { get; set; }
        public int receiverId { get; set; }
        public string MessageContent { get; set; }
        public DateTime MessageTimestamp { get; set; }
    }
}
