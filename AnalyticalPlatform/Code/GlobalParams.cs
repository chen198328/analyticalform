using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticalPlatform
{
    /// <summary>
    /// 全局变量
    /// </summary>
   public static class GlobalParams
    {
       /// <summary>
       /// 主数据库连接
       /// </summary>
       public static string MasterConnectionString;
       /// <summary>
       /// 本地数据库连接
       /// </summary>
       public static string ConnectionString;
       public static User User;
       public static string Databasename;
       public static int ErrorCount;
       public static string ErrorMessage;
    }
}
