using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_XM_InvestmentMap : EntityTypeConfiguration<T_XM_Investment>
    {
        public T_XM_InvestmentMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ItemName)
                .HasMaxLength(200);

            this.Property(t => t.Keys)
                .HasMaxLength(200);

            this.Property(t => t.Linkman)
                .HasMaxLength(20);

            this.Property(t => t.Position)
                .HasMaxLength(50);

            this.Property(t => t.Phone)
                .HasMaxLength(30);

            this.Property(t => t.Mobile)
                .HasMaxLength(30);

            this.Property(t => t.Fax)
                .HasMaxLength(30);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.QQ)
                .HasMaxLength(20);

            this.Property(t => t.IsPublic)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.AimIndustry)
                .HasMaxLength(1000);

            this.Property(t => t.AjmArea)
                .HasMaxLength(1000);

            this.Property(t => t.InvestmentPeriod)
                .HasMaxLength(20);

            this.Property(t => t.PartnerRequirements)
                .HasMaxLength(1000);

            // Table & Column Mappings

        }
    }
}
