using ETLCabData.Models;

namespace ETLCabData.Services
{
    public class DataTransformer
    {
        public CabTrip Transform(CabTrip record)
        {
            record.PassengerCount ??= 0;

            record.StoreAndFwdFlag = record.StoreAndFwdFlag.Trim();

            record.StoreAndFwdFlag = record.StoreAndFwdFlag == "Y" ? "Yes" : "No";

            // Convert timestamps from EST to UTC
            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            record.TpepPickupDatetime = TimeZoneInfo.ConvertTimeToUtc(record.TpepPickupDatetime, estZone);
            record.TpepDropoffDatetime = TimeZoneInfo.ConvertTimeToUtc(record.TpepDropoffDatetime, estZone);

            return record;
        }
    }
}
