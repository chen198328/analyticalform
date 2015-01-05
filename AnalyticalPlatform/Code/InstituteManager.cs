using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace AnalyticalPlatform
{
    public class InstituteManager
    {
        public string Province { set; get; }
        public string City { set; get; }
        public string PostCode { set; get; }
        public static string[] ins = new string[5];
        public string Country { set; get; }
        public readonly static Regex regex = new Regex(@"\S*\d+\S*");
        public InstituteManager(string address)
        {
            if (string.IsNullOrEmpty(address))
                return;
            string[] addressList = address.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int count = addressList.Length;
            Country = GetCountry(addressList[count - 1]);
            if (Country.ToUpper().Equals("USA"))
            {
                string temp = addressList[count - 1];
                Match match = regex.Match(temp);
                if (match != null)
                {
                    PostCode = match.Value;
                }
                if (!string.IsNullOrEmpty(PostCode))
                {
                    Province = temp.Replace(PostCode, "").Replace("USA", "").Trim();
                }
                City = addressList[count - 2].Trim();

            }
            else
            {
                string temp = string.Format("{0}{1}", addressList[count - 2], addressList[count - 3]);
                Match match = regex.Match(temp);
                if (match != null)
                    PostCode = match.Value;
                if (!string.IsNullOrEmpty(PostCode))
                {
                    Province = addressList[count - 2].Replace(PostCode, "").Trim();
                    City = addressList[count - 3].Replace(PostCode, "").Trim();
                }
                else
                {
                    Province = addressList[count - 2].Trim();
                    City = addressList[count - 3].Trim();
                }
                //zip city

            }
            for (int i = 0; i < 5; i++)
            {
                ins[i] = string.Empty;
            }
            int max = addressList.Length > 5 ? 5 : addressList.Length;
            for (int i = 0; i < max; i++)
            {
                ins[i] = addressList[i].Trim();
            }



        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reprintAddress"></param>
        /// <returns>index 0 country,index 1 province,index 2 postcode</returns>
        private string GetCountry(string reprintAddress)
        {
            int startindex = reprintAddress.LastIndexOf(',') + 1;
            string country = reprintAddress.Substring(startindex);
            country = country.Contains("USA") ? "USA" : country.Trim();
            startindex = country.IndexOf('.');
            while (startindex != -1)
            {
                country = country.Remove(startindex, 1);
                startindex = country.IndexOf('.');
            }
            return country;

        }
    }
}
