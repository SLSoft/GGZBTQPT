using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using GGZBTQPT_PRO.Models.Mapping;

namespace GGZBTQPT_PRO.Models
{
    public class GGZBTQPTDBContext : DbContext
    {
        static GGZBTQPTDBContext()
        {
            Database.SetInitializer<GGZBTQPTDBContext>(null);
        }

		public GGZBTQPTDBContext()
			: base("Name=GGZBTQPTDBContext")
		{
		}

        public DbSet<T_HY_Attention> T_HY_Attention { get; set; }
        public DbSet<T_HY_Favorite> T_HY_Favorite { get; set; }
        public DbSet<T_JG_Agency> T_JG_Agency { get; set; }
        public DbSet<T_JG_Linkman> T_JG_Linkman { get; set; }
        public DbSet<T_JG_Product> T_JG_Product { get; set; }
        public DbSet<T_QY_Corp> T_QY_Corp { get; set; }
        public DbSet<T_QY_Financial> T_QY_Financial { get; set; }
        public DbSet<T_QY_Person> T_QY_Person { get; set; }
        public DbSet<T_QY_Product> T_QY_Product { get; set; }
        public DbSet<T_XM_Financing> T_XM_Financing { get; set; }
        public DbSet<T_XM_Investment> T_XM_Investment { get; set; }
        public DbSet<T_XM_Transaction> T_XM_Transaction { get; set; }
        public DbSet<T_XM_Alendar> T_XM_Alendar { get; set; }
        public DbSet<T_ZC_User> T_ZC_User { get; set; }
        public DbSet<T_ZC_System> T_ZC_System { get; set; }
        public DbSet<T_ZC_Menu> T_ZC_Menu { get; set; }
        public DbSet<T_ZC_Department> T_ZC_Department { get; set; }
        public DbSet<T_ZC_Role> T_ZC_Role { get; set; }
        public DbSet<T_PTF_DicDetail> T_PTF_DicDetail { get; set; }
        public DbSet<T_PTF_DicTreeDetail> T_PTF_DicTreeDetail { get; set; }
        public DbSet<T_PTF_DicType> T_PTF_DicType { get; set; }
        public DbSet<T_ZC_CommonLog> T_ZC_CommonLog { get; set; } 
        public DbSet<T_ZC_OnlineLog> T_ZC_OnlineLog { get; set; } 
        public DbSet<T_ZC_MemberLog> T_ZC_MemberLog { get; set; } 
        public DbSet<T_HY_Member> T_HY_Member { get; set; }
        public DbSet<T_XM_Case> T_XM_Case { get; set; }
        public DbSet<T_HY_Message> T_HY_Message { get; set; }
        public DbSet<T_NB_File> T_NB_File { get; set; }
        public DbSet<T_NB_Meeting> T_NB_Meeting { get; set; }
        public DbSet<T_NB_Attendance> T_NB_Attendance { get; set; }

        public DbSet<T_QY_RZXQ> T_QY_RZXQ { get; set; }
        public DbSet<T_QY_TZXQ> T_QY_TZXQ { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new T_HY_AttentionMap());
            modelBuilder.Configurations.Add(new T_HY_FavoriteMap());
            modelBuilder.Configurations.Add(new T_HY_MemberMap());
            modelBuilder.Configurations.Add(new T_JG_AgencyMap());
            modelBuilder.Configurations.Add(new T_JG_LinkmanMap());
            modelBuilder.Configurations.Add(new T_JG_ProductMap());
            modelBuilder.Configurations.Add(new T_QY_CorpMap());
            modelBuilder.Configurations.Add(new T_QY_FinancialMap());
            modelBuilder.Configurations.Add(new T_QY_PersonMap());
            modelBuilder.Configurations.Add(new T_QY_ProductMap());
            modelBuilder.Configurations.Add(new T_XM_FinancingMap());
            modelBuilder.Configurations.Add(new T_XM_InvestmentMap());
            modelBuilder.Configurations.Add(new T_ZC_UserMap());
            modelBuilder.Configurations.Add(new T_ZC_SystemMap());
            modelBuilder.Configurations.Add(new T_ZC_MenuMap());
            modelBuilder.Configurations.Add(new T_ZC_DepartmentMap());
            modelBuilder.Configurations.Add(new T_ZC_RoleMap());
            modelBuilder.Configurations.Add(new T_PTF_DicDetailMap());
            modelBuilder.Configurations.Add(new T_PTF_DicTreeDetailMap());
            modelBuilder.Configurations.Add(new T_PTF_DicTypeMap());
            modelBuilder.Configurations.Add(new T_ZC_OnlineLogMap());
            modelBuilder.Configurations.Add(new T_ZC_MemberLogMap());
            modelBuilder.Configurations.Add(new T_XM_CaseMap());
            modelBuilder.Configurations.Add(new T_HY_MessageMap());           
            modelBuilder.Configurations.Add(new T_NB_FileMap());
            modelBuilder.Configurations.Add(new T_NB_MeetingMap());
            modelBuilder.Configurations.Add(new T_NB_AttendanceMap());
        }

    }
}
