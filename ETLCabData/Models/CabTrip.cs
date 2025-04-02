namespace ETLCabData.Models
{
    public class CabTrip
    {
        public DateTime TpepPickupDatetime { get; set; }
        public DateTime TpepDropoffDatetime { get; set; }
        public byte? PassengerCount { get; set; }
        public decimal TripDistance { get; set; }
        public string StoreAndFwdFlag { get; set; }
        public int PULocationID { get; set; }
        public int DOLocationID { get; set; }
        public decimal FareAmount { get; set; }
        public decimal TipAmount { get; set; }
    }
}
