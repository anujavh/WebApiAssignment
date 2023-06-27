using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAssignemnt.Models
{
    [Table("LogRequests")]
    [PrimaryKey("Id")]
    public class LogRequests
    {
        public int Id { get; set; }
        public string IPAddress { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set; }=DateTime.Now;
        public string RequestBody { get; set; }=string.Empty;

    }
}
