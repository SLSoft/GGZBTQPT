using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
  public class T_ZC_SystemMap : EntityTypeConfiguration<T_ZC_System>
  {
    public T_ZC_SystemMap()
        {
            // Primary Key
          this.HasKey(t => t.ID);


            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //this.Property(t => t.CreatedAt)
            //    .HasColumnType("datetime2");
            //this.Property(t => t.UpdatedAt)
            //    .HasColumnType("datetime2");







            // Table & Column Mappings

        }
  }
}