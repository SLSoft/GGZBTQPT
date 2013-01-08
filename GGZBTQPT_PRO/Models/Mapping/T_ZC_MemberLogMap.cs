using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_ZC_MemberLogMap : EntityTypeConfiguration<T_ZC_MemberLog>
    {

        public T_ZC_MemberLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            this.HasRequired(t => t.Member)
                .WithMany(s => s.MemberLogs)
                .HasForeignKey(t => t.MemberID);
        }
    }
}