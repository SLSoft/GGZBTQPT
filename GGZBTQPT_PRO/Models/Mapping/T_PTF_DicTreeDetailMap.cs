using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_PTF_DicTreeDetailMap : EntityTypeConfiguration<T_PTF_DicTreeDetail>
    {
        public T_PTF_DicTreeDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.DicType)
                .IsFixedLength()
                .HasMaxLength(4);

            this.Property(t => t.Code)
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .HasMaxLength(100);

            this.Property(t => t.SubName)
                .HasMaxLength(50);

            this.Property(t => t.ParentCode)
                .HasMaxLength(50);

            this.Property(t => t.IsValid)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.UpdTime)
                .HasMaxLength(20);

            this.Property(t => t.UpdUserCode)
                .HasMaxLength(20);

            this.Property(t => t.UpdPgm)
                .HasMaxLength(8);

            this.Property(t => t.Remark)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("T_PTF_DicTreeDetail");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.DicType).HasColumnName("DicType");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.SubName).HasColumnName("SubName");
            this.Property(t => t.Taxis).HasColumnName("Taxis");
            this.Property(t => t.Depth).HasColumnName("Depth");
            this.Property(t => t.ParentCode).HasColumnName("ParentCode");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
            this.Property(t => t.UpdTime).HasColumnName("UpdTime");
            this.Property(t => t.UpdUserCode).HasColumnName("UpdUserCode");
            this.Property(t => t.UpdPgm).HasColumnName("UpdPgm");
            this.Property(t => t.Remark).HasColumnName("Remark");
        }
    }
}
