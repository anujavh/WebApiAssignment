using System.ComponentModel;

namespace WebApiAssignemnt.Dto
{
    public class ReqGetMessageHistoryDto
    {
        public int userId { get; set; }
       
        public DateTime beforeTime{ get; set; } = DateTime.Now; //before : All messages before this timestamp should be returned from API 
        
        public int count { get; set; } = 20; 
        public int sort { get; set; } = 1; //1=asc, 2=desc


    }
}
