using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_QY_PersonInfoMap : EntityTypeConfiguration<T_QY_PersonInfo>
    {
        public T_QY_PersonInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.Code);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UserCode)
                .HasMaxLength(64);

            this.Property(t => t.Name)
                .HasMaxLength(20);

            this.Property(t => t.HomeTown)
                .HasMaxLength(20);

            this.Property(t => t.CardID)
                .HasMaxLength(30);

            this.Property(t => t.Gender)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.College)
                .HasMaxLength(100);

            this.Property(t => t.Specialty)
                .HasMaxLength(100);

            this.Property(t => t.Address)
                .HasMaxLength(100);

            this.Property(t => t.Phone)
                .HasMaxLength(30);

            this.Property(t => t.Mobile)
                .HasMaxLength(30);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.MSN)
                .HasMaxLength(30);

            this.Property(t => t.QQ)
                .HasMaxLength(20);

            this.Property(t => t.EduExperience)
                .HasMaxLength(2000);

            this.Property(t => t.WorkExperience)
                .HasMaxLength(2000);

            this.Property(t => t.Remark)
                .HasMaxLength(2000);

            this.Property(t => t.OP)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("T_QY_PersonInfo");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.UserCode).HasColumnName("UserCode");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.HomeTown).HasColumnName("HomeTown");
            this.Property(t => t.CardType).HasColumnName("CardType");
            this.Property(t => t.CardID).HasColumnName("CardID");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Birth).HasColumnName("Birth");
            this.Property(t => t.College).HasColumnName("College");
            this.Property(t => t.Specialty).HasColumnName("Specialty");
            this.Property(t => t.Education).HasColumnName("Education");
            this.Property(t => t.Degree).HasColumnName("Degree");
            this.Property(t => t.Titles).HasColumnName("Titles");
            this.Property(t => t.TitlesGrade).HasColumnName("TitlesGrade");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.MSN).HasColumnName("MSN");
            this.Property(t => t.QQ).HasColumnName("QQ");
            this.Property(t => t.EduExperience).HasColumnName("EduExperience");
            this.Property(t => t.WorkExperience).HasColumnName("WorkExperience");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
            this.Property(t => t.OP).HasColumnName("OP");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");
        }
    }
}
