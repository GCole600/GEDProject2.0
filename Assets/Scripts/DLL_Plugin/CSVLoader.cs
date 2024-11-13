using CSVReaderPlugin;
using System.Collections.Generic;
using SingletonPattern;

namespace DLL_Plugin
{
    public class CSVLoader : Singleton<CSVLoader>
    {
        private CSVReader _csvReader;

        private List<Dictionary<string, string>> _data;

        private void Start()
        {
            _csvReader = new CSVReader();
        }

        public void LoadCSV(string filePath)
        {
            // Read the CSV file
            _data = _csvReader.ReadCSV(filePath);
        }

        public int GetValueAt(int rowIndex, string columnName)
        {
            var row = _data[rowIndex];
            var value = row.ContainsKey(columnName) ? row[columnName] : null;
            return value != null ? int.Parse(value) : 0;
        } 
    }
}