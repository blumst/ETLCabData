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

            // Define file paths for input CSV and duplicates output
            string csvFilePath = "sample-cab-data.csv";
            string duplicatesFilePath = "duplicates.csv";

            if (!File.Exists(csvFilePath))
            {
                Console.WriteLine("CSV file not found: " + csvFilePath);
                return;
            }

            try
            {
                // Read CSV data and transform each record
                var csvProcessor = new CsvProcessor();
                IEnumerable<CabTrip> records = csvProcessor.ReadCsv(csvFilePath);

                // Remove duplicate records based on defined key and capture duplicates
                List<CabTrip> uniqueRecords = DuplicateHandler.RemoveDuplicates(records, out List<CabTrip> duplicates);

                DuplicateHandler.WriteDuplicates(duplicatesFilePath, duplicates);

                // Perform bulk insertion of unique records into the SQL database
                var bulkInserter = new SqlBulkInserter();
                bulkInserter.BulkInsert(connectionString, uniqueRecords);

                Console.WriteLine($"Successfully loaded {uniqueRecords.Count} unique records into the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
