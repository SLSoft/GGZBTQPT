using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_ZC_RoleMap : EntityTypeConfiguration<T_ZC_Role>
    {
        public T_ZC_RoleMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.HasMany(r => r.Users)
                .WithMany(u => u.Roles);

            this.HasMany(r => r.Menus)
                .WithMany(m => m.Roles); 
        }
    }
}