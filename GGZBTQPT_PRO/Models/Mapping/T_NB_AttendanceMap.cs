using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_NB_AttendanceMap:EntityTypeConfiguration<T_NB_Attendance>
    {
        public T_NB_AttendanceMap()
        {
            this.HasKey(t => t.ID);

            this.HasRequired(t => t.User)
                .WithMany(s => s.Attendances)
                .HasForeignKey(t => t.UserId);

        }
    }
}