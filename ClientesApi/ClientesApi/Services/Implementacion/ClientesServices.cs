using Microsoft.EntityFrameworkCore;
using ClientesApi.Modelo;
using ClientesApi.Services.Personal;

namespace ClientesApi.Services.Implementacion
{
    public class ClientesServices : IClientesServices
    {
        private DbclienteContext _dbcontext;

        public ClientesServices(DbclienteContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<Cliente>> GetListClientes()
        {
            try
            { 
                List<Cliente> lista = new List<Cliente>();
                lista = await _dbcontext.Clientes.Include(cli => cli.CodigoPaisNavigation).ToListAsync();
                return lista;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public async Task<Cliente> GetCliente(int IdCliente)
        {
            try
            {
                Cliente? encontrado = new Cliente();
                encontrado = await _dbcontext.Clientes.Include(cli => cli.CodigoPaisNavigation)
                    .Where(e => e.IdCliente == IdCliente).FirstOrDefaultAsync();

                return encontrado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Cliente> AddCliente(Cliente modelo)
        {
            try
            {
                _dbcontext.Clientes.Add(modelo);
                await _dbcontext.SaveChangesAsync();

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateCliente(Cliente modelo)
        {
            try
            {
                _dbcontext.Clientes.Update(modelo);
                await _dbcontext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteCliente(Cliente modelo)
        {
            try
            {
                _dbcontext.Clientes.Remove(modelo);
                await _dbcontext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
