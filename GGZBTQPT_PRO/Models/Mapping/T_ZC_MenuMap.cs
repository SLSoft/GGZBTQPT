using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
  public class T_ZC_MenuMap : EntityTypeConfiguration<T_ZC_Menu>
  {
    public T_ZC_MenuMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            // Foreign Key
            this.HasRequired(t => t.System)
              .WithMany(s => s.Menus)
              .HasForeignKey(t => t.SystemID);

            this.Property(t => t.ParentID).IsRequired();
            this.Property(t => t.SystemID).IsRequired();

            // Table & Column Mappings

        }
  }
}