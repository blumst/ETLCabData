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
            const string csvFilePath = "sample-cab-data.csv";
            const string duplicatesFilePath = "duplicates.csv";

            if (!File.Exists(csvFilePath))
            {
                Console.WriteLine("CSV file not found: " + csvFilePath);
                return;
            }

            try
            {
                IDataTransformer transformer = new DataTransformer();
                ICsvProcessor csvProcessor = new CsvProcessor(transformer);
                IDuplicateHandler duplicateHandler = new DuplicateHandler();
                IDbInserter dbInserter = new SqlBulkInserter();

                // Read CSV, transform records, and remove duplicates.
                IEnumerable<CabTrip> records = csvProcessor.ReadCsv(csvFilePath);
                List<CabTrip> uniqueRecords = duplicateHandler.RemoveDuplicates(records, out List<CabTrip> duplicates);

                duplicateHandler.WriteDuplicates(duplicatesFilePath, duplicates);

                // Bulk insert unique records into the SQL database.
                dbInserter.BulkInsert(connectionString, uniqueRecords);

                Console.WriteLine($"Successfully loaded {uniqueRecords.Count} unique records into the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
