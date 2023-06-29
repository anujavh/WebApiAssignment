using NuGet.Packaging.Signing;

namespace WebApiAssignemnt.Dto
{
    public class RespGetMessageHistoryDto
    {
        public int MessageId { get; set; }
        public UserDetail? UserDetail { get; set; }
        public int senderId{get;set;}
        public int receiverId { get;set;}

        public string MessageContent { get; set; } = string.Empty;
        public DateTime MessageTimestamp { get; set; }

     

    }
}
