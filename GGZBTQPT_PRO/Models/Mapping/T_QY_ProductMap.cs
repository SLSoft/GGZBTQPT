using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_QY_ProductMap : EntityTypeConfiguration<T_QY_Product>
    {
        public T_QY_ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ProductName)
                .HasMaxLength(100);

            this.Property(t => t.IsPublic)
                .IsFixedLength()
                .HasMaxLength(1);


        }
    }
}
