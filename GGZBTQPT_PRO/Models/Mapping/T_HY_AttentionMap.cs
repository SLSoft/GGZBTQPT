using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_HY_AttentionMap : EntityTypeConfiguration<T_HY_Attention>
    {
        public T_HY_AttentionMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            this.HasMany(t => t.Members)
                .WithMany(t => t.Attentions)
                .Map(m =>
                {
                    m.ToTable("T_HY_MemberT_HY_Attention");
                    m.MapLeftKey("T_HY_Attention_ID");
                    m.MapRightKey("T_HY_Member_ID");
                });




        }
    }
}
