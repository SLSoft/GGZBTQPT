using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_JG_AgencyMap : EntityTypeConfiguration<T_JG_Agency>
    {
        public T_JG_AgencyMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.AgencyName)
                .HasMaxLength(100);

            this.Property(t => t.Address)
                .HasMaxLength(100);

            this.Property(t => t.Phone)
                .HasMaxLength(30);

            this.Property(t => t.Services)
                .HasMaxLength(1000);

            this.Property(t => t.Remark)
                .HasMaxLength(2000);

            // Table & Column Mappings

        }
    }
}
