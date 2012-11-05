using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_XM_InvestmentMap : EntityTypeConfiguration<T_XM_Investment>
    {
        public T_XM_InvestmentMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ItemName)
                .HasMaxLength(200);

            this.Property(t => t.Keys)
                .HasMaxLength(200);

            this.Property(t => t.Linkman)
                .HasMaxLength(20);

            this.Property(t => t.Position)
                .HasMaxLength(50);

            this.Property(t => t.Phone)
                .HasMaxLength(30);

            this.Property(t => t.Mobile)
                .HasMaxLength(30);

            this.Property(t => t.Fax)
                .HasMaxLength(30);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.QQ)
                .HasMaxLength(20);

            this.Property(t => t.IsPublic)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.AimIndustry)
                .HasMaxLength(1000);

            this.Property(t => t.AjmArea)
                .HasMaxLength(1000);

            this.Property(t => t.InvestmentPeriod)
                .HasMaxLength(20);

            this.Property(t => t.PartnerRequirements)
                .HasMaxLength(1000);

            // Table & Column Mappings



            this.ToTable("T_XM_Investment");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.MemberID).HasColumnName("MemberID");
            this.Property(t => t.ItemName).HasColumnName("ItemName");
            this.Property(t => t.Province).HasColumnName("Province");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.Region).HasColumnName("Region");
            this.Property(t => t.Industry).HasColumnName("Industry");
            this.Property(t => t.ValidDate).HasColumnName("ValidDate");
            this.Property(t => t.Keys).HasColumnName("Keys");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Linkman).HasColumnName("Linkman");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Fax).HasColumnName("Fax");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.QQ).HasColumnName("QQ");
            this.Property(t => t.IsPublic).HasColumnName("IsPublic");
            this.Property(t => t.Investment).HasColumnName("Investment");
            this.Property(t => t.StartInvestment).HasColumnName("StartInvestment");
            this.Property(t => t.AimIndustry).HasColumnName("AimIndustry");
            this.Property(t => t.AjmArea).HasColumnName("AjmArea");
            this.Property(t => t.ReturnRatio).HasColumnName("ReturnRatio");
            this.Property(t => t.TeamworkType).HasColumnName("TeamworkType");
            this.Property(t => t.InvestmentPeriod).HasColumnName("InvestmentPeriod");
            this.Property(t => t.PartnerRequirements).HasColumnName("PartnerRequirements");
            this.Property(t => t.PublicStatus).HasColumnName("PublicStatus");
            this.Property(t => t.SubmitTime).HasColumnName("SubmitTime");
            this.Property(t => t.PublicTime).HasColumnName("PublicTime");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
            this.Property(t => t.OP).HasColumnName("OP");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");

            // Foreign Key
            this.HasRequired(t => t.Member)
              .WithMany(s => s.Investments)
              .HasForeignKey(t => t.MemberID);
        }
    }
}
