using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_NB_MeetingMap : EntityTypeConfiguration<T_NB_Meeting>
    {
        public T_NB_MeetingMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            this.HasRequired(t => t.CreateUser)
                .WithMany(s => s.CreateMeetings)
                .HasForeignKey(t => t.CreateUserId);

            this.HasMany(n => n.PartakeUsers)
                .WithMany(u => u.PartakeMeetings);

        }
    }
}