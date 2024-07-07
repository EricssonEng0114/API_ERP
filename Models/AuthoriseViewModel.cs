namespace API_ERP.Models
{
    public class AuthoriseViewModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_sec { get; set; }
    }
}
