using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_PTF_DicTypeMap : EntityTypeConfiguration<T_PTF_DicType>
    {
        public T_PTF_DicTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.DicType)
                .IsFixedLength()
                .HasMaxLength(4);

            this.Property(t => t.DicName)
                .HasMaxLength(100);

            this.Property(t => t.DicTableName)
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
            this.ToTable("T_PTF_DicType");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.DicType).HasColumnName("DicType");
            this.Property(t => t.DicName).HasColumnName("DicName");
            this.Property(t => t.DicTableName).HasColumnName("DicTableName");
            this.Property(t => t.SystemType).HasColumnName("SystemType");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
            this.Property(t => t.UpdTime).HasColumnName("UpdTime");
            this.Property(t => t.UpdUserCode).HasColumnName("UpdUserCode");
            this.Property(t => t.UpdPgm).HasColumnName("UpdPgm");
            this.Property(t => t.Remark).HasColumnName("Remark");
        }
    }
}
