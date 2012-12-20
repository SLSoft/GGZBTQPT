using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_HY_MessageMap : EntityTypeConfiguration<T_HY_Message>
    {
        public T_HY_MessageMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            this.HasRequired(t => t.SendMember)
             .WithMany(s => s.SendedMessages)
             .HasForeignKey(t => t.SendMemberID); 

            this.HasRequired(t => t.ReceiveMember)
             .WithMany(s => s.ReceivedMessages)
             .HasForeignKey(t => t.ReceiveMemberID);
        }
    }
}