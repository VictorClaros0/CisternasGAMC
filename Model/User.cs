using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CisternasGAMC.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short UserId { get; set; }

        [StringLength(30, ErrorMessage = "Formato incorrecto")]
        [Required(ErrorMessage = "FirstName es obligatorio.")]
        public string FirstName { get; set; }

        [StringLength(40, ErrorMessage = "Formato incorrecto")]
        [Required(ErrorMessage = "LastName es obligatorio.")]
        public string LastName { get; set; }

        [StringLength(30, ErrorMessage = "Formato incorrecto")]
        [Required(ErrorMessage = "Email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        public string Email { get; set; }

        [StringLength(25, ErrorMessage = "Formato incorrecto")]
        [Required(ErrorMessage = "Password es obligatorio.")]
        public string Password { get; set; }

        [StringLength(8, ErrorMessage = "Formato incorrecto")]
        [Required(ErrorMessage = "Número de teléfono inválido.")]
        public string PhoneNumber { get; set; }

        public byte Role { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime RegisterDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastUpdate { get; set; }

        [Required]
        public byte Status { get; set; }
    }
}
