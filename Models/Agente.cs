using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Agente
    {

        [Key]
        public string Id { get; set; }

        [Required]
        public string Nombre{ get; set; }
        [Required]
        public string Contact{ get; set; }
        [Required]
        public string Email{ get; set; }
        [Required]
        public string Phone{ get; set; }


    }
}
