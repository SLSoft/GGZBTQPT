using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_HY_FavoriteMap : EntityTypeConfiguration<T_HY_Favorite>
    {
        public T_HY_FavoriteMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            this.HasMany(t => t.Members)
                .WithMany(t => t.Favorites)
                .Map(m =>
                {
                    m.ToTable("T_HY_MemberT_HY_Favorite");
                    m.MapLeftKey("T_HY_Favorite_ID");
                    m.MapRightKey("T_HY_Member_ID");
                });

            // Foreign Key
            this.HasRequired(t => t.Financial)
              .WithMany(s => s.Favoites)
              .HasForeignKey(t => t.FavoriteID);
        }
    }
}
