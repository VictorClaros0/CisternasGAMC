using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CisternasGAMC.Model
{
    public class Otb
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short OtbId { get; set; }

        [StringLength(60, ErrorMessage = "Formato incorrecto")]
        [Required(ErrorMessage = "Name es obligatorio.")]
        public string Name { get; set; }

        [Range(0, 32767, ErrorMessage = "El número de familias debe ser positivo.")]
        [Required]
        public short FamilyCount { get; set; }

        [Required]        
        public byte District { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastUpdate { get; set; }
    }
}
