using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_JG_ProductMap : EntityTypeConfiguration<T_JG_Product>
    {
        public T_JG_ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ProductName)
                .HasMaxLength(200);

            this.Property(t => t.Superiority)
                .HasMaxLength(500);

            this.Property(t => t.RepaymentType)
                .HasMaxLength(500);

            this.Property(t => t.AppCondition)
                .HasMaxLength(500);

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


        }
    }
}
