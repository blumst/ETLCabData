using ETLCabData.Models;
using ETLCabData.Services;
using Microsoft.Extensions.Configuration;

namespace ETLCabData
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("CabDataDb");

            string csvFilePath = "sample-cab-data.csv";
            string duplicatesFilePath = "duplicates.csv";

            var csvProcessor = new CsvProcessor();
            IEnumerable<CabTrip> records = csvProcessor.ReadCsv(csvFilePath);

            List<CabTrip> uniqueRecords = DuplicateHandler.RemoveDuplicates(records, out List<CabTrip> duplicates);

            DuplicateHandler.WriteDuplicates(duplicatesFilePath, duplicates);

            var bulkInserter = new SqlBulkInserter();
            bulkInserter.BulkInsert(connectionString, uniqueRecords);

            Console.WriteLine($"Successfully loaded {uniqueRecords.Count} unique records into the database.");
        }
    }
}
