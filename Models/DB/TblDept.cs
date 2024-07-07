namespace API_ERP.Models.DB
{
    public class TblDept
    {
        public int DeptID { get; set; }
        public string DeptName { get; set; }
        public bool Enabled { get; set; }
        public int Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public int Updator { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
