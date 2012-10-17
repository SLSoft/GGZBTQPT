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




        }
    }
}
