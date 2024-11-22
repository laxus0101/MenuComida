using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MenuComida.Models;

namespace MenuComida.Data
{
    public class MenuComidaContext : DbContext
    {
        public MenuComidaContext (DbContextOptions<MenuComidaContext> options)
            : base(options)
        {
        }

        public DbSet<MenuComida.Models.Pizzas> Pizzas { get; set; } = default!;
        public DbSet<MenuComida.Models.Usuarios> Usuarios { get; set; } = default!;
        public DbSet<MenuComida.Models.carrito> carrito { get; set; } = default!;
    }
}
