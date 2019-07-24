using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;

namespace MyTest
{
    public class ExcelReader
    {
        public List<Voice> SelectMenuTable(string path, string sheetName)
        {
            DataRowCollection collect = ReadExcel(path, sheetName);
            List<Voice> menuArray = new List<Voice>();
            for (var i = 0; i < collect.Count; i++)
            {
                if (collect[i][1].ToString() == "") continue;

                Voice voice = new Voice
                {
                    Name = collect[i][0].ToString(),
                    English = collect[i][1].ToString(),
                    Chinese = collect[i][2].ToString()
                };
                menuArray.Add(voice);
            }

            return menuArray;
        }


        DataRowCollection ReadExcel(string path, string sheetName)
        {
            var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);

            var excelReader = path.EndsWith(".xls")
                ? ExcelReaderFactory.CreateBinaryReader(stream)
                : ExcelReaderFactory.CreateOpenXmlReader(stream);

            var result = excelReader.AsDataSet();
            //int columns = result.Tables[0].Columns.Count;
            //int rows = result.Tables[0].Rows.Count;

            //tables可以按照sheet名获取，也可以按照sheet索引获取
            //return result.Tables[0].Rows;
            return result.Tables[sheetName].Rows;
        }
    }

    [System.Serializable]
    public class Voice
    {
        public string Name;
        public string Chinese;
        public string English;
    }
}