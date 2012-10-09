using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_QY_CorpInfoMap : EntityTypeConfiguration<T_QY_CorpInfo>
    {
        public T_QY_CorpInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.Code);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.UserID)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.CorpName)
                .HasMaxLength(200);

            this.Property(t => t.CorpCode)
                .HasMaxLength(20);

            this.Property(t => t.LegalPerson)
                .HasMaxLength(20);

            this.Property(t => t.Website)
                .HasMaxLength(100);

            this.Property(t => t.Range)
                .HasMaxLength(1000);

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
            this.ToTable("T_QY_CorpInfo");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.CorpName).HasColumnName("CorpName");
            this.Property(t => t.CorpCode).HasColumnName("CorpCode");
            this.Property(t => t.RegTime).HasColumnName("RegTime");
            this.Property(t => t.Property).HasColumnName("Property");
            this.Property(t => t.Province).HasColumnName("Province");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.Region).HasColumnName("Region");
            this.Property(t => t.RegCapital).HasColumnName("RegCapital");
            this.Property(t => t.Industry).HasColumnName("Industry");
            this.Property(t => t.Stage).HasColumnName("Stage");
            this.Property(t => t.LegalPerson).HasColumnName("LegalPerson");
            this.Property(t => t.Employees).HasColumnName("Employees");
            this.Property(t => t.Website).HasColumnName("Website");
            this.Property(t => t.Logo).HasColumnName("Logo");
            this.Property(t => t.Range).HasColumnName("Range");
            this.Property(t => t.Remark).HasColumnName("Remark");
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
