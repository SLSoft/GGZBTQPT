using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_JG_LinkmanMap : EntityTypeConfiguration<T_JG_Linkman>
    {
        public T_JG_LinkmanMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(20);

            this.Property(t => t.Business)
                .HasMaxLength(200);

            this.Property(t => t.Phone)
                .HasMaxLength(30);

            this.Property(t => t.Mobile)
                .HasMaxLength(30);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.Remark)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("T_JG_Linkman");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.AgencyID).HasColumnName("AgencyID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Business).HasColumnName("Business");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
            this.Property(t => t.OP).HasColumnName("OP");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");

            this.HasRequired(t => t.Agency)
             .WithMany(s => s.Linkmans)
             .HasForeignKey(t => t.AgencyID);
        }
    }
}
