namespace API_ERP.Models.DB
{
    public class TblUserAddress
    {
        public int UID { get; set; }
        public string MailAdd1 { get; set; }
        public string MailAdd2 { get; set; }
        public string MailAdd3 { get; set; }
        public string MailPostcode { get; set; }
        public byte MailState { get; set; }
        public short MailArea { get; set; }
        public int MailCountry { get; set; }
        public string ResAdd1 { get; set; }
        public string ResAdd2 { get; set; }
        public string ResAdd3 { get; set; }
        public string ResPostcode { get; set; }
        public byte ResState { get; set; }
        public short ResArea { get; set; }
        public int ResCountry { get; set; }
        public int Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public int Updator { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
