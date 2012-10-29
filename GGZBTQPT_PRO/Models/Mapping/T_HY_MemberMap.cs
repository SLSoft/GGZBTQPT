using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_HY_MemberMap : EntityTypeConfiguration<T_HY_Member>
    {
        public T_HY_MemberMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


            this.Property(t => t.CellPhone)
                .IsRequired();


            this.Property(t => t.LoginName)
                .IsRequired().HasMaxLength(20);

            this.Property(t => t.Password)
                .IsRequired().HasMaxLength(12);

            //this.HasMany(t => t.Attentions)
            //    .WithMany(t => t.Members)
            //    .Map(m =>
            //    {
            //        m.ToTable("T_HY_MemberT_HY_Attention");
            //        m.MapLeftKey("T_HY_Member_ID");
            //        m.MapRightKey("T_HY_Attention_ID");
            //    });

            //this.HasMany(t => t.Favorites)
            //    .WithMany(t => t.Members)
            //    .Map(m =>
            //    {
            //        m.ToTable("T_HY_MemberT_HY_Favorite");
            //        m.MapLeftKey("T_HY_Member_ID");
            //        m.MapRightKey("T_HY_Favorite_ID");
            //    });


        }
    }
}