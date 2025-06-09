using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace AutomationAssessment.Utils
{
    public class ReadTestData
    {
        public static List<RegisterUserModel> ParseTestData()
        {
            using var reader = new StreamReader("Data/test_data.csv");
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            return csv.GetRecords<RegisterUserModel>().ToList();
        }
    }
}
