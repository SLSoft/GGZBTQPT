using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_HY_MemberMap : EntityTypeConfiguration<T_HY_Member>
    {
        public T_HY_MemberMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            this.Property(t => t.CellPhone)
                .IsRequired();


            // Foreign Key
            this.HasRequired(t => t.Agency)
              .WithMany()
              .HasForeignKey(a => a.AgencyID)
              .WillCascadeOnDelete(true);

            this.HasRequired(t => t.Corp)
              .WithMany()
              .HasForeignKey(a => a.CorpID)
              .WillCascadeOnDelete(true); 

            this.HasRequired(t => t.Person)
              .WithMany()
              .HasForeignKey(a => a.PersonID)
              .WillCascadeOnDelete(true);
            // Table & Column Mappings

        }
    }
}