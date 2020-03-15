using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace WebAPI.Models
{
    public class APIContext : DbContext     
    {

        public APIContext(DbContextOptions<APIContext> options)
            : base(options)
        {

            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

   


        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Consignatario> Consignatarios { get; set; }
        public virtual DbSet<Agente> Agentes { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Puerto> Puertos { get; set; }
        public virtual DbSet<Destino> Destinos { get; set; }
        public virtual DbSet<Commodity> Commodities { get; set; }

        public virtual DbSet<Region> Regiones { get; set; }
        public virtual DbSet<Carrier> Carriers { get; set; }
        public virtual DbSet<Servicio> Servicios { get; set; }
        public virtual DbSet<ShippingInstruction> ShippingInstructions { get; set; }
        public virtual DbSet<ShippingInstructionDetail> ShippingInstructionDetails { get; set; }


    }
}
