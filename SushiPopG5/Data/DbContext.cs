using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SushiPopG5.Models;

    public class DbContext : IdentityDbContext
    {
        public DbContext (DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public DbSet<SushiPopG5.Models.Usuario> Usuario { get; set; } = default!;

        public DbSet<SushiPopG5.Models.Empleado>? Empleado { get; set; }

        public DbSet<SushiPopG5.Models.Cliente>? Cliente { get; set; }

        public DbSet<SushiPopG5.Models.Carrito>? Carrito { get; set; }

        public DbSet<SushiPopG5.Models.CarritoItem>? CarritoItem { get; set; }

        public DbSet<SushiPopG5.Models.Categoria>? Categoria { get; set; }

        public DbSet<SushiPopG5.Models.Contacto>? Contacto { get; set; }

        public DbSet<SushiPopG5.Models.Descuento>? Descuento { get; set; }

        public DbSet<SushiPopG5.Models.Pedido>? Pedido { get; set; }

        public DbSet<SushiPopG5.Models.Producto>? Producto { get; set; }

        public DbSet<SushiPopG5.Models.Reclamo>? Reclamo { get; set; }

        public DbSet<SushiPopG5.Models.Reserva>? Reserva { get; set; }
    }
