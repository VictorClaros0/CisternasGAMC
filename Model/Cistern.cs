namespace CisternasGAMC.Model
{
    public class Cistern
    {
        public int CisternId {  get; set; }
        public string Status {  get; set; }
        public string PlateNumber { get; set; }
        public int Capacity { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}
