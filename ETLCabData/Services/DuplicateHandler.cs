﻿using CsvHelper;
using ETLCabData.Models;
using System.Globalization;

namespace ETLCabData.Services
{
    public static class DuplicateHandler
    {
        public static List<CabTrip> RemoveDuplicates(IEnumerable<CabTrip> trips, out List<CabTrip> duplicates)
        {
            var uniqueTrips = new List<CabTrip>();
            duplicates = new List<CabTrip>();
            var seenKeys = new HashSet<string>();

            foreach (var trip in trips)
            {
                string key = $"{trip.TpepPickupDatetime}-{trip.TpepDropoffDatetime}-{trip.PassengerCount}";

                if (!seenKeys.Add(key))
                    duplicates.Add(trip);
                else
                    uniqueTrips.Add(trip);
            }

            return uniqueTrips;
        }

        public static void WriteDuplicates(string filePath, List<CabTrip> duplicates)
        {
            using var writer = new StreamWriter(filePath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(duplicates);
        }
    }
}
