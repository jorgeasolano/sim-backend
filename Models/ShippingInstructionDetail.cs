using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ShippingInstructionDetail
    {

        [Key]
        public long Id { get; set; }

        [Required]
        public decimal Cantidad { get; set; }


        [Required]
        [ForeignKey("Servicio")]
        public string ServicioId { get; set; }
        public Servicio Servicio { get; set; }


        [Required]
        public decimal Costo { get; set; }

        [Required]
        public decimal SubTotal { get; set; }





    }
}
