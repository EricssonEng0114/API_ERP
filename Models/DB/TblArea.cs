namespace API_ERP.Models.DB
{
    public class TblArea
    {
        public short AreaID { get; set; }
        public string AreaName { get; set; }
        public int StateID { get; set; }
        public bool Enabled { get; set; }
        public int Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public int Updator { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
