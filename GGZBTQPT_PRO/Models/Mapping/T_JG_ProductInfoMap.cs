using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_JG_ProductInfoMap : EntityTypeConfiguration<T_JG_ProductInfo>
    {
        public T_JG_ProductInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.Code);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.ProductName)
                .HasMaxLength(200);

            this.Property(t => t.AgencyID)
                .HasMaxLength(64);

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

            this.Property(t => t.OP)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("T_JG_ProductInfo");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.AgencyID).HasColumnName("AgencyID");
            this.Property(t => t.FinancingAmount).HasColumnName("FinancingAmount");
            this.Property(t => t.FinancingLimit).HasColumnName("FinancingLimit");
            this.Property(t => t.InterestRate).HasColumnName("InterestRate");
            this.Property(t => t.CustomerType).HasColumnName("CustomerType");
            this.Property(t => t.Superiority).HasColumnName("Superiority");
            this.Property(t => t.RepaymentType).HasColumnName("RepaymentType");
            this.Property(t => t.AppCondition).HasColumnName("AppCondition");
            this.Property(t => t.Linkman).HasColumnName("Linkman");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Fax).HasColumnName("Fax");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.QQ).HasColumnName("QQ");
            this.Property(t => t.IsPublic).HasColumnName("IsPublic");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
            this.Property(t => t.OP).HasColumnName("OP");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
        }
    }
}
