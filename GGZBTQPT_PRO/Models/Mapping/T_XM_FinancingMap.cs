using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GGZBTQPT_PRO.Models.Mapping
{
    public class T_XM_FinancingMap : EntityTypeConfiguration<T_XM_Financing>
    {
        public T_XM_FinancingMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ItemName)
                .HasMaxLength(200);

            this.Property(t => t.Keys)
                .HasMaxLength(200);

            this.Property(t => t.Assure)
                .HasMaxLength(500);

            this.Property(t => t.Collateral)
                .HasMaxLength(200);

            this.Property(t => t.PartnerRequirements)
                .HasMaxLength(1000);

            this.Property(t => t.IsMortgage)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.PublicStatus)
                .IsFixedLength()
                .HasMaxLength(1);

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

            // Table & Column Mappings

            this.ToTable("T_XM_Financing");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.MemberID).HasColumnName("MemberID");
            this.Property(t => t.ItemName).HasColumnName("ItemName");
            this.Property(t => t.Province).HasColumnName("Province");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.Region).HasColumnName("Region");
            this.Property(t => t.Industry).HasColumnName("Industry");
            this.Property(t => t.ValidDate).HasColumnName("ValidDate");
            this.Property(t => t.Keys).HasColumnName("Keys");
            this.Property(t => t.ItemContent).HasColumnName("ItemContent");
            this.Property(t => t.ItemType).HasColumnName("ItemType");
            this.Property(t => t.FinancSum).HasColumnName("FinancSum");
            this.Property(t => t.FinancType).HasColumnName("FinancType");
            this.Property(t => t.FinancCycle).HasColumnName("FinancCycle");
            this.Property(t => t.TotalInvestment).HasColumnName("TotalInvestment");
            this.Property(t => t.ReturnRatio).HasColumnName("ReturnRatio");
            this.Property(t => t.ItemStage).HasColumnName("ItemStage");
            this.Property(t => t.ItemValue).HasColumnName("ItemValue");
            this.Property(t => t.Assure).HasColumnName("Assure");
            this.Property(t => t.Collateral).HasColumnName("Collateral");
            this.Property(t => t.PartnerRequirements).HasColumnName("PartnerRequirements");
            this.Property(t => t.AssetsType).HasColumnName("AssetsType");
            this.Property(t => t.AssetsCost).HasColumnName("AssetsCost");
            this.Property(t => t.IsMortgage).HasColumnName("IsMortgage");
            this.Property(t => t.TransferPrice).HasColumnName("TransferPrice");
            this.Property(t => t.TransferType).HasColumnName("TransferType");
            this.Property(t => t.TransactionMode).HasColumnName("TransactionMode");
            this.Property(t => t.Description).HasColumnName("Description2");
            this.Property(t => t.Investment).HasColumnName("Investment");
            this.Property(t => t.CooperationMode).HasColumnName("CooperationMode");
            this.Property(t => t.BuildCycle).HasColumnName("BuildCycle");
            this.Property(t => t.ReturnCycle).HasColumnName("ReturnCycle");
            this.Property(t => t.PublicStatus).HasColumnName("PublicStatus");
            this.Property(t => t.SubmitTime).HasColumnName("SubmitTime");
            this.Property(t => t.PublicTime).HasColumnName("PublicTime");
            this.Property(t => t.Linkman).HasColumnName("Linkman");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Mobile).HasColumnName("Mobile");
            this.Property(t => t.Fax).HasColumnName("Fax");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.QQ).HasColumnName("QQ");
            this.Property(t => t.IsPublic).HasColumnName("IsPublic");
            this.Property(t => t.IsValid).HasColumnName("IsValid");
            this.Property(t => t.OP).HasColumnName("OP");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.UpdateTime).HasColumnName("UpdateTime");


            // Foreign Key
            this.HasRequired(t => t.Member)
              .WithMany(s => s.Financials)
              .HasForeignKey(t => t.MemberID);

        }
    }
}
