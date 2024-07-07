namespace API_ERP.Models.DB
{
    public class TblUserLogin
    {
        public int UID { get; set; }
        public string UserName { get; set; }
        public string Passwd { get; set; }
        public bool Enabled { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsActivate { get; set; }
        public DateTime ActivateDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime PassLastChg { get; set; }
        public int FailTry { get; set; }
        public int Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public int Updator { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
