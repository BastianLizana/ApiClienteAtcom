using System;
using System.Collections.Generic;

namespace ClientesApi.Modelo;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string? Nombre { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public int? CodigoPais { get; set; }

    public virtual Pais? CodigoPaisNavigation { get; set; }
}
