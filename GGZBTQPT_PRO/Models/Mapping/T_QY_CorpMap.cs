using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_QY_CorpMap : EntityTypeConfiguration<T_QY_Corp>
    {
        public T_QY_CorpMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
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


        }
    }
}
