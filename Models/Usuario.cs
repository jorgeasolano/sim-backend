using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Usuario
    {

        [Key]
        public string Id { get; set; }

        [Required]
        public string Nombre{ get; set; }
        [Required]
        public string CorreoElectronico { get; set; }
        [Required]
        public string Contrasena { get; set; }
        [Required]

        public Boolean EsAdmin { get; set; }

        public Boolean EsActivo { get; set; }

    }
}
