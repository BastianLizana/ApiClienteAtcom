using ClientesApi.Modelo;

namespace ClientesApi.Services.Personal
{
    public interface IClientesServices
    {
        Task<List<Cliente>> GetListClientes();
        Task<Cliente> GetCliente(int IdCliente);
        Task<Cliente> AddCliente(Cliente modelo);
        Task<bool> UpdateCliente(Cliente modelo);
        Task<bool> DeleteCliente(Cliente modelo);
    }
}
