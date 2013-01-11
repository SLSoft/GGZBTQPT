using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_XM_AlendarMap :  EntityTypeConfiguration<T_XM_Alendar>
    {
        public T_XM_AlendarMap()
        {
            this.HasKey(t => t.ID);
            this.Property(t => t.month)
                .HasMaxLength(20);

            this.Property(t => t.month).HasColumnName("month");
        }
    }
}