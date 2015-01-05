using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace AnalyticalPlatform
{
    public class ConnectionIO
    {
        public static string GetFile(string name)
        {
            try
            {
                if (File.Exists(name))
                {
                    using (StreamReader reader = new StreamReader(name))
                    {
                        return reader.ReadLine();
                    }
                }
                return null;
            }
            catch (IOException)
            {
                return null;
            }
        }
        public static bool WriteFile(string path, string content)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, false, Encoding.Default))
                {
                    writer.Write(content);
                    writer.Flush();
                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
        }
    }
}
