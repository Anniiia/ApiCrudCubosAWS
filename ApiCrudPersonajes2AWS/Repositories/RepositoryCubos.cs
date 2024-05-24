using ApiCrudPersonajes2AWS.Data;
using ApiCrudPersonajes2AWS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCrudPersonajes2AWS.Repositories
{
    public class RepositoryCubos
    {

        private CubosContext context;

        public RepositoryCubos(CubosContext context)
        {
            this.context = context;
        }
        public async Task<List<Cubo>> GetCubosAsync()
        {
            return await this.context.Cubos.ToListAsync();
        }

        public async Task<Cubo> FindCubosAsync(int id)
        {
            return await this.context.Cubos.FirstOrDefaultAsync(x => x.IdCubo == id);
        }

        private async Task<int> GetMaxIdCuboAsync()
        {
            return await this.context.Cubos.MaxAsync(x => x.IdCubo) + 1;
        }

        public async Task<Cubo> CreateCuboAsync
            (string nombre, string marca,string imagen, int precio)
        {
            Cubo cubo = new Cubo
            {
                IdCubo = await this.GetMaxIdCuboAsync(),
                Nombre = nombre,
                Marca = marca,
                Imagen = imagen,
                Precio = precio
            };
            this.context.Cubos.Add(cubo);
            await this.context.SaveChangesAsync();
            return cubo;
        }

        public async Task UpdateCuboAsync
            (int id, string nombre, string marca, string imagen, int precio)
        {
            Cubo cubo = await this.FindCubosAsync(id);
            cubo.Nombre = nombre;
            cubo.Marca = marca;
            cubo.Imagen = imagen;
            cubo.Precio = precio;
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteCuboAsync(int id)
        {
            Cubo cubo = await this.FindCubosAsync(id);
            this.context.Cubos.Remove(cubo);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<string>> GetMarcasAsync()
        {
            List<string> marcas = await this.context.Cubos.Select(x => x.Marca).Distinct().ToListAsync();
            return marcas;
        }

        public async Task<List<Cubo>> FindCubosMarcasAsync(string marca)
        {

            List<Cubo> cubos = await this.context.Cubos.Where(x => x.Marca == marca).ToListAsync();
            return cubos;
        }
    }
}
