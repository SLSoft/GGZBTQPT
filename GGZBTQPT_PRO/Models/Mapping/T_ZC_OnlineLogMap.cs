using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_ZC_OnlineLogMap : EntityTypeConfiguration<T_ZC_OnlineLog>
    {

        public T_ZC_OnlineLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            this.HasRequired(t => t.Member)
                .WithMany(s => s.OnlineLogs)
                .HasForeignKey(t => t.MemberID);
        }
    }
}