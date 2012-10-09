using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_QY_FinancialMap : EntityTypeConfiguration<T_QY_Financial>
    {
        public T_QY_FinancialMap()
        {
            // Primary Key
            this.HasKey(t => t.Code);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.CorpCode)
                .HasMaxLength(64);

            this.Property(t => t.CurYear)
                .HasMaxLength(10);

            this.Property(t => t.IsPublic)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.OP)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("T_QY_Financial");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.CorpCode).HasColumnName("CorpCode");
            this.Property(t => t.TotalAssets).HasColumnName("TotalAssets");
            this.Property(t => t.CurYear).HasColumnName("CurYear");
            this.Property(t => t.Revenue).HasColumnName("Revenue");
            this.Property(t => t.IsPublic).HasColumnName("IsPublic");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
            this.Property(t => t.OP).HasColumnName("OP");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
        }
    }
}
