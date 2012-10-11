using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_HY_MemberMap : EntityTypeConfiguration<T_HY_Member>
    {
        public T_HY_MemberMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            this.Property(t => t.CellPhone)
                .IsRequired();

            this.Property(t => t.LoginName)
                .IsRequired().HasMaxLength(20);

            this.Property(t => t.Password)
                .IsRequired().HasMaxLength(12);

       
            // Table & Column Mappings

        }
    }
}