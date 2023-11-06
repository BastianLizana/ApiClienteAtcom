namespace ClientesApi.DTOs
{
    public class ClientesDTO
    {
        public int IdCliente { get; set; }

        public string? Nombre { get; set; }

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public int? CodigoPais { get; set; }
        public string? NombrePais { get; set; }
    }
}
