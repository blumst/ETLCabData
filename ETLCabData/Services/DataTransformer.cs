using ETLCabData.Models;

namespace ETLCabData.Services
{
    public class DataTransformer : IDataTransformer
    {
        public CabTrip Transform(CabTrip record)
        {
            record.PassengerCount ??= 0;

            record.StoreAndFwdFlag = record.StoreAndFwdFlag.Trim();

            record.StoreAndFwdFlag = record.StoreAndFwdFlag == "Y" ? Constants.YesValue : Constants.NoValue;

            // Convert timestamps from EST to UTC
            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById(Constants.EasternTimeZoneId);
            record.TpepPickupDatetime = TimeZoneInfo.ConvertTimeToUtc(record.TpepPickupDatetime, estZone);
            record.TpepDropoffDatetime = TimeZoneInfo.ConvertTimeToUtc(record.TpepDropoffDatetime, estZone);

            return record;
        }
    }
}
