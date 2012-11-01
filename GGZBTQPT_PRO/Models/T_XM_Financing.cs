using System;
using System.Collections.Generic;

namespace GGZBTQPT_PRO.Models
{
    public class T_XM_Financing
    {
        public T_XM_Financing()
        { 
        }

        public int ID { get; set; }

        public string ItemName { get; set; }
        public Nullable<int> Province { get; set; }
        public Nullable<int> City { get; set; }
        public Nullable<int> Region { get; set; }
        public Nullable<int> Industry { get; set; }
        public Nullable<System.DateTime> ValidDate { get; set; }
        public string Keys { get; set; }
        public string ItemContent { get; set; }
        public Nullable<int> ItemType { get; set; }
        public Nullable<decimal> FinancSum { get; set; }
        public Nullable<int> FinancType { get; set; }
        public Nullable<int> FinancCycle { get; set; }
        public Nullable<decimal> TotalInvestment { get; set; }
        public Nullable<double> ReturnRatio { get; set; }
        public Nullable<int> ItemStage { get; set; }
        public Nullable<decimal> ItemValue { get; set; }
        public string Assure { get; set; }
        public string Collateral { get; set; }
        public string PartnerRequirements { get; set; }
        public Nullable<int> AssetsType { get; set; }
        public Nullable<decimal> AssetsCost { get; set; }
        public string IsMortgage { get; set; }
        public Nullable<decimal> TransferPrice { get; set; }
        public Nullable<int> TransferType { get; set; }
        public string TransactionMode { get; set; }
        public Nullable<decimal> Investment { get; set; }
        public Nullable<int> CooperationMode { get; set; }
        public Nullable<int> BuildCycle { get; set; }
        public Nullable<int> ReturnCycle { get; set; }
        public Nullable<decimal> OtherItemFinancSum { get; set; }
        public Nullable<int> OtherItemFinancCycle { get; set; }
        public string PublicStatus { get; set; }
        public Nullable<System.DateTime> SubmitTime { get; set; }
        public Nullable<System.DateTime> PublicTime { get; set; }
        public string Linkman { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public string Address { get; set; }
        public string IsPublic { get; set; }
        public Nullable<bool> IsValid { get; set; }
        public int OP { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }


        public int MemberID { get; set; }
        //-------TO-DO------------
        //�������ĿӦ�úͻ�Ա��һ�Զ�Ĺ�ϵ��ÿ����Ա���ܷ��������Ŀ����Ҫ���Ӷ�Ӧ��ӳ���ϵ

        public virtual T_HY_Member Member { get; set; }

        public virtual ICollection<T_HY_Favorite> Favoites { get; set; }//ÿ����Ŀ����Ӧ����ղ�

        public string IndustryName 
        { 
            get 
            {
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                return db.T_PTF_DicDetail.Find(this.Industry).Name;
            } 
        }

        public string PublicText
        {
            get
            {
                switch (this.PublicStatus)
                {
                    case "1":
                        return "������";
                    case "2":
                        return "����ͨ��";
                    case "9":
                        return "��������";
                }
                return "";
            }
        }

        public string MemberName
        {
            get
            {
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                return db.T_HY_Member.Find(this.MemberID).LoginName;
            }
        }

        public string Amount
        {
            get
            {
                switch (this.ItemType)
                {
                    case 1:
                        return FinancSum.ToString();
                    case 2:
                        return TransferPrice.ToString();
                    case 3:
                        return Investment.ToString();
                    case 9:
                        return OtherItemFinancSum.ToString();
                }
                return "����";
            }
        }

        public string ItemText
        {
            get
            {
                switch (this.ItemType)
                {
                    case 1:
                        return "��Ŀ����";
                    case 2:
                        return "�ʲ�����";
                    case 3:
                        return "��������";
                }
                return "����";
            }
        }
    }
}
