using ClientesApi.Modelo;

namespace ClientesApi.Services.Personal
{
    public interface IPaisServices
    {
        Task<List<Pais>> GetListPais();
    }
}
