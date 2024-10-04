namespace CisternasGAMC.Model
{
    public class WaterDelivery
    {
        public int WaterDeliveryId {  get; set; }
        public int CisternId { get; set; }
        public int DriverId { get; set; }
        public int OtbId {  get; set; }
        public DateTime DeliveryDate { get; set; }
        public double DeliveredAmount { get; set; }
        public string DeliveryStatus { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }
    }
}
