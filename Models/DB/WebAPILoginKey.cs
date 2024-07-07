using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_ERP.Models.DB
{
    public class WebAPILoginKey
    {
        [Key]
        public int APICode { get; set; }
        public string ApiAppName { get; set; }
        public string ApiKeyOwner { get; set; }
        public bool KeyStatus { get; set; }
        public string ApiKey { get; set; }
        public DateTime CreateDate { get; set; }

        [NotMapped]
        public string Username { get; set; }
        [NotMapped]
        public string Password { get; set; }
    }
}
