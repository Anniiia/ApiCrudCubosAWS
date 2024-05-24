using ApiCrudPersonajes2AWS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCrudPersonajes2AWS.Data
{
    public class CubosContext : DbContext
    {
        public CubosContext(DbContextOptions<CubosContext> options)
            : base(options) { }

        public DbSet<Cubo> Cubos { get; set; }
    }

}
