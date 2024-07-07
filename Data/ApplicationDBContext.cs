using API_ERP.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace API_ERP.Data
{
    public class ApplicationDBContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, IConfiguration configuration)
           : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString, builder =>
                {
                    builder.EnableRetryOnFailure();
                    //builder.CommandTimeout(300);//300 second
                });
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region T
            modelBuilder.Entity<TblArea>().HasKey(k => new { k.AreaID });
            modelBuilder.Entity<TblCountry>().HasKey(k => new { k.CountryID });
            modelBuilder.Entity<TblDept>().HasKey(k => new { k.DeptID });
            modelBuilder.Entity<TblPostcode>().HasKey(k => new { k.PostcodeID });
            modelBuilder.Entity<TblState>().HasKey(k => new { k.StateID });
            modelBuilder.Entity<TblStatus>().HasKey(k => new { k.StatusID });
            modelBuilder.Entity<TblTypeCode>().HasKey(k => new { k.TypeID });
            modelBuilder.Entity<TblTypeGrpD>().HasKey(k => new { k.GDID });
            modelBuilder.Entity<TblTypeGrpM>().HasKey(k => new { k.GID });
            modelBuilder.Entity<TblUser>().HasKey(k => new { k.UID });
            modelBuilder.Entity<TblUserAddress>().HasKey(k => new { k.UID });
            modelBuilder.Entity<TblUserContact>().HasKey(k => new { k.UID });
            modelBuilder.Entity<TblUserLogin>().HasKey(k => new { k.UID });
            #endregion

            #region W
            
            #endregion
        }

        #region DB Set

        #region T
        public DbSet<TblArea> TblArea { get; set; }
        public DbSet<TblCountry> TblCountry { get; set; }
        public DbSet<TblDept> TblDept { get; set; }
        public DbSet<TblPostcode> TblPostcode { get; set; }
        public DbSet<TblState> TblState { get; set; }
        public DbSet<TblStatus> TblStatus { get; set; }
        public DbSet<TblTypeCode> TblTypeCode { get; set; }
        public DbSet<TblTypeGrpD> TblTypeGrpD { get; set; }
        public DbSet<TblTypeGrpM> TblTypeGrpM { get; set; }
        public DbSet<TblUser> TblUser { get; set; }
        public DbSet<TblUserAddress> TblUserAddress { get; set; }
        public DbSet<TblUserContact> TblUserContact { get; set; }
        public DbSet<TblUserLogin> TblUserLogin { get; set; }
        #endregion

        #region W
        public DbSet<WebAPILoginKey> WebAPILoginKey { get; set; }
        #endregion

        #endregion
    }
}
