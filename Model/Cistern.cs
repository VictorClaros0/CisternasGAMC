using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CisternasGAMC.Model
{
    public class Cistern
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte CisternId { get; set; }

        [Required]
        public byte Status { get; set; }

        [StringLength(7, ErrorMessage = "Formato incorrecto", MinimumLength = 6)]
        [Required(ErrorMessage = "PlateNumber es obligatorio.")]
        public string PlateNumber { get; set; }

        [Range(0, 32767, ErrorMessage = "La capacidad debe ser un número positivo.")]
        [Required]
        public short Capacity { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime RegisterDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastUpdate { get; set; }
    }
}
