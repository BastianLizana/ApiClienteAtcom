using System;
using System.Collections.Generic;

namespace ClientesApi.Modelo;

public partial class Pais
{
    public int CodigoPais { get; set; }

    public string? NombrePais { get; set; }

    public virtual ICollection<Cliente> Clientes { get; } = new List<Cliente>();
}
