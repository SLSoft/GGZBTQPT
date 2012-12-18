using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_XM_CaseMap : EntityTypeConfiguration<T_XM_Case>
    {
        public T_XM_CaseMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 

            this.Property(t => t.Name).IsRequired();
            this.Property(t => t.Summary).IsRequired();
            this.Property(t => t.Analysis).IsRequired();
            this.Property(t => t.Type).IsRequired(); 

        }
    }
}