using System.IO;
using System.Text;

namespace AnalyticsData
{
    public static class CSVSerializer
    {
        private static string FileName = "Analytics";
        private static string Path = ".";
        public static string Create(Diagnostics data)
        {
            var filePath =  Path + "/" + FileName + ".csv";
            string content =  data.SerializeToCSV();
            
            var header = "Participant,Total Time,Total Fails,First Decode,Second Decode,Third Decode,Fails Lvl 1,Fails Lvl 2, Fails Lvl 2";

            if (File.Exists(filePath))
            {
                using var sw1 = new StreamWriter(filePath, true, Encoding.UTF8);
                sw1.WriteLine(content);
                sw1.Flush();
                sw1.Close();
                return filePath;
            }
            using var sw = new StreamWriter(filePath, false, Encoding.UTF8);
            sw.WriteLine(header);
            sw.WriteLine(content);
            sw.Flush();
            sw.Close();

            return filePath;
        }

    }
}