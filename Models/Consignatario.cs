﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Consignatario
    {

        [Key]
        public string Id { get; set; }

        [Required]
        public string Nombre{ get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Contacto { get; set; }

        [Required]
        public string Direccion { get; set; }


        [Required]
        public string Email { get; set; }


        [Required]
        public string LegalId { get; set; }



    }
}
