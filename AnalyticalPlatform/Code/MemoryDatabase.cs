using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
namespace AnalyticalPlatform
{
    public class MemoryDatabase
    {
        private static readonly List<Paper> paperlist = new List<Paper>();
        private static object objInstance = new object();
        private static object objGetRange = new object();
        private static object objAddRange = new object();
        private static object objTaskCount = new object();
        private static bool isWorkingTask { set; get; }


        /// <summary>
        /// 截取指定的论文列表
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<Paper> GetRange(int start, int count)
        {
            lock (objGetRange)
            {
                if (count > paperlist.Count)
                    count = paperlist.Count;
                List<Paper> temppaperList = paperlist.GetRange(start, count);
                paperlist.RemoveRange(start, count);
                return temppaperList;
            }
        }
        public static List<Paper> GetPaperList()
        {
            return paperlist;
        }
        /// <summary>
        /// 添加论文列表
        /// </summary>
        /// <param name="paperList"></param>
        public static void AddRange(List<Paper> paperList)
        {
            lock (objAddRange)
            {
                paperlist.AddRange(paperList);
            }
        }
        public static void SetWorkingTask(bool isworkingtask)
        {
            isWorkingTask = isworkingtask;
        }
        public static bool IsStop()
        {
            if (paperlist.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
