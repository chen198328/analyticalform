using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace AnalyticalPlatform
{
    public class ReferenceManager
    {
        private static readonly Regex regYear = new Regex(@"\d{4}");
        private static readonly Regex regVolume = new Regex(@"\sV[0-9]+", RegexOptions.IgnoreCase);
        private static readonly Regex regPage = new Regex(@"\sP[0-9]+", RegexOptions.IgnoreCase);
        private static readonly Regex regDoi = new Regex(@"\sDOI.+", RegexOptions.IgnoreCase);
        private static readonly int Year = DateTime.Now.Year;
        public static Reference Get(string input)
        {
            Reference reference = new Reference();
            bool isyear = false;
            bool isvolume = false;
            bool ispage = false;

            string strTemp = string.Empty;
            int position = 0;
            string[] inputs = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (inputs.Length > 0)
            {
                int index = 0;
                reference.Author = inputs[index];
                index++;
                position++;
                while (index < inputs.Length)
                {
                    strTemp = inputs[index++].Trim();
                    if (!isyear && regYear.IsMatch(strTemp))
                    {
                        reference.Year = int.Parse(regYear.Match(strTemp).Value);

                        isyear = true;
                        continue;
                    }
                    else if (!isvolume && regVolume.IsMatch(strTemp))
                    {
                        reference.Volume = int.Parse(regVolume.Match(strTemp).Value.Substring(1));
                        isvolume = true;
                        continue;
                    }
                    else if (!ispage && regPage.IsMatch(strTemp))
                    {
                        reference.Page = int.Parse(regPage.Match(strTemp).Value.Substring(1));
                        ispage = true;
                        continue;
                    } if (regDoi.IsMatch(strTemp))
                    {
                        reference.Doi = regDoi.Match(strTemp).Value.Substring(4);
                        continue;
                    }
                    else
                    {
                        reference.Journal = strTemp;
                    }

                }
            }
            return reference;

        }
        public static Reference Gets(string input)
        {
            
                Reference reference = new Reference();
                string strTemp = string.Empty;
                try
                {
                if (regYear.IsMatch(input))
                {
                    strTemp = regYear.Match(input).Value.Trim().Substring(0, 4);
                    reference.Year = int.Parse(strTemp);
                    if (reference.Year > Year)
                    {
                        reference.Year = 0;
                        Match match=regYear.Match(input).NextMatch();
                        if ( match!= null&&!string.IsNullOrEmpty(match.Value))
                        {
                            
                            strTemp = match.Value.Substring(0, 4);
                            reference.Year = int.Parse(strTemp);
                            if (reference.Year > Year)
                                reference.Year = 0;
                        }
                    }

                }
                if (regVolume.IsMatch(input))
                {
                    strTemp = regVolume.Match(input).Value.Substring(2);
                    reference.Volume = int.Parse(strTemp);
                    input = regVolume.Replace(input, "");
                }
                if (regPage.IsMatch(input))
                {
                    strTemp = regPage.Match(input).Value.Substring(2);
                    reference.Page = int.Parse(strTemp);
                    input = regPage.Replace(input, "");
                }
                if (regDoi.IsMatch(input))
                {
                    strTemp = regDoi.Match(input).Value;
                    reference.Doi = strTemp.Substring(5);
                    input = regDoi.Replace(input, "");
                }
            }
            catch (Exception) {
                string b = input;
            }
            string[] inputs = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            try{
            if (inputs.Length > 2)
            {
                reference.Journal = inputs[2].Trim();
                reference.Author = inputs[0].Trim();
            }
            else if (inputs.Length == 2)
            {
                if (inputs[1] == reference.Year.ToString())
                {
                    reference.Author = inputs[0].Trim();
                }
                else if (inputs[0] == reference.Year.ToString())
                {
                    reference.Journal = inputs[1].Trim();
                }
                else
                {
                    reference.Author = inputs[0].Trim();
                    int year = 0;
                    int.TryParse(inputs[1].Trim(), out year);
                    if (year > 0)
                    {
                        reference.Year = year;
                    }
                    else
                    {
                        reference.Journal = inputs[1];
                    }
                }
            }
            else if (inputs.Length == 1)
            {
                int year = 0;
                int.TryParse(inputs[0].Trim(), out year);
                if (Year>=year&&year > 0)
                {
                    reference.Year = year;
                }
                else
                {
                    reference.Author = inputs[0];
                }
            }
            }
            catch (Exception)
            {
                string[]a=inputs;
            }
            return reference;
        }
    }
    public class Reference
    {
        public string Author { set; get; }
        public int Year { set; get; }
        public string Journal { set; get; }
        public int Volume { set; get; }
        public int Page { set; get; }
        public string Doi { set; get; }
        public Reference()
        {
            Author = Journal = Doi = string.Empty;
        }
    }
}
