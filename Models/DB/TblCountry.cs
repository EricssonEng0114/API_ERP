namespace API_ERP.Models.DB
{
    public class TblCountry
    {
        public int CountryID {  get; set; }
        public string Code { get; set; }
        public string CountryName { get; set; }
        public bool Enabled { get; set; }
        public string Currency { get; set; }
        public int Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public int Updator { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
