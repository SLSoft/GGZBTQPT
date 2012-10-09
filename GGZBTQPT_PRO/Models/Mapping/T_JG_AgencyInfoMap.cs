using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_JG_AgencyInfoMap : EntityTypeConfiguration<T_JG_AgencyInfo>
    {
        public T_JG_AgencyInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.Code);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.UserCode)
                .HasMaxLength(64);

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

            this.Property(t => t.OP)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("T_JG_AgencyInfo");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.UserCode).HasColumnName("UserCode");
            this.Property(t => t.AgencyName).HasColumnName("AgencyName");
            this.Property(t => t.AgencyType).HasColumnName("AgencyType");
            this.Property(t => t.RegTime).HasColumnName("RegTime");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.RegCapital).HasColumnName("RegCapital");
            this.Property(t => t.Province).HasColumnName("Province");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.Region).HasColumnName("Region");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Services).HasColumnName("Services");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
            this.Property(t => t.OP).HasColumnName("OP");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
        }
    }
}
