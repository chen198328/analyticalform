using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ClownFish;
namespace AnalyticalPlatform
{
    /// <summary>
    /// 客户端与数据库影射
    /// </summary>
    public class DataTableManager
    {
        public static DataSet GetAllTables()
        {
            DataSet dataset = new DataSet();
            dataset.Tables.Add(GetPaper());
            dataset.Tables.Add(GetAuthor());
            dataset.Tables.Add(GetCategory());
            dataset.Tables.Add(GetDocumentType());
            dataset.Tables.Add(GetKeyword());
            dataset.Tables.Add(GetReference());
            dataset.Tables.Add(GetReprintAuthor());
            dataset.Tables.Add(GetAbstract());
            dataset.Tables.Add(GetInstitute());
            dataset.Tables.Add(GetAuthorInstitute());
            dataset.Tables.Add(GetFund());
            dataset.AcceptChanges();
            return dataset;

        }
        public static DataTable GetPaper()
        {
            DataTable table = DbHelper.FillDataTable("select top 1 * from Paper", null, CommandKind.SqlTextNoParams);
            table.TableName = "Paper";
            table.Clear();
            return table;
            #region
            //DataTable table = new DataTable("Paper");
            //table.Columns.Add(new DataColumn("Title"));
            //table.Columns.Add(new DataColumn("Source"));
            //table.Columns.Add(new DataColumn("Year", typeof(int)));
            //table.Columns.Add(new DataColumn("Keywords"));
            //table.Columns.Add(new DataColumn("Categories"));
            //table.Columns.Add(new DataColumn("DocumentTypes"));
            //table.Columns.Add(new DataColumn("Authors"));
            //table.Columns.Add(new DataColumn("KeywordsPlus"));
            //table.Columns.Add(new DataColumn("ResearchAreas"));
            //table.Columns.Add(new DataColumn("Journal"));
            //table.Columns.Add(new DataColumn("Guid"));
            //table.Columns.Add(new DataColumn("Source"));
            //table.Columns.Add(new DataColumn("Language"));
            //table.Columns.Add(new DataColumn("Email"));
            //table.Columns.Add(new DataColumn("Country"));
            //table.Columns.Add(new DataColumn("FundingAgency"));
            //table.Columns.Add(new DataColumn("GrantNumber"));
            //table.Columns.Add(new DataColumn("TotalCitiesInDB", typeof(int)));
            //table.Columns.Add(new DataColumn("NumberofReference", typeof(int)));
            //table.Columns.Add(new DataColumn("Publisher"));
            //table.Columns.Add(new DataColumn("CityofPublisher"));
            //table.Columns.Add(new DataColumn("AddressofPublisher"));
            //table.Columns.Add(new DataColumn("JournalAbbreiation"));
            //table.Columns.Add(new DataColumn("ISSN"));
            //table.Columns.Add(new DataColumn("Year", typeof(int)));
            //table.Columns.Add(new DataColumn("Month", typeof(int)));
            //table.Columns.Add(new DataColumn("Issue", typeof(int)));
            //table.Columns.Add(new DataColumn("Volume", typeof(int)));
            //table.Columns.Add(new DataColumn("PageBegin", typeof(int)));
            //table.Columns.Add(new DataColumn("PageEnd", typeof(int)));
            //table.Columns.Add(new DataColumn("DOI"));
            //table.Columns.Add(new DataColumn("IDSNumber"));
            //table.Columns.Add(new DataColumn("AccessionNumber"));
            //table.Columns.Add(new DataColumn("PublishType"));
            //table.Columns.Add(new DataColumn("FundCount", typeof(int)));
            //table.Columns.Add(new DataColumn("ArticleNumber"));
            //return table;
            #endregion
        }
        public static DataTable GetAuthor()
        {
            DataTable table = DbHelper.FillDataTable("select top 1 * from Author", null, CommandKind.SqlTextNoParams);
            table.TableName = "Author";
            table.Clear();
            return table;
            #region
            //DataTable table = new DataTable("Author");
            //table.Columns.Add(new DataColumn("Name"));
            //table.Columns.Add(new DataColumn("AbbreName"));
            //table.Columns.Add(new DataColumn("PGuid"));
            //table.Columns.Add(new DataColumn("Year", typeof(int)));
            //table.Columns.Add(new DataColumn("Order", typeof(int)));
            //table.AcceptChanges();
            //return table;
            #endregion
        }
        public static DataTable GetCategory()
        {
            DataTable table = DbHelper.FillDataTable("select top 1 * from Category", null, CommandKind.SqlTextNoParams);
            table.TableName = "Category";
            table.Clear();
            return table;
            #region
            //DataTable table = new DataTable("Category");
            //table.Columns.Add(new DataColumn("Category"));
            //table.Columns.Add(new DataColumn("Type"));
            //table.Columns.Add(new DataColumn("PGuid"));
            //table.Columns.Add(new DataColumn("Year", typeof(int)));
            //table.Columns.Add(new DataColumn("Order", typeof(int)));
            //table.AcceptChanges();
            //return table;
            #endregion

        }
        public static DataTable GetCitation()
        {
            DataTable table = DbHelper.FillDataTable("select top 1 * from Citation", null, CommandKind.SqlTextNoParams);
            table.TableName = "Citation";
            table.Clear();
            return table;
            #region
            //DataTable table = new DataTable("Citation");
            //table.Columns.Add(new DataColumn("PGuid"));
            //table.Columns.Add(new DataColumn("RGuid"));
            //table.Columns.Add(new DataColumn("Year", typeof(int)));
            //table.AcceptChanges();
            //return table;
            #endregion
        }
        public static DataTable GetDocumentType()
        {
            DataTable table = DbHelper.FillDataTable("select top 1 * from DocumentType", null, CommandKind.SqlTextNoParams);
            table.TableName = "DocumentType";
            table.Clear();
            return table;
            #region
            //DataTable table = new DataTable("DocumentType");
            //table.Columns.Add(new DataColumn("PGuid"));
            //table.Columns.Add(new DataColumn("DocumentType"));
            //table.Columns.Add(new DataColumn("Year", typeof(int)));
            //table.Columns.Add(new DataColumn("Order", typeof(int)));
            //table.AcceptChanges();
            //return table;
            #endregion
        }
        public static DataTable GetKeyword()
        {
            DataTable table = DbHelper.FillDataTable("select top 1 * from Keyword", null, CommandKind.SqlTextNoParams);
            table.TableName = "Keyword";
            table.Clear();
            return table;
            #region
            //DataTable table = new DataTable("Keyword");
            //table.Columns.Add(new DataColumn("PGuid"));
            //table.Columns.Add(new DataColumn("Keyword"));
            //table.Columns.Add(new DataColumn("Type"));
            //table.Columns.Add(new DataColumn("Year", typeof(int)));
            //table.Columns.Add(new DataColumn("Order", typeof(int)));
            //table.AcceptChanges();
            //return table;
            #endregion
        }
        public static DataTable GetReference()
        {
            DataTable table = DbHelper.FillDataTable("select top 1 * from Reference", null, CommandKind.SqlTextNoParams);
            table.TableName = "Reference";
            table.Clear();
            return table;
            #region
            //DataTable table = new DataTable("Reference");
            //table.Columns.Add(new DataColumn("PGuid"));
            //table.Columns.Add(new DataColumn("Reference"));
            //table.Columns.Add(new DataColumn("Order", typeof(int)));
            //table.Columns.Add(new DataColumn("Author"));
            //table.Columns.Add(new DataColumn("Year", typeof(int)));
            //table.Columns.Add(new DataColumn("Journal"));
            //table.Columns.Add(new DataColumn("Volume", typeof(int)));
            //table.Columns.Add(new DataColumn("Page", typeof(int)));
            //table.Columns.Add(new DataColumn("Doi"));
            //table.AcceptChanges();
            //return table;
            #endregion
        }
        public static DataTable GetReprintAuthor()
        {
            DataTable table = DbHelper.FillDataTable("select top 1 * from ReprintAuthor", null, CommandKind.SqlTextNoParams);
            table.TableName = "ReprintAuthor";
            table.Clear();
            return table;
            #region
            //DataTable table = new DataTable("ReprintAuthor");
            //table.Columns.Add(new DataColumn("Name"));
            //table.Columns.Add(new DataColumn("NameAbbre"));
            //table.Columns.Add(new DataColumn("Country"));
            //table.Columns.Add(new DataColumn("Institute"));
            //table.Columns.Add(new DataColumn("Province"));
            //table.Columns.Add(new DataColumn("City"));
            //table.Columns.Add(new DataColumn("PostCode"));
            //table.Columns.Add(new DataColumn("Address"));
            //table.Columns.Add(new DataColumn("ins1"));
            //table.Columns.Add(new DataColumn("ins2"));
            //table.Columns.Add(new DataColumn("ins3"));
            //table.Columns.Add(new DataColumn("ins4"));
            //table.Columns.Add(new DataColumn("ins5"));
            //table.Columns.Add(new DataColumn("PGuid"));
            //table.AcceptChanges();
            //return table;
            #endregion
        }
        public static DataTable GetInstitute()
        {
            DataTable table = DbHelper.FillDataTable("select top 1 * from Institute", null, CommandKind.SqlTextNoParams);
            table.TableName = "Institute";
            table.Clear();
            return table;
            #region
            //DataTable table = new DataTable("Institute");

            //table.Columns.Add(new DataColumn("Country"));
            //table.Columns.Add(new DataColumn("Institute"));
            //table.Columns.Add(new DataColumn("Organization"));
            //table.Columns.Add(new DataColumn("Lab"));
            //table.Columns.Add(new DataColumn("Hospital"));
            //table.Columns.Add(new DataColumn("Province"));
            //table.Columns.Add(new DataColumn("City"));
            //table.Columns.Add(new DataColumn("PostCode"));
            //table.Columns.Add(new DataColumn("Address"));
            //table.Columns.Add(new DataColumn("ins1"));
            //table.Columns.Add(new DataColumn("ins2"));
            //table.Columns.Add(new DataColumn("ins3"));
            //table.Columns.Add(new DataColumn("ins4"));
            //table.Columns.Add(new DataColumn("ins5"));
            //table.Columns.Add(new DataColumn("PGuid"));
            //table.Columns.Add(new DataColumn("Guid"));
            //table.AcceptChanges();
            //return table;
            #endregion
        }
        public static DataTable GetAuthorInstitute()
        {
            DataTable table = DbHelper.FillDataTable("select top 1 * from AuthorInstitute", null, CommandKind.SqlTextNoParams);
            table.TableName = "AuthorInstitute";
            table.Clear();
            return table;
            #region
            //DataTable table = new DataTable("AuthorInstitute");
            //table.Columns.Add(new DataColumn("Author"));
            //table.Columns.Add(new DataColumn("Order"));
            //table.Columns.Add(new DataColumn("PGuid"));
            //table.Columns.Add(new DataColumn("IGuid"));
            //table.AcceptChanges();
            //return table;
            #endregion
        }
        public static DataTable GetAbstract()
        {
            DataTable table = DbHelper.FillDataTable("select top 1 * from Abstract", null, CommandKind.SqlTextNoParams);
            table.TableName = "Abstract";
            table.Clear();
            return table;
            #region
            //DataTable table = new DataTable("Abstract");
            //table.Columns.Add(new DataColumn("PGuid"));
            //table.Columns.Add(new DataColumn("Abstract"));
            //table.AcceptChanges();
            //return table;
            #endregion
        }
        public static DataTable GetFund()
        {
            DataTable table = DbHelper.FillDataTable("select top 1 * from Fund", null, CommandKind.SqlTextNoParams);
            table.TableName = "Fund";
            table.Clear();
            return table;
            #region
            //DataTable table = new DataTable("Fund");
            //table.Columns.Add(new DataColumn("PGuid"));
            //table.Columns.Add(new DataColumn("Funding"));
            //table.Columns.Add(new DataColumn("Code"));
            //table.Columns.Add(new DataColumn("Order", typeof(int)));
            //table.AcceptChanges();
            //return table;
            #endregion

        }
    }
}
