using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_QY_PersonMap : EntityTypeConfiguration<T_QY_Person>
    {
        public T_QY_PersonMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
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



 
        }
    }
}
