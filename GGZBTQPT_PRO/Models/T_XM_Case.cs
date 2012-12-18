using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    public class T_XM_Case
    {
        public T_XM_Case()
        {
            
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "必须填写案例名称")]
        [Display(Name = "案例名称")]
        public string Name { get; set; }//案例名称

        [Required(ErrorMessage = "必须选择案例类型")]
        [Display(Name = "案例类型")]
        public int Type { get; set; }//金融项目类型 
 
        [Display(Name = "生成类型")]
        public int GenerateType { get; set; }//金融案例生成类型，用于判断该类型是自主填写的，还是通过交易记录生成的

        [Display(Name = "生成人")]
        public int GenerateFromID { get; set; }//生成人的ID，包括会员ID，用户ID，项目ID

        [Required(ErrorMessage = "必须填写案例摘要")]
        [Display(Name = "案例摘要")]
        public string Summary { get; set; }//案例摘要

        [Required(ErrorMessage = "必须填写案例分析内容")]
        [Display(Name = "案例分析")]
        public string Analysis { get; set; }//案例分析 

        [Display(Name = "上载案例分析文件")]
        public byte[] File { get; set; }//案例分析文件

        public int OP { get; set; }//发布人
        public bool IsPublished { get; set; }
        public bool IsValid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 
    }
}