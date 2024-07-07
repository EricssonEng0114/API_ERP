namespace API_ERP.Models.DB
{
    public class TblState
    {
        public byte StateID { get; set; }
        public string Code { get; set; }
        public string StateName { get; set; }
        public int CountryID { get; set; }
        public int Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public int Updator { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
