using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Enums
{
    //会员类别
    public enum MemberTypes : int
    {
        [Display(Name = "全部")]
        全部 = -1,
        [Display(Name = "个人")]
        个人 = 1,
        [Display(Name = "企业")]
        企业 = 2,
        [Display(Name = "机构")]
        机构 = 3
    }

    //会员状态
    public enum MemberStates : int
    {
        //[Display(Name = "待审核")]
        //待审核 = 0,
        [Display(Name = "通过")]
        通过 = 1,
        [Display(Name = "驳回")]
        驳回 = 2
    }

    public enum CaseTypes : int
    {
        [Display(Name = "融资项目")]
        Financing,
        [Display(Name = "投资项目")]
        Investment
    }

    //
    //---------日志相关-------------
    public enum LogLevels : int
    { 
        //操作日志、登录日志、错误日志、警告日志（用于操作失败）
        operate, login, error, warn
    }

    public enum OperateTypes : int
    {
        //操作日志类别
        Attention,//关注
        Favorite,//收藏
        Release,//发布
        Edit,//编辑
        Add,//新增
        Delete,//删除
        Search//搜索 
    }


    public enum GenerateTypes : int
    { 
        [Display(Name = "用户生成")]
        FromUser, 
        [Display(Name = "系统生成")] 
        FromSystem,
        [Display(Name = "会员生成")]
        FromMember
    }

    public enum GenerateSystem : int
    {
        [Display(Name = "金融推荐")]
        Recommend,
        [Display(Name = "找项目")]
        FindFinancial,
        [Display(Name = "找资金")]
        FindInvestment,
        [Display(Name = "找服务")]
        FindService,
        [Display(Name = "我的关注")]
        Attention,
        [Display(Name = "我的收藏")]
        Favorite,
        [Display(Name = "我的发布")]
        Publish
    }


    //使用级别，用于系统用户分类，部门分类等，是为了区分系统用户和系统内置的管理用户
    public enum UseLevel : int
    { 
        [Display(Name = "用户级别")]
        System, 
        [Display(Name = "管理级别")] 
        Manage
    }
}

