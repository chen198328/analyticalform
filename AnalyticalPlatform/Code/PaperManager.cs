using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
namespace AnalyticalPlatform
{
    public class PaperManager
    {
        public static List<Paper> FillPaperFromStream(StreamReader read, string filename)
        {
            string line = string.Empty;
            Paper paper = null;
            List<Paper> paperlist = new List<Paper>();
            string labelField = string.Empty;
            string whitespace = "\x20";
            string startstring = string.Empty;
            while ((line = read.ReadLine()) != null)
            {
                if (line == string.Empty)
                {
                    continue;
                }
                //此标记为连续字段的标记符号  所有字段都标记为大写
                startstring = line.Substring(0, 2);
                line = line.Substring(2, line.Length - 2).Trim();
                if (startstring == whitespace + whitespace)
                    startstring = labelField;
                #region 文档类型
                //if (startstring == "FN")
                //{
                //    continue;
                //}
                #endregion
                #region 论文字段的映射
                switch (startstring)
                {
                    case "PT":
                        paper = new Paper();
                        paper.FileName = filename;
                        paper.PublishType = line;
                        break;
                    case "TI":
                        paper.Title += line + whitespace;
                        labelField = "TI";
                        break;
                    case "SO":
                        paper.Source += line + whitespace;
                        labelField = "SO";
                        break;
                    case "LA":
                        paper.Language = line + ";" + paper.Language;
                        labelField = "LA";
                        break;
                    case "DT":
                        paper.DocumentTypes = line + ";" + paper.DocumentTypes;
                        labelField = "DT";
                        break;
                    case "AU":
                        paper.Author.Add(line);
                        labelField = "AU";
                        break;
                    case "AF":
                        paper.FullNameofAuthor.Add(line);
                        labelField = "AF";
                        break;
                    case "DE":
                        //关键词注意句号的处理
                        paper.Keywords += line + whitespace;
                        labelField = "DE";
                        break;
                    case "ID":
                        //
                        paper.KeywordsPlus += line + whitespace;
                        labelField = "ID";
                        break;
                    case "AB":
                        paper.Abstract += line + whitespace;
                        labelField = "AB";
                        break;
                    case "EM":
                        //会存在多个邮箱 其中;隔开
                        paper.Email = line + whitespace;
                        labelField = "EM";
                        break;
                    case "C1":
                        //其中作者名使用全称AF
                        //只有一个作者时，不会提示作者名字[作者名] 
                        //多个作者 一个地址时 如下表示[a;b;c]Address
                        //address.Append(line);
                        paper.Address.Add(line);
                        labelField = "C1";
                        break;
                    case "RP":
                        paper.ReprintAddress += line + whitespace;
                        labelField = "RP";
                        break;
                    case "FU":
                        paper.Funding += line + whitespace;
                        labelField = "FU";
                        break;
                    case "FX":
                        paper.Acknowledgement += line + whitespace;
                        labelField = "FX";
                        break;
                    case "TC":
                        paper.TotalCites = line;
                        break;
                    case "NR":
                        paper.NumberofReference = line;
                        break;
                    case "CR":
                        paper.Reference.Add(line);
                        labelField = "CR";
                        break;
                    case "PU":
                        paper.Publisher += line + whitespace;
                        labelField = "PU";
                        break;
                    case "PI":
                        paper.CityofPublisher += line + whitespace;
                        labelField = "PI";
                        break;
                    case "PA":
                        paper.AddressofPublisher += line + whitespace;
                        labelField = "PA";
                        break;
                    case "J9":
                        paper.Journal += line + whitespace;
                        labelField = "J9";
                        break;
                    case "JI":
                        paper.JournalAbbreiation += line + whitespace;
                        labelField = "JI";
                        break;
                    case "SN":
                        paper.ISSN = line;
                        break;
                    case "PY":
                        paper.Year = line;
                        break;
                    case "PD":
                        paper.Month = line;
                        break;
                    case "IS":
                        paper.Issue = line;
                        break;
                    case "VL":
                        paper.Volume = line;
                        break;
                    case "BP":
                        paper.PageBegin = line;
                        break;
                    case "EP":
                        paper.PageEnd = line;
                        break;
                    case "AR":
                        paper.ArticleNumber = line;
                        break;
                    case "DI":
                        paper.DOI = line.Replace("'", "\"");
                        break;
                    case "WC":
                        //多个学科使用;隔开 建议使用string[]
                        paper.WebofScienceCategories += line + whitespace;
                        labelField = "WC";
                        break;
                    case "SC":
                        //多个学科使用;隔开 建议使用string[]
                        paper.ResearchAreas += line + whitespace;
                        labelField = "SC";
                        break;
                    case "UT":
                        paper.AccessionNumber = line;
                        break;
                    case "ER":
                        if (paper != null)
                        {
                          paper.Language=paper.Language.Trim(';');
                            paperlist.Add(paper);
                        }
                        break;
                    default:
                        labelField = "";
                        break;
                }
                #endregion
            }
            read.Close();
            return paperlist;

        }
        public static void TransferData(List<Paper> paperlist)
        {
            SqlBulkCopy(FillDataSetFromList(paperlist));
        }
        public static void TransferData(List<Paper> paperlist, SqlConnection connection)
        {
            SqlBulkCopy(FillDataSetFromList(paperlist), connection);
        }
        public static void TransferDataByPartion(List<Paper> paperlist)
        {
            SqlBulkCopyByPartion(FillDataSetFromList(paperlist));
        }
        /// <summary>
        /// 批量存储
        /// </summary>
        /// <param name="dataset"></param>
        public static void SqlBulkCopy(DataSet dataset)
        {
            Trace.Write(dataset.Tables["Paper"].Rows.Count + "\r\n");
            using (SqlConnection connection = new SqlConnection(GlobalParams.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock, trans);
                try
                {
                    //Stopwatch stopwatch = new Stopwatch();

                    sqlBulkCopy.BatchSize = 10000 * 200;
                    sqlBulkCopy.BulkCopyTimeout = 60 * 10;

                    //stopwatch.Start();
                    sqlBulkCopy.DestinationTableName = "Paper";
                    sqlBulkCopy.WriteToServer(dataset.Tables["Paper"]);
                    //dataset.Tables.Remove("Paper");

                    sqlBulkCopy.DestinationTableName = "Abstract";
                    sqlBulkCopy.WriteToServer(dataset.Tables["Abstract"]);
                    //dataset.Tables.Remove("Abstract");

                    sqlBulkCopy.DestinationTableName = "Author";
                    sqlBulkCopy.WriteToServer(dataset.Tables["Author"]);
                    //dataset.Tables.Remove("Author");

                    sqlBulkCopy.DestinationTableName = "Category";
                    sqlBulkCopy.WriteToServer(dataset.Tables["Category"]);
                    //dataset.Tables.Remove("Category");

                    sqlBulkCopy.DestinationTableName = "Keyword";
                    sqlBulkCopy.WriteToServer(dataset.Tables["Keyword"]);
                    //dataset.Tables.Remove("Keyword");

                    sqlBulkCopy.DestinationTableName = "DocumentType";
                    sqlBulkCopy.WriteToServer(dataset.Tables["DocumentType"]);
                    //dataset.Tables.Remove("DocumentType");

                    sqlBulkCopy.DestinationTableName = "Reference";
                    sqlBulkCopy.WriteToServer(dataset.Tables["Reference"]);
                    //dataset.Tables.Remove("Reference");

                    sqlBulkCopy.DestinationTableName = "ReprintAuthor";
                    sqlBulkCopy.WriteToServer(dataset.Tables["ReprintAuthor"]);
                    //dataset.Tables.Remove("ReprintAuthor");

                    sqlBulkCopy.DestinationTableName = "Institute";
                    sqlBulkCopy.WriteToServer(dataset.Tables["Institute"]);
                    //dataset.Tables.Remove("Institute");

                    sqlBulkCopy.DestinationTableName = "AuthorInstitute";
                    sqlBulkCopy.WriteToServer(dataset.Tables["AuthorInstitute"]);
                    //dataset.Tables.Remove("AuthorInstitute");

                    sqlBulkCopy.DestinationTableName = "Fund";
                    sqlBulkCopy.WriteToServer(dataset.Tables["Fund"]);
                    //dataset.Tables.Remove("Fund");
                    trans.Commit();

                }
                catch (IOException ex)
                {
                    trans.Rollback();
                    throw new Exception("数据插入失败" + ex.Message.ToString());
                }
                catch (SqlException ex)
                {
                    trans.Rollback();
                    throw new Exception("数据插入失败" + ex.Message.ToString());
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("数据插入失败" + ex.Message.ToString());
                }
                finally
                {
                    connection.Close();
                    trans.Dispose();
                    sqlBulkCopy.Close();
                    dataset = null;
                    GC.Collect();
                }

            }
        }
        public static void SqlBulkCopy(DataSet dataset, SqlConnection connection)
        {
            SqlTransaction trans = connection.BeginTransaction();
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock, trans);
            try
            {
                //Stopwatch stopwatch = new Stopwatch();

                sqlBulkCopy.BatchSize = 10000 * 200;
                sqlBulkCopy.BulkCopyTimeout = 60 * 10;

                //stopwatch.Start();
                sqlBulkCopy.DestinationTableName = "Paper";
                sqlBulkCopy.WriteToServer(dataset.Tables["Paper"]);
                //dataset.Tables.Remove("Paper");

                sqlBulkCopy.DestinationTableName = "Abstract";
                sqlBulkCopy.WriteToServer(dataset.Tables["Abstract"]);
                //dataset.Tables.Remove("Abstract");

                sqlBulkCopy.DestinationTableName = "Author";
                sqlBulkCopy.WriteToServer(dataset.Tables["Author"]);
                //dataset.Tables.Remove("Author");

                sqlBulkCopy.DestinationTableName = "Category";
                sqlBulkCopy.WriteToServer(dataset.Tables["Category"]);
                //dataset.Tables.Remove("Category");

                sqlBulkCopy.DestinationTableName = "Keyword";
                sqlBulkCopy.WriteToServer(dataset.Tables["Keyword"]);
                //dataset.Tables.Remove("Keyword");

                sqlBulkCopy.DestinationTableName = "DocumentType";
                sqlBulkCopy.WriteToServer(dataset.Tables["DocumentType"]);
                //dataset.Tables.Remove("DocumentType");

                sqlBulkCopy.DestinationTableName = "Reference";
                sqlBulkCopy.WriteToServer(dataset.Tables["Reference"]);
                //dataset.Tables.Remove("Reference");

                sqlBulkCopy.DestinationTableName = "ReprintAuthor";
                sqlBulkCopy.WriteToServer(dataset.Tables["ReprintAuthor"]);
                //dataset.Tables.Remove("ReprintAuthor");

                sqlBulkCopy.DestinationTableName = "Institute";
                sqlBulkCopy.WriteToServer(dataset.Tables["Institute"]);
                //dataset.Tables.Remove("Institute");

                sqlBulkCopy.DestinationTableName = "AuthorInstitute";
                sqlBulkCopy.WriteToServer(dataset.Tables["AuthorInstitute"]);
                //dataset.Tables.Remove("AuthorInstitute");

                sqlBulkCopy.DestinationTableName = "Fund";
                sqlBulkCopy.WriteToServer(dataset.Tables["Fund"]);
                //dataset.Tables.Remove("Fund");
                trans.Commit();

            }
            catch (IOException ex)
            {
                trans.Rollback();
                throw new Exception("数据插入失败" + ex.Message.ToString());
            }
            catch (SqlException ex)
            {
                trans.Rollback();
                throw new Exception("数据插入失败" + ex.Message.ToString());
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw new Exception("数据插入失败" + ex.Message.ToString());
            }
            finally
            {
                sqlBulkCopy.Close();
                dataset = null;
                GC.Collect();
            }
        }
        static DataSet DataCache;
        public static void SqlBulkCopyWithCache(DataSet dataset, bool isLast = false)
        {
            if (DataCache == null)
            {
                DataCache = dataset.Copy();
            }
            else
            {
                DataCache.Merge(dataset);
            }
            dataset = null;
            if (DataCache.Tables["Paper"].Rows.Count > 25000 || isLast)
            {
                Trace.Write(DataCache.Tables["Paper"].Rows.Count + "\r\n");
                //需要添加Transaction
                using (SqlConnection connection = new SqlConnection(GlobalParams.ConnectionString))
                {
                    connection.Open();
                    SqlTransaction trans = connection.BeginTransaction();
                    SqlBulkCopy sqlBulkCopy;
                    sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock, trans);

                    try
                    {
                        //Stopwatch stopwatch = new Stopwatch();

                        sqlBulkCopy.BatchSize = 10000 * 200;
                        sqlBulkCopy.BulkCopyTimeout = 60 * 10;

                        //stopwatch.Start();
                        sqlBulkCopy.DestinationTableName = "Paper";
                        sqlBulkCopy.WriteToServer(DataCache.Tables["Paper"]);
                        DataCache.Tables.Remove("Paper");

                        sqlBulkCopy.DestinationTableName = "Abstract";
                        sqlBulkCopy.WriteToServer(DataCache.Tables["Abstract"]);
                        DataCache.Tables.Remove("Abstract");

                        sqlBulkCopy.DestinationTableName = "Author";
                        sqlBulkCopy.WriteToServer(DataCache.Tables["Author"]);
                        DataCache.Tables.Remove("Author");

                        sqlBulkCopy.DestinationTableName = "Category";
                        sqlBulkCopy.WriteToServer(DataCache.Tables["Category"]);
                        DataCache.Tables.Remove("Category");

                        sqlBulkCopy.DestinationTableName = "Keyword";
                        sqlBulkCopy.WriteToServer(DataCache.Tables["Keyword"]);
                        DataCache.Tables.Remove("Keyword");

                        sqlBulkCopy.DestinationTableName = "DocumentType";
                        sqlBulkCopy.WriteToServer(DataCache.Tables["DocumentType"]);
                        DataCache.Tables.Remove("DocumentType");

                        sqlBulkCopy.DestinationTableName = "Reference";
                        sqlBulkCopy.WriteToServer(DataCache.Tables["Reference"]);
                        DataCache.Tables.Remove("Reference");

                        sqlBulkCopy.DestinationTableName = "ReprintAuthor";
                        sqlBulkCopy.WriteToServer(DataCache.Tables["ReprintAuthor"]);
                        DataCache.Tables.Remove("ReprintAuthor");

                        sqlBulkCopy.DestinationTableName = "Institute";
                        sqlBulkCopy.WriteToServer(DataCache.Tables["Institute"]);
                        DataCache.Tables.Remove("Institute");

                        sqlBulkCopy.DestinationTableName = "AuthorInstitute";
                        sqlBulkCopy.WriteToServer(DataCache.Tables["AuthorInstitute"]);
                        DataCache.Tables.Remove("AuthorInstitute");

                        sqlBulkCopy.DestinationTableName = "Fund";
                        sqlBulkCopy.WriteToServer(DataCache.Tables["Fund"]);
                        DataCache.Tables.Remove("Fund");
                        trans.Commit();
                        DataCache = null;

                    }
                    catch (IOException ex)
                    {
                        trans.Rollback();
                        throw new Exception("数据插入失败" + ex.Message.ToString());
                    }
                    catch (SqlException ex)
                    {
                        trans.Rollback();
                        throw new Exception("数据插入失败" + ex.Message.ToString());
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("数据插入失败" + ex.Message.ToString());
                    }
                    finally
                    {
                        sqlBulkCopy.Close();
                        GC.Collect();
                    }

                }
            }
        }
        public static void SqlBulkCopyByPartion(DataSet dataset)
        {
            Task[] TaskList = new Task[3];
            try
            {
                TaskList[0] = Task.Factory.StartNew(() =>
                {
                    SqlBulkCopyByPartion1(dataset);
                });
                TaskList[1] = Task.Factory.StartNew(() =>
                {
                    SqlBulkCopyByPartion2(dataset);
                });
                TaskList[2] = Task.Factory.StartNew(() =>
                {
                    SqlBulkCopyByPartion3(dataset);
                });
                Task.WaitAll(TaskList);
            }
            catch (AggregateException exception)
            {
                string message = string.Empty;
                foreach (var item in exception.InnerExceptions)
                {
                    message += item.Message;
                }
                throw new Exception(message);
            }
        }
        public static void SqlBulkCopyByPartion1(DataSet dataset)
        {

            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(GlobalParams.ConnectionString, SqlBulkCopyOptions.TableLock))
            {

                sqlBulkCopy.BatchSize = 10000 * 100;
                sqlBulkCopy.BulkCopyTimeout = 60 * 10;

                sqlBulkCopy.DestinationTableName = "Paper";
                sqlBulkCopy.WriteToServer(dataset.Tables["Paper"]);
                dataset.Tables.Remove("Paper");

                sqlBulkCopy.DestinationTableName = "ReprintAuthor";
                sqlBulkCopy.WriteToServer(dataset.Tables["ReprintAuthor"]);
                dataset.Tables.Remove("ReprintAuthor");

                sqlBulkCopy.DestinationTableName = "Abstract";
                sqlBulkCopy.WriteToServer(dataset.Tables["Abstract"]);
                dataset.Tables.Remove("Abstract");

                sqlBulkCopy.DestinationTableName = "Reference";
                sqlBulkCopy.WriteToServer(dataset.Tables["Reference"]);
                dataset.Tables.Remove("Reference");

                sqlBulkCopy.DestinationTableName = "Institute";
                sqlBulkCopy.WriteToServer(dataset.Tables["Institute"]);

                sqlBulkCopy.Close();

            }
        }
        public static void SqlBulkCopyByPartion2(DataSet dataset)
        {

            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(GlobalParams.ConnectionString, SqlBulkCopyOptions.TableLock))
            {

                sqlBulkCopy.BatchSize = 10000 * 100;
                sqlBulkCopy.BulkCopyTimeout = 60 * 10;

                sqlBulkCopy.DestinationTableName = "Author";
                sqlBulkCopy.WriteToServer(dataset.Tables["Author"]);
                dataset.Tables.Remove("Author");

                sqlBulkCopy.DestinationTableName = "Keyword";
                sqlBulkCopy.WriteToServer(dataset.Tables["Keyword"]);
                dataset.Tables.Remove("Keyword");

                sqlBulkCopy.DestinationTableName = "DocumentType";
                sqlBulkCopy.WriteToServer(dataset.Tables["DocumentType"]);
                dataset.Tables.Remove("DocumentType");

                sqlBulkCopy.DestinationTableName = "Category";
                sqlBulkCopy.WriteToServer(dataset.Tables["Category"]);
                dataset.Tables.Remove("Category");

                sqlBulkCopy.DestinationTableName = "Fund";
                sqlBulkCopy.WriteToServer(dataset.Tables["Fund"]);
                dataset.Tables.Remove("Fund");
                sqlBulkCopy.Close();
            }
        }
        public static void SqlBulkCopyByPartion3(DataSet dataset)
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(GlobalParams.ConnectionString, SqlBulkCopyOptions.TableLock))
            {
                sqlBulkCopy.BatchSize = 10000 * 100;
                sqlBulkCopy.BulkCopyTimeout = 60 * 10;

                sqlBulkCopy.DestinationTableName = "AuthorInstitute";
                sqlBulkCopy.WriteToServer(dataset.Tables["AuthorInstitute"]);
                dataset.Tables.Remove("AuthorInstitute");

                sqlBulkCopy.Close();

            }
        }
        /// <summary>
        /// 把对象列表转化为Dataset，便于以后的使用
        /// </summary>
        /// <param name="paperList"></param>
        /// <returns></returns>
        public static DataSet FillDataSetFromList(List<Paper> paperList)
        {

            int position = 1;
            //存储作者的顺序，用于修改连接作者与机构的关系
            Dictionary<string, int> AuthorOrder = new Dictionary<string, int>();
            string strTemp = string.Empty;
            int intTemp = 0;
            InstituteManager institute = null;
            Reference reference = null;
            DataSet dataset = DataTableManager.GetAllTables();
            Paper paper = null;

            string name = string.Empty;
            string nameAbbre = string.Empty;
            string reprintauthor = string.Empty;
            DataRow dataRow = dataset.Tables["Paper"].NewRow();
            DataRow dataRowAb = dataset.Tables["Abstract"].NewRow();
            DataRow dataRowAu = dataset.Tables["Author"].NewRow();
            DataRow dataRowCa = dataset.Tables["Category"].NewRow();
            DataRow dataRowKe = dataset.Tables["Keyword"].NewRow();
            DataRow dataRowDo = dataset.Tables["DocumentType"].NewRow();
            DataRow dataRowRe = dataset.Tables["Reference"].NewRow();
            DataRow dataRowRp = dataset.Tables["ReprintAuthor"].NewRow();
            DataRow dataRowIn = dataset.Tables["Institute"].NewRow();
            DataRow dataRowAI = dataset.Tables["AuthorInstitute"].NewRow();
            DataRow dataRowFu = dataset.Tables["Fund"].NewRow();

            for (int i = 0; i < paperList.Count; i++)
            {
                if (i % 5000 == 0)
                {
                    GC.Collect();
                    // System.Threading.Thread.Sleep(500);
                }
                name = string.Empty;
                nameAbbre = string.Empty;

                paper = paperList[i];


                #region abstract add row
                if (paper.Abstract != null)
                {
                    dataRowAb["Abstract"] = paper.Abstract;
                    dataRowAb["PGuid"] = paper.Guid;
                    dataset.Tables["Abstract"].Rows.Add(dataRowAb.ItemArray);

                }
                #endregion

                reprintauthor = GetReprintAuthor(paper.ReprintAddress);
                #region author add row
                position = 1;
                AuthorOrder.Clear();
                if (paper.FullNameofAuthor != null && paper.Author != null)
                {
                    int maxCount = paper.FullNameofAuthor.Count > paper.Author.Count ? paper.FullNameofAuthor.Count : paper.Author.Count;
                    for (int index = 0; index < maxCount; index++)
                    {
                        if (index < paper.FullNameofAuthor.Count && index < paper.Author.Count)
                        {
                            dataRowAu["PGuid"] = paper.Guid;
                            dataRowAu["Year"] = paper.Year;
                            //存储作者顺序 包括全名与简称
                            intTemp = position++;
                            dataRowAu["Order"] = intTemp;
                            strTemp = paper.FullNameofAuthor[index];
                            dataRowAu["Name"] = strTemp;
                            AuthorOrder[strTemp] = intTemp;
                            strTemp = paper.Author[index];
                            dataRowAu["AbbreName"] = strTemp;
                            AuthorOrder[strTemp] = intTemp;

                            #region 修改ReprintAuthor的简称和全称
                            if (paper.FullNameofAuthor[index] == reprintauthor || paper.Author[index] == reprintauthor)
                            {
                                name = paper.FullNameofAuthor[index];
                                nameAbbre = paper.Author[index];
                            }
                            #endregion

                        }
                        else if (index > paper.FullNameofAuthor.Count && index < paper.Author.Count)
                        {
                            dataRowAu["PGuid"] = paper.Guid;
                            dataRowAu["Year"] = paper.Year;
                            dataRowAu["AbbreName"] = paper.Author[index];
                            dataRowAu["Order"] = position++;
                            #region 修改ReprintAuthor的简称和全称
                            if (paper.Author[index].Equals(reprintauthor))
                            {
                                nameAbbre = reprintauthor;
                            }
                            #endregion
                        }
                        else if (index > paper.Author.Count && index < paper.FullNameofAuthor.Count)
                        {
                            dataRowAu["PGuid"] = paper.Guid;
                            dataRowAu["Year"] = paper.Year;
                            dataRowAu["Name"] = paper.FullNameofAuthor[index];
                            dataRowAu["Order"] = position++;
                            #region ReprintAuthor全称和简称
                            if (paper.FullNameofAuthor[index].Equals(reprintauthor))
                            {
                                name = reprintauthor;
                            }
                            #endregion
                        }
                        dataset.Tables["Author"].Rows.Add(dataRowAu.ItemArray);
                    }

                }

                #endregion

                #region category add row
                position = 1;
                if (!string.IsNullOrEmpty(paper.WebofScienceCategories))
                {
                    List<string> categories = paper.WebofScienceCategories.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                    dataRow["CategoryCount"] = categories.Count;
                    categories.ForEach(p =>
                    {
                        dataRowCa["PGuid"] = paper.Guid;
                        dataRowCa["Category"] = p.Trim();
                        dataRowCa["Type"] = "WebofScience";
                        dataRowCa["Year"] = paper.Year;
                        dataRowCa["Order"] = position++;
                        dataset.Tables["Category"].Rows.Add(dataRowCa.ItemArray);
                    });
                    categories = null;
                }
                position = 1;
                if (!string.IsNullOrEmpty(paper.ResearchAreas))
                {
                    List<string> researchareas = paper.ResearchAreas.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                    dataRow["ResearchAreaCount"] = researchareas.Count;
                    researchareas.ForEach(p =>
                    {
                        dataRowCa["PGuid"] = paper.Guid;
                        dataRowCa["Category"] = p.Trim();
                        dataRowCa["Type"] = "ResearchArea";
                        dataRowCa["Year"] = paper.Year;
                        dataRowCa["Order"] = position++;
                        dataset.Tables["Category"].Rows.Add(dataRowCa.ItemArray);
                    });

                }
                #endregion

                #region keyword add row
                position = 1;
                if (!string.IsNullOrEmpty(paper.Keywords))
                {
                    List<string> keywords = GetKeywords(paper.Keywords);
                    dataRow["KeywordCount"] = keywords.Count;
                    keywords.ForEach(p =>
                    {
                        dataRowKe["PGuid"] = paper.Guid;
                        dataRowKe["Keyword"] = p;
                        dataRowKe["Type"] = "Keyword";
                        dataRowKe["Year"] = paper.Year;
                        dataRowKe["Order"] = position++;
                        dataset.Tables["Keyword"].Rows.Add(dataRowKe.ItemArray);
                    });
                }
                position = 1;
                if (!string.IsNullOrEmpty(paper.KeywordsPlus))
                {
                    List<string> keywordsplus = GetKeywords(paper.KeywordsPlus);
                    dataRow["KeywordPlusCount"] = keywordsplus.Count;
                    keywordsplus.ForEach(p =>
                    {
                        dataRowKe["PGuid"] = paper.Guid;
                        dataRowKe["Keyword"] = p;
                        dataRowKe["Type"] = "KeywordPlus";
                        dataRowKe["Year"] = paper.Year;
                        dataRowKe["Order"] = position++;
                        dataset.Tables["Keyword"].Rows.Add(dataRowKe.ItemArray);
                    });
                }
                #endregion

                #region documenttype add row
                position = 1;
                if (!string.IsNullOrEmpty(paper.DocumentTypes))
                {
                    List<string> documenttypes = paper.DocumentTypes.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                    dataRow["DocumentTypeCount"] = documenttypes.Count;
                    documenttypes.ForEach(p =>
                    {
                        dataRowDo["PGuid"] = paper.Guid;
                        dataRowDo["DocumentType"] = p.Trim();
                        dataRowDo["Year"] = paper.Year;
                        dataRowDo["Order"] = position++;
                        dataset.Tables["DocumentType"].Rows.Add(dataRowDo.ItemArray);
                    });
                }
                #endregion

                #region reference add row
                position = 1;
                if (paper.Reference != null && paper.Reference.Count > 0)
                {
                    dataRow["ReferenceCount"] = paper.Reference.Count;
                    paper.Reference.ForEach(p =>
                    {
                        try
                        {
                            dataRowRe["Guid"] = Guid.NewGuid().ToString("N");
                            dataRowRe["PGuid"] = paper.Guid;
                            dataRowRe["Order"] = position++;
                            //还有其他行
                            reference = ReferenceManager.Gets(p);
                            dataRowRe["Author"] = reference.Author;
                            dataRowRe["Year"] = reference.Year;
                            dataRowRe["Journal"] = reference.Journal;
                            dataRowRe["Volume"] = reference.Volume;
                            dataRowRe["Page"] = reference.Page;
                            dataRowRe["Doi"] = reference.Doi;
                            dataset.Tables["Reference"].Rows.Add(dataRowRe.ItemArray);
                            reference = null;
                        }
                        catch (Exception)
                        {
                            throw new Exception("数据转换错误，参考文献：" + paper.FileName + ":" + p);
                        }
                    });
                }
                #endregion

                #region reprintauthor add row
                if (!string.IsNullOrEmpty(paper.ReprintAddress))
                {
                    try
                    {
                        dataRowRp["Name"] = name;
                        dataRowRp["NameAbbre"] = nameAbbre;
                        dataRowRp["PGuid"] = paper.Guid;
                        dataRowRp["Address"] = dataRowIn["Address"] = paper.ReprintAddress;
                        institute = new InstituteManager(GetReprintAddress(paper.ReprintAddress));
                        dataRowRp["Province"] = dataRowIn["Province"] = institute.Province;
                        dataRowRp["City"] = dataRowIn["City"] = institute.City;
                        dataRowRp["PostCode"] = dataRowIn["PostCode"] = institute.PostCode;
                        dataRowRp["Country"] = institute.Country;
                        dataRowRp["CountryNorm"] = institute.Country;
                        for (int j = 1; j < 6; j++)
                        {
                            dataRowRp[string.Format("ins{0}", j)] = dataRowIn[string.Format("ins{0}", j)] = InstituteManager.ins[j - 1];
                        }
                        institute = null;
                        dataset.Tables["ReprintAuthor"].Rows.Add(dataRowRp.ItemArray);
                    }
                    catch (Exception)
                    {
                        throw new Exception("数据转换错误，通讯作者信息：" + paper.FileName + ":" + paper.ReprintAddress);
                    }
                    //同时添加进机构   一些论文只有通讯作者数据，没有机构数据，需要将通讯作者数据添加进机构

                }
                #endregion

                #region institute add row  && authorinstitute row add inside
                if (paper.Address != null && paper.Address.Count > 0)
                {
                    #region
                    paper.Address.ForEach(p =>
                    {
                        try
                        {
                            dataRowIn["PGuid"] = paper.Guid;
                            strTemp = Guid.NewGuid().ToString("N");
                            dataRowIn["Guid"] = strTemp;
                            List<string> authorList = GetAuthorList(p);
                            if (authorList[0].Equals("a"))
                                authorList = paper.FullNameofAuthor;

                            authorList.ForEach(a =>
                            {
                                dataRowAI["Author"] = a;
                                int order = 0;
                                AuthorOrder.TryGetValue(a, out order);
                                if (order != 0)
                                {
                                    dataRowAI["Order"] = AuthorOrder[a];
                                    dataRowAI["IGuid"] = strTemp;
                                    dataRowAI["PGuid"] = paper.Guid;
                                    dataset.Tables["AuthorInstitute"].Rows.Add(dataRowAI.ItemArray);
                                }
                            });
                            string temp = GetInstituteString(p);
                            dataRowIn["Address"] = temp;
                            institute = new InstituteManager(temp);
                            dataRowIn["Province"] = institute.Province;
                            dataRowIn["City"] = institute.City;
                            dataRowIn["PostCode"] = institute.PostCode;
                            dataRowIn["Country"] = institute.Country;
                            dataRowIn["CountryNorm"] = dataRowIn["Country"].ToString();
                            for (int j = 1; j < 6; j++)
                            {
                                dataRowIn[string.Format("ins{0}", j)] = InstituteManager.ins[j - 1];
                            }
                            dataset.Tables["Institute"].Rows.Add(dataRowIn.ItemArray);
                        }
                        catch (Exception)
                        {
                            throw new Exception("数据转换错误，机构信息:" + paper.FileName + ":" + p);
                        }
                    });
                    #endregion
                }
                else if (paper.ReprintAddress != null)
                {
                    //当机构表为空是，将通讯作者表的数据补充到机构表中
                    try
                    {
                        strTemp = Guid.NewGuid().ToString("N");
                        dataRowIn["Guid"] = strTemp;
                        dataRowIn["PGuid"] = paper.Guid;
                        dataRowIn["Country"] = dataRowRp["Country"].ToString();
                        dataRowIn["CountryNorm"] = dataRowRp["CountryNorm"].ToString();
                        dataRowIn["Province"] = dataRowRp["Province"].ToString();
                        dataRowIn["City"] = dataRowRp["City"].ToString();
                        dataRowIn["PostCode"] = dataRowRp["PostCode"].ToString();
                        dataRowIn["ins1"] = dataRowRp["ins1"].ToString();
                        dataRowIn["ins2"] = dataRowRp["ins2"].ToString();
                        dataRowIn["ins3"] = dataRowRp["ins3"].ToString();
                        dataRowIn["ins4"] = dataRowRp["ins4"].ToString();
                        dataRowIn["ins5"] = dataRowRp["ins5"].ToString();
                        dataset.Tables["Institute"].Rows.Add(dataRowIn.ItemArray);

                        paper.FullNameofAuthor.ForEach(p =>
                        {
                            dataRowAI["IGuid"] = strTemp;
                            dataRowAI["Author"] = p;
                            AuthorOrder.TryGetValue(p, out intTemp);
                            dataRowAI["Order"] = intTemp;
                            dataRowAI["PGuid"] = paper.Guid;
                            dataset.Tables["AuthorInstitute"].Rows.Add(dataRowAI.ItemArray);
                        });
                    }
                    catch
                    {
                    }
                }
                else
                {
                    //通讯作者、机构都不存在
                }
                #endregion

                #region Fund add row
                if (!string.IsNullOrEmpty(paper.Funding))
                {
                    try
                    {
                        dataRowFu["PGuid"] = paper.Guid;

                        string[] funds = ConvertString(paper.Funding, ';', ',').Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        dataRow["FundCount"] = funds.Length;
                        for (int j = 0; j < funds.Length; j++)
                        {
                            dataRowFu["Order"] = j + 1;
                            if (funds[j].Contains('[')&&funds[j].Contains(']'))
                            {
                                dataRowFu["Funding"] = funds[j].Substring(0, funds[j].IndexOf('[')).Trim();
                                string[] codes = funds[j].Substring(funds[j].IndexOf('[') + 1, funds[j].IndexOf(']') - funds[j].IndexOf('[') - 1).Split(new char[] { ',' });
                                for (int c = 0; c < codes.Length; c++)
                                {
                                    dataRowFu["Code"] = codes[c].Trim();
                                    dataset.Tables["Fund"].Rows.Add(dataRowFu.ItemArray);
                                }
                            }
                            else
                            {
                                dataRowFu["Funding"] = funds[j].Trim();
                                dataRowFu["Code"] = string.Empty;
                                dataset.Tables["Fund"].Rows.Add(dataRowFu.ItemArray);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("数据转换错误，基金信息：" + paper.FileName + ":" + paper.Funding);
                    }
                }
                #endregion

                #region paper add row
                dataRow["Guid"] = paper.Guid;
                dataRow["Title"] = paper.Title;
                dataRow["TotalCites"] = paper.TotalCites;
                dataRow["Journal"] = paper.Journal;
                dataRow["Source"] = paper.Source;
                dataRow["Language"] = paper.Language;
                dataRow["Email"] = paper.Email;
                dataRow["Publisher"] = paper.Publisher;
                dataRow["CityofPublisher"] = paper.CityofPublisher;
                dataRow["AddressofPublisher"] = paper.AddressofPublisher;
                dataRow["JournalAbbreiation"] = paper.JournalAbbreiation;
                dataRow["ISSN"] = paper.ISSN;
                dataRow["Year"] = paper.Year;
                dataRow["Month"] = paper.Month;
                dataRow["Issue"] = paper.Issue;
                dataRow["Volume"] = paper.Volume;
                dataRow["PageBegin"] = paper.PageBegin;
                dataRow["PageEnd"] = paper.PageEnd;
                dataRow["DOI"] = ReplaceSingeQuote(paper.DOI);
                dataRow["IDSNumber"] = ReplaceSingeQuote(paper.IDSNumber);
                dataRow["AccessionNumber"] = ReplaceSingeQuote(paper.AccessionNumber);
                dataRow["PublishType"] = ReplaceSingeQuote(paper.PublishType);
                dataRow["ArticleNumber"] = paper.ArticleNumber;
                dataRow["AuthorCount"] = paper.Author != null ? paper.Author.Count : 0;
                dataRow["FullAuthorCount"] = paper.FullNameofAuthor != null ? paper.FullNameofAuthor.Count : 0;
                dataset.Tables["Paper"].Rows.Add(dataRow.ItemArray);

                #endregion
                #region 清除该论文内存
                paperList[i] = null;
                #endregion
            }

            dataset.Tables["Paper"].AcceptChanges();
            dataset.Tables["Abstract"].AcceptChanges();
            dataset.Tables["Author"].AcceptChanges();
            dataset.Tables["Category"].AcceptChanges();
            dataset.Tables["Keyword"].AcceptChanges();
            dataset.Tables["DocumentType"].AcceptChanges();
            dataset.Tables["Reference"].AcceptChanges();
            dataset.Tables["ReprintAuthor"].AcceptChanges();
            dataset.Tables["Institute"].AcceptChanges();
            dataset.Tables["AuthorInstitute"].AcceptChanges();
            paperList = null;
            GC.Collect();
            return dataset;
        }
        /// <summary>
        /// 将单引号保存成双引号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ReplaceSingeQuote(string input)
        {
            return input;
            //{
            //    if (!string.IsNullOrEmpty(input))
            //        return input;
            //    // return input.Replace("'", "''");
            //    else
            //        return null;
        }
        public static readonly Regex regex = new Regex(@"\(.*?\)");
        /// <summary>
        /// 关键词拆分：根据';'，但是当';'在'()'内时不拆分
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public static List<string> GetKeywords(string keyword)
        {
            keyword = RemoveWhiteSpace(keyword);
            if (keyword.Contains('('))
            {
                MatchCollection matches = regex.Matches(keyword);
                foreach (Match match in matches)
                {
                    if (!string.IsNullOrEmpty(match.Value))
                    {
                        string replacement = match.Value.Replace(';', '`');
                        keyword = keyword.Replace(match.Value, replacement);
                    }
                }
            }
            string[] keywords = keyword.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> result = null;
            if (keywords != null)
            {
                result = new List<string>(keywords.Length);
                foreach (string item in keywords)
                {
                    result.Add(item.Trim().Replace('`', ';'));
                }
            }
            return result;
        }
        /// <summary>
        /// 去掉标点符号[',',';',':','.']两边的空格
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string RemoveWhiteSpace(string field)
        {
            return field;
            if (!string.IsNullOrEmpty(field))
            {
                if (field.Contains(','))
                    field = Regex.Replace(field, @"\s*,\s*", ",");
                if (field.Contains(':'))
                    field = Regex.Replace(field, @"\s*:\s*", ":");
                if (field.Contains('.'))
                    field = Regex.Replace(field, @"\s*\.\s*", ".");
                if (field.Contains(';'))
                    field = Regex.Replace(field, @"\s*;\s*", ";");
                return field;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 从地址信息中获取作者信息
        /// </summary>
        /// <param name="address"></param>
        /// <returns>返回作者列表，如果没有作者信息，表示本文的所有作者都是相同的地址</returns>
        private static List<string> GetAuthorList(string address)
        {
            if (string.IsNullOrEmpty(address))
                return null;
            //包括【】
            List<string> result = new List<string>();
            if (address.Contains('['))
            {
                int start = address.IndexOf('[') + 1;
                int end = address.IndexOf(']');

                string strAuthor = address.Substring(start, end - start);
                result = strAuthor.Split(new char[] { ';' }).ToList<string>();

                //result.ForEach(p => p = p.Trim());
                for (int i = 0; i < result.Count; i++)
                {
                    result[i] = result[i].Trim();
                }
            }
            else if (address.Contains(';'))
            {
                int end = address.LastIndexOf(';');

                string strAuthor = address.Substring(0, end);
                result = strAuthor.Split(new char[] { ';' }).ToList<string>();

                for (int i = 0; i < result.Count; i++)
                {
                    result[i] = result[i].Trim();
                }
            }
            else
            {
                result.Add("a");
            }
            return result;

            //int start = address.IndexOf('[') + 1;
            //int end = address.IndexOf(']');
            //if (end < start)
            //{
            //    return new List<string> { "a" };

            //}
            //string strAuthor = address.Substring(start, end - start);
            //List<string> result = strAuthor.Split(new char[] { ';' }).ToList<string>();
            //return result;
        }
        /// <summary>
        /// 从地址信息中获取机构信息
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private static string GetInstituteString(string address)
        {
            if (address == null)
            {
                return string.Empty;
            }
            else if (address.Contains("]"))
            {
                return address.Substring(address.IndexOf("]") + 1);
            }
            else if (address.Contains(";"))
            {
                return address.Substring(address.LastIndexOf(";") + 1);
            }
            else
            {
                return address;
            }
        }
        /// <summary>
        /// 将[]中的分号，替换成指定符号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        private static string ConvertString(string input, char oldchar, char newchar)
        {
            Regex regx = new Regex(@"\[.*?\]");
            MatchCollection matchcollection = regx.Matches(input);
            foreach (Match item in matchcollection)
            {
                string temp = item.Value.Replace(oldchar, newchar);
                input = input.Replace(item.Value, temp);
            }
            return input;

        }
        /// <summary>
        /// 获取通讯作者名字
        /// </summary>
        /// <param name="reprintauthoraddress"></param>
        /// <returns></returns>
        private static string GetReprintAuthor(string reprintauthoraddress)
        {
            if (!string.IsNullOrEmpty(reprintauthoraddress) && reprintauthoraddress.Contains("("))
            {
                int index = reprintauthoraddress.IndexOf("(");
                return reprintauthoraddress.Substring(0, index).Trim();

            }
            else
            {
                return reprintauthoraddress;
            }
        }
        private static string GetReprintAddress(string reprintauthoraddress)
        {
            if (!string.IsNullOrEmpty(reprintauthoraddress) && reprintauthoraddress.Contains(")"))
            {
                int index = reprintauthoraddress.IndexOf(")");
                return reprintauthoraddress.Substring(index + 1);
            }
            else
            {
                return reprintauthoraddress;
            }
        }
    }
}
