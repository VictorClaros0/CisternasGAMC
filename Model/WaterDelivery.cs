using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CisternasGAMC.Model
{
    public class WaterDelivery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WaterDeliveryId { get; set; }

        [ForeignKey("Cistern")]
        public byte CisternId { get; set; }

        [ForeignKey("User")]
        public short DriverId { get; set; }

        [ForeignKey("Otb")]
        public short OtbId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DeliveryDate { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "La cantidad debe ser un valor positivo.")]
        public float DeliveredAmount { get; set; }

        [Required]
        public byte DeliveryStatus { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ArrivalDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DepartureDate { get; set; }
    }
}