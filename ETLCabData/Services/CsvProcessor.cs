using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using ETLCabData.Models;

namespace ETLCabData.Services
{
    // Mapping configuration for CsvHelper
    public class CabTripMap : ClassMap<CabTrip>
    {
        public CabTripMap()
        {
            Map(m => m.TpepPickupDatetime).Name("tpep_pickup_datetime");
            Map(m => m.TpepDropoffDatetime).Name("tpep_dropoff_datetime");
            Map(m => m.PassengerCount).Name("passenger_count");
            Map(m => m.TripDistance).Name("trip_distance");
            Map(m => m.StoreAndFwdFlag).Name("store_and_fwd_flag");
            Map(m => m.PULocationID).Name("PULocationID");
            Map(m => m.DOLocationID).Name("DOLocationID");
            Map(m => m.FareAmount).Name("fare_amount");
            Map(m => m.TipAmount).Name("tip_amount");
        }
    }

    public class CsvProcessor(IDataTransformer transformer) : ICsvProcessor
    {
        // Reads CSV file and transforms each record using DataTransformer
        private readonly IDataTransformer _transformer = transformer;

        public IEnumerable<CabTrip> ReadCsv(string filePath)
        {
            using var reader = new StreamReader(filePath);

            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Context.RegisterClassMap<CabTripMap>();

            // Iterate over each record, transform it and yield return
            foreach (var record in csv.GetRecords<CabTrip>())
                yield return _transformer.Transform(record);
        }
    }
}
