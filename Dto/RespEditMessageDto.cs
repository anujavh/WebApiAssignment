namespace WebApiAssignemnt.Dto
{
    public class RespEditMessageDto
    {

        public int MessageId { get; set; }
        public int SenderId { get; set; }
         
        public int receiverId { get; set; }
    
        public string MessageContent { get; set; } = string.Empty;

    }
}
