using NuGet.Packaging.Signing;

namespace WebApiAssignemnt.Dto
{
    public class RespGetMessageHistoryDto
    {
        public int id { get; set; }
        public UserDetail? UserDetail { get; set; }
        public int senderId{get;set;}
        public int receiverId { get;set;}

        public string messageContent { get; set; } = string.Empty;
        public DateTime messageTimestamp { get; set; }

     

    }
}
