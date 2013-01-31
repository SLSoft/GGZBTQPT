using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    public class T_XM_Financing
    {
        public T_XM_Financing()
        { 
            ItemName = " ";
            Province = "0";
            City = "0";
            Region = "0";
            Industry = 0;
            ValidDate = DateTime.Now;
            Keys = " ";
            ItemContent = " ";
            ItemType = 0;
            FinancSum = 0;
            FinancType = 0;
            FinancCycle = 0;
            TotalInvestment = 0;
            ReturnRatio = 0;
            ItemStage = 0;
            ItemValue = 0;
            Assure = " ";
            Collateral = " ";
            PartnerRequirements = " ";
            AssetsType = 0;
            AssetsCost = 0;
            IsMortgage = "1";
            TransferPrice = 0;
            TransferType = 1;
            TransactionMode = " ";
            Investment = 0;
            CooperationMode = 0;
            BuildCycle = 0;
            ReturnCycle = 0;
            OtherItemFinancSum = 0;
            OtherItemFinancCycle = 0;
            PublicStatus = " ";
            SubmitTime = DateTime.MaxValue;
            PublicTime = DateTime.MaxValue;
            Linkman = " ";
            Position = " ";
            Phone = " ";
            Mobile = " ";
            Fax = " ";
            Email = " ";
            QQ = " ";
            Address = " ";
            IsPublic = " ";
            IsValid = true;
            OP = 0;
            CreateTime = DateTime.MaxValue;
            UpdateTime = DateTime.MaxValue;
            Pic = new Byte[0]; 
            MemberID = 9999;
            Clicks = 0;
        }

        
        public int ID { get; set; }
        [Required(ErrorMessage = "项目名称不能为空")]
        public string ItemName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public Nullable<int> Industry { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> ValidDate { get; set; }
        public string Keys { get; set; }
        [AllowHtml]
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
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> CreateTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public byte[] Pic { get; set; }
        public int Clicks { get; set; }

        public int MemberID { get; set; } 
        public virtual T_HY_Member Member { get; set; }

        public virtual ICollection<T_HY_Favorite> Favoites { get; set; }//每个项目都对应多个收藏


        //所属区域
        public string AreaName
        {
            get
            {
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                List<T_PTF_DicTreeDetail> dic = db.T_PTF_DicTreeDetail.Where(d => (d.DicType == "34" && d.Code == this.Province)).ToList();
                if (dic.Count > 0)
                    return dic[0].Name;
                else
                    return "";
            }
        }

        //所属行业
        public string IndustryName 
        { 
            get 
            {
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                if (db.T_PTF_DicDetail.Find(this.Industry) != null)
                    return db.T_PTF_DicDetail.Find(this.Industry).Name;
                else
                    return "";
            } 
        }

        //审核状态
        public string PublicText
        {
            get
            {
                switch (this.PublicStatus)
                {
                    case "1":
                        return "待审批";
                    case "2":
                        return "审批通过";
                    case "9":
                        return "审批驳回";
                }
                return "";
            }
        }
        //发布人
        public string MemberName
        {
            get
            {
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                if (db.T_HY_Member.Find(this.MemberID) != null)
                    return db.T_HY_Member.Find(this.MemberID).MemberName;
                else
                    return "";
            }
        }
        //融资金额
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
                return "不限";
            }
        }
        //项目类别
        public string ItemText
        {
            get
            {
                switch (this.ItemType)
                {
                    case 1:
                        return "项目融资";
                    case 2:
                        return "资产交易";
                    case 3:
                        return "政府招商";
                }
                return "其他";
            }
        }
        //融资方式
        public string FinancTypeText
        {
            get
            {
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                if (db.T_PTF_DicDetail.Find(this.FinancType) != null)
                    return db.T_PTF_DicDetail.Find(this.FinancType).Name;
                else
                    return "";
            }
        }

        //项目阶段
        public string ItemStageText
        {
            get
            {
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                if (db.T_PTF_DicDetail.Find(this.ItemStage) != null)
                    return db.T_PTF_DicDetail.Find(this.ItemStage).Name;
                else
                    return "";
            }
        }

        //资产类别
        public string AssetsTypeText
        {
            get
            {
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                if (db.T_PTF_DicDetail.Find(this.AssetsType) != null)
                    return db.T_PTF_DicDetail.Find(this.AssetsType).Name;
                else
                    return "";
            }
        }

        //交易方式
        public string TransactionModeText
        {
            get
            {
                string result = "";
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                if (this.TransactionMode.Length > 1)
                {
                    string[] temp = TransactionMode.Split(',');
                    foreach(string t in temp)
                    {
                         if(db.T_PTF_DicDetail.Find(t) != null)
                             result += db.T_PTF_DicDetail.Find(t).Name + " ";
                    }
                }
                return result;
            }
        }

        //合作方式
        public string CooperationModeText
        {
            get
            {
                GGZBTQPTDBContext db = new GGZBTQPTDBContext();
                if (db.T_PTF_DicDetail.Find(this.CooperationMode) != null)
                    return db.T_PTF_DicDetail.Find(this.CooperationMode).Name;
                else
                    return "";
            }
        }
        //转让形式
        public string TransferTypeText
        {
            get
            {
                return this.TransferType == 1 ? "整体" : "部分";
            }
        }
    }
}
