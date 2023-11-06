using Microsoft.EntityFrameworkCore;
using ClientesApi.Modelo;
using ClientesApi.Services.Personal;

namespace ClientesApi.Services.Implementacion
{
    public class PaisServices : IPaisServices
    {
        private DbclienteContext _dbcontext;

        public PaisServices(DbclienteContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<Pais>> GetListPais()
        {
            try 
            { 
                List<Pais> lista = new List<Pais>();
                lista = await _dbcontext.Pais.ToListAsync();
                return lista;
            }
            catch(Exception ex){
                throw ex;
            }
        }
    }
}
