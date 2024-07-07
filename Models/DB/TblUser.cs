namespace API_ERP.Models.DB
{
    public class TblUser
    {
        public int UID { get; set; }
        public short TypeID { get; set; }
        public short StatusID { get; set; }
        public byte Lvl { get; set; }
        public string Name { get; set; }
        public string NRIC { get; set; }
        public short Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Nickname { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime ResignDate { get; set; }
        public DateTime TerminateDate { get; set; }
        public DateTime ProbationDate { get; set; }
        public bool IsConfirm { get; set; }
        public DateTime ConfirmDate { get; set; }
        public short DeptID { get; set; }
        public int Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public int Updator { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
