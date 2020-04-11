using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ShippingInstruction
    {

        [Key]
        public long Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }


        [Required]
        [ForeignKey("Cliente")]
        public string ClienteId { get; set; }     
        public Cliente Cliente {get; set;}



        [Required]
        [ForeignKey("Agente")]
        public string AgenteId { get; set; }
        public Agente Agente { get; set; }


        [Required]
        [ForeignKey("Consignatario")]
        public string ConsignatarioId { get; set; }
        public Consignatario Consignatario { get; set; }


        [Required]
        [ForeignKey("Shipper")]
        public string ShipperId { get; set; }
        public Shipper Shipper { get; set; }


  
        [ForeignKey("Commodity")]
        public string CommodityId { get; set; }
        public Commodity Commodity { get; set; }


        [Required]
        public string LugarDeCarga { get; set; }


        [Required]
        [ForeignKey("PuertoDeDespacho")]
        public string PuertoDeDespachoId { get; set; }
        public Puerto PuertoDeDespacho { get; set; }

        [Required]
        [ForeignKey("PuertoDeDescarga")]
        public string PuertoDeDescargaId { get; set; }
        public Puerto PuertoDeDescarga { get; set; }

        [Required]
        public string DestinoFinal { get; set; }

        [Required]
        [ForeignKey("Region")]
        public string RegionId { get; set; }
        public Region Region { get; set; }



        [Required]
        public string Country { get; set; }



        [Required]
        public string Volume { get; set; }


        [Required]
        public string Mercancia { get; set; }

        [Required]
        [ForeignKey("Carrier")]
        public string CarrierId { get; set; }
        public Carrier Carrier { get; set; }





        [Required]
        public string Incoterm { get; set; }

        public List<ShippingInstructionDetail> ShippingInstructionDetails { get; set; }


        [Required]
        public decimal Costo { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public string Nominacion { get; set; }


        [Required]
        [ForeignKey("Usuario")]
        public string VendedorId { get; set; }
        public Usuario Usuario { get; set; }



        [Required]
        public string ShipmentType { get; set; }

        [Required]
        public string WayBillType { get; set; }

        [Required]
        public string ServiceType { get; set; }

        [Required]
        public string PPCC { get; set; }

        [Required]
        public string FCLLCL { get; set; }

        [Required]
        public string Comentarios { get; set; }

      
        public string PrintToken { get; set; }


    }
}
