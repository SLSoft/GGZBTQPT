using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
  public class T_ZC_UserMap : EntityTypeConfiguration<T_ZC_User>
  {
    public T_ZC_UserMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.IdCard)
                .IsRequired();

            this.Property(t => t.CellPhone)
                .IsRequired();
 
            // Foreign Key
            this.HasRequired(t => t.Department)
              .WithMany(d => d.Users)
              .HasForeignKey(t => t.DepartmentID)
              .WillCascadeOnDelete(false);
            // Table & Column Mappings
        }
  }
}