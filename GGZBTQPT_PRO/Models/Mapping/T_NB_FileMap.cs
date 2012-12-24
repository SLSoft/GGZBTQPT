using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_NB_FileMap : EntityTypeConfiguration<T_NB_File>
    {
        public T_NB_FileMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            this.HasRequired(t => t.SendUser)
                .WithMany(s => s.SendFiles)
                .HasForeignKey(t => t.SendUserId);

            this.HasMany(n => n.ReceiveUsers)
                .WithMany(u => u.ReceiveFiles);

        }
    }
}