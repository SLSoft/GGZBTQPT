using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_XM_TransactionMap : EntityTypeConfiguration<T_XM_Transaction>
    {
        public T_XM_TransactionMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

    }
}