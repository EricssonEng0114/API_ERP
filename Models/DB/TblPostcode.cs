namespace API_ERP.Models.DB
{
    public class TblPostcode
    {
        public short PostcodeID { get; set; }
        public string Postcode { get; set; }
        public int StateID { get; set; }
        public int AreaID { get; set; }
        public bool Enabled { get; set; }
        public int Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public int Updator { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
