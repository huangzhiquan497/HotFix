using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using UnityEngine;

namespace MyTest
{
    public class ExcelReader
    {
        private string Name = "Voice";

        public List<Voice> SelectMenuTable()
        {
            var excelName = Name + ".xls";
            var sheetName = "sheet1";

            DataRowCollection collect = ReadExcel(excelName, sheetName);
            List<Voice> menuArray = new List<Voice>();
            for (int i = 1; i < collect.Count; i++)
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


        DataRowCollection ReadExcel(string excelName, string sheetName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), excelName);
            FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateBinaryReader(stream);

            DataSet result = excelReader.AsDataSet();
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