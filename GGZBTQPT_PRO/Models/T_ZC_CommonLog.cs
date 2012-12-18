using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GGZBTQPT_PRO.Models
{
    public class T_ZC_CommonLog
    {
        public T_ZC_CommonLog()
        {
            
        }

        public int ID { get; set; }

        public DateTime Date { get; set; }//录入时间
        public string Thread { get; set; }//线程
        public string Level { get; set; }//级别,目前分为info,debug,warn,error,fatal,我们将info作为操作日志,error作为错误日志
        public string Logger { get; set; }//错误发生的方法，目前错误日志都通过过滤器进行了过滤，所以Logger中记录的都是过滤器中的方法；
        public string Message { get; set; }//提示信息（包括警告、操作成功、操作失败等等）
        public string Source { get; set; }//代码行,目前错误日志都通过过滤器进行了过滤，所以Source中记录的都是过滤器中的方法； 
        public string Exception { get; set; }//详细的错误信息（主要是错误的堆栈信息）
        public string User { get; set; }//当前操作的人员
        public int OperateType { get; set; }//操作类型记录，详细参考枚举类
        public int GenerateType { get; set; }//操作来源 系统、用户、会员
    }
}