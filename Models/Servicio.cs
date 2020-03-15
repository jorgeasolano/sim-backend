using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Servicio
    {

        [Key]
        public string Id { get; set; }

        [Required]
        public string Nombre{ get; set; }

        [Required]
        public decimal Costo { get; set; }

        [Required]
        public decimal Precio { get; set; }



    }
}
