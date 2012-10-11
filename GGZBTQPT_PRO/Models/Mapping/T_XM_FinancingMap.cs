using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_XM_FinancingMap : EntityTypeConfiguration<T_XM_Financing>
    {
        public T_XM_FinancingMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ItemName)
                .HasMaxLength(200);

            this.Property(t => t.Keys)
                .HasMaxLength(200);

            this.Property(t => t.Assure)
                .HasMaxLength(500);

            this.Property(t => t.Collateral)
                .HasMaxLength(200);

            this.Property(t => t.PartnerRequirements)
                .HasMaxLength(1000);

            this.Property(t => t.IsMortgage)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PublicStatus)
                .IsFixedLength()
                .HasMaxLength(1);

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

            // Table & Column Mappings
     
        }
    }
}
