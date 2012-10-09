using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_QY_ProductsMap : EntityTypeConfiguration<T_QY_Products>
    {
        public T_QY_ProductsMap()
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

            this.Property(t => t.ProductName)
                .HasMaxLength(100);

            this.Property(t => t.IsPublic)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.OP)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("T_QY_Products");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.CorpCode).HasColumnName("CorpCode");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.SalesTotal).HasColumnName("SalesTotal");
            this.Property(t => t.cpjs).HasColumnName("cpjs");
            this.Property(t => t.IsPublic).HasColumnName("IsPublic");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
            this.Property(t => t.OP).HasColumnName("OP");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
        }
    }
}
