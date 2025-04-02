using ETLCabData.Models;

namespace ETLCabData.Services
{
    public class DataTransformer
    {
        public CabTrip Transform(CabTrip record)
        {
            record.StoreAndFwdFlag = record.StoreAndFwdFlag.Trim();

            record.StoreAndFwdFlag = record.StoreAndFwdFlag == "Y" ? "Yes" : "No";

            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            record.TpepPickupDatetime = TimeZoneInfo.ConvertTimeFromUtc(record.TpepPickupDatetime, estZone);
            record.TpepDropoffDatetime = TimeZoneInfo.ConvertTimeFromUtc(record.TpepDropoffDatetime, estZone);

            return record;
        }
    }
}
