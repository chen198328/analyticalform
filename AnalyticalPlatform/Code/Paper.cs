using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalyticalPlatform
{
    public class Paper
    {
        public string Guid { set; get; }
        /// <summary>
        /// 出版物类型 PT
        /// </summary>
        public string PublishType { set; get; }
        /// <summary>
        /// 论文标题 TI
        /// </summary>
        public string Title { set; get; }
        /// <summary>
        /// 论文来源 SO
        /// </summary>
        public string Source { set; get; }
        /// <summary>
        /// 语言 LA
        /// </summary>
        public string Language { set; get; }
        /// <summary>
        /// 文档类型 DT
        /// </summary>
        public string DocumentTypes { set; get; }
        /// <summary>
        /// 作者 AU
        /// </summary>
        public List<string> Author { set; get; }
        /// <summary>
        /// 作者全称 AF
        /// </summary>
        public List<string> FullNameofAuthor { set; get; }
        /// <summary>
        /// 作者关键词 DE
        /// </summary>
        public string Keywords { set; get; }
        /// <summary>
        /// 系统增添关键词 ID
        /// </summary>
        public string KeywordsPlus { set; get; }
        /// <summary>
        /// 论文摘要 AB
        /// </summary>
        public string Abstract { set; get; }
        /// <summary>
        /// 通讯作者邮箱 EM
        /// </summary>
        public string Email { set; get; }
        /// <summary>
        /// 作者地址信息,后期拓展考虑使用作者+地址构建字典 C1
        /// </summary>
        //public List<string> Address { set; get; }
        public List<string> Address { set; get; }
        /// <summary>
        /// 通讯作者地址信息 RP
        /// </summary>
        public string ReprintAddress { set; get; }
        /// <summary>
        /// 基金字段 FU
        /// </summary>
        public string Funding { set; get; }
        /// <summary>
        /// 致谢字段
        /// </summary>
        public string Acknowledgement { set; get; }
        /// <summary>
        /// 被引频次 TC
        /// </summary>
        public string TotalCites { set; get; }
        /// <summary>
        /// 在数据库中的被引频次
        /// </summary>
        public string TotalCitesInDB { set; get; }
        /// <summary>
        /// 参考文献数量 NR
        /// </summary>
        public string NumberofReference { set; get; }
        /// <summary>
        /// 参考文献列表 CR
        /// </summary>
        public List<string> Reference { set; get; }
        /// <summary>
        /// 出版商 PU
        /// </summary>
        public string Publisher { set; get; }
        /// <summary>
        /// 出版商所在城市 PI
        /// </summary>
        public string CityofPublisher { set; get; }
        /// <summary>
        /// 出版商地址 PA
        /// </summary>
        public string AddressofPublisher { set; get; }

        /// <summary>
        /// 期刊简称 用于匹配参考文献 J9
        /// </summary>
        public string Journal { set; get; }
        /// <summary>
        /// 期刊简称 JI
        /// </summary>
        public string JournalAbbreiation { set; get; }
        /// <summary>
        /// 期刊序列号 SN
        /// </summary>
        public string ISSN { set; get; }
        /// <summary>
        /// 发表年份 PY
        /// </summary>
        public string Year { set; get; }
        /// <summary>
        /// 发表月份 PD 需要将英文转换
        /// </summary>
        public string Month { set; get; }
        /// <summary>
        /// 卷 IS
        /// </summary>
        public string Issue { set; get; }
        /// <summary>
        /// 期 VL
        /// </summary>
        public string Volume { set; get; }
        /// <summary>
        /// 起始页 BP
        /// </summary>
        public string PageBegin { set; get; }
        /// <summary>
        /// 结束页 EP
        /// </summary>
        public string PageEnd { set; get; }
        /// <summary>
        /// 文章编号 AR
        /// </summary>
        public string ArticleNumber { set; get; }
        /// <summary>
        /// DOI DI
        /// </summary>
        public string DOI { set; get; }
        /// <summary>
        /// WebofScience分类 WC
        /// </summary>
        public string WebofScienceCategories { set; get; }
        /// <summary>
        /// Reasearch Areas分类 SC
        /// </summary>
        public string ResearchAreas { set; get; }
        /// <summary>
        /// IDS Number GA
        /// </summary>
        public string IDSNumber { set; get; }
        /// <summary>
        /// 入藏号 UT
        /// </summary>
        public string AccessionNumber { set; get; }
        /// <summary>
        /// 文件名,用于报错与提示
        /// </summary>
        public string FileName { set; get; }
        public Paper()
        {
            Guid = System.Guid.NewGuid().ToString("N");
            Author = new List<string>();
            FullNameofAuthor = new List<string>();
            Address = new List<string>();
            Reference = new List<string>();
        }
    }
}
