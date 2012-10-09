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
        public DbSet<T_JG_AgencyInfo> T_JG_AgencyInfo { get; set; }
        public DbSet<T_JG_Linkman> T_JG_Linkman { get; set; }
        public DbSet<T_JG_ProductInfo> T_JG_ProductInfo { get; set; }
        public DbSet<T_QY_CorpInfo> T_QY_CorpInfo { get; set; }
        public DbSet<T_QY_Financial> T_QY_Financial { get; set; }
        public DbSet<T_QY_PersonInfo> T_QY_PersonInfo { get; set; }
        public DbSet<T_QY_Products> T_QY_Products { get; set; }
        public DbSet<T_XM_Financing> T_XM_Financing { get; set; }
        public DbSet<T_XM_Investment> T_XM_Investment { get; set; }
        public DbSet<T_ZC_User> T_ZC_User { get; set; }
        public DbSet<T_ZC_System> T_ZC_System { get; set; }
        public DbSet<T_ZC_Menu> T_ZC_Menu { get; set; }
        public DbSet<T_ZC_Department> T_ZC_Department { get; set; }
        public DbSet<T_ZC_Role> T_ZC_Role { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new T_HY_AttentionMap());
            modelBuilder.Configurations.Add(new T_HY_FavoriteMap());
            modelBuilder.Configurations.Add(new T_JG_AgencyInfoMap());
            modelBuilder.Configurations.Add(new T_JG_LinkmanMap());
            modelBuilder.Configurations.Add(new T_JG_ProductInfoMap());
            modelBuilder.Configurations.Add(new T_QY_CorpInfoMap());
            modelBuilder.Configurations.Add(new T_QY_FinancialMap());
            modelBuilder.Configurations.Add(new T_QY_PersonInfoMap());
            modelBuilder.Configurations.Add(new T_QY_ProductsMap());
            modelBuilder.Configurations.Add(new T_XM_FinancingMap());
            modelBuilder.Configurations.Add(new T_XM_InvestmentMap());
            modelBuilder.Configurations.Add(new T_ZC_UserMap());
            modelBuilder.Configurations.Add(new T_ZC_SystemMap());
            modelBuilder.Configurations.Add(new T_ZC_MenuMap());
            modelBuilder.Configurations.Add(new T_ZC_DepartmentMap());
            modelBuilder.Configurations.Add(new T_ZC_RoleMap());

        }
    }
}
