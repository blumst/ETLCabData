using ETLCabData.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ETLCabData.Services
{
    public class SqlBulkInserter
    {
        // Performs bulk insertion of CabTrip records into SQL Server using SqlBulkCopy
        public void BulkInsert(string connectionString, List<CabTrip> trips)
        {
            DataTable table = new();

            table.Columns.Add("tpep_pickup_datetime", typeof(DateTime));
            table.Columns.Add("tpep_dropoff_datetime", typeof(DateTime));
            table.Columns.Add("passenger_count", typeof(byte));
            table.Columns.Add("trip_distance", typeof(decimal));
            table.Columns.Add("store_and_fwd_flag", typeof(string));
            table.Columns.Add("PULocationID", typeof(int));
            table.Columns.Add("DOLocationID", typeof(int));
            table.Columns.Add("fare_amount", typeof(decimal));
            table.Columns.Add("tip_amount", typeof(decimal));

            foreach (var trip in trips)
            {
                table.Rows.Add(
                    trip.TpepPickupDatetime,
                    trip.TpepDropoffDatetime,
                    trip.PassengerCount,
                    trip.TripDistance,
                    trip.StoreAndFwdFlag,
                    trip.PULocationID,
                    trip.DOLocationID,
                    trip.FareAmount,
                    trip.TipAmount
                );
            }

            using var connection = new SqlConnection(connectionString);
            connection.Open();

            // Initialize SqlBulkCopy to efficiently insert data
            using var bulkCopy = new SqlBulkCopy(connection);
            bulkCopy.DestinationTableName = "dbo.CabTrips";

            bulkCopy.ColumnMappings.Add("tpep_pickup_datetime", "tpep_pickup_datetime");
            bulkCopy.ColumnMappings.Add("tpep_dropoff_datetime", "tpep_dropoff_datetime");
            bulkCopy.ColumnMappings.Add("passenger_count", "passenger_count");
            bulkCopy.ColumnMappings.Add("trip_distance", "trip_distance");
            bulkCopy.ColumnMappings.Add("store_and_fwd_flag", "store_and_fwd_flag");
            bulkCopy.ColumnMappings.Add("PULocationID", "PULocationID");
            bulkCopy.ColumnMappings.Add("DOLocationID", "DOLocationID");
            bulkCopy.ColumnMappings.Add("fare_amount", "fare_amount");
            bulkCopy.ColumnMappings.Add("tip_amount", "tip_amount");

            // Write the DataTable data to the SQL Server table
            bulkCopy.WriteToServer(table);
        }
    }
}
