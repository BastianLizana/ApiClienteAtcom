using ClientesApi.Modelo;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using ClientesApi.Services.Personal;
using ClientesApi.Services.Implementacion;

using AutoMapper;
using ClientesApi.DTOs;
using ClientesApi.Utilidades;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbclienteContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionDB"));
});

builder.Services.AddScoped<IPaisServices, PaisServices>();
builder.Services.AddScoped<IClientesServices, ClientesServices>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
        .AllowAnyHeader() 
        .AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Peticiones Api Rest
app.MapGet("/pais/lista", async (
    IPaisServices _paisServicio,
    IMapper _mapper
    ) =>
{
    var listaPais = await _paisServicio.GetListPais();
    var listaPaisDTO = _mapper.Map<List<PaisDTO>>(listaPais);

    if (listaPaisDTO.Count > 0)
    {
        return Results.Ok(listaPaisDTO);
    }
    else { 
        return Results.NotFound();
    }
});

app.MapGet("/cliente/lista", async (
    IClientesServices _clienteServicio,
    IMapper _mapper
    ) =>
{
    var listaCliente = await _clienteServicio.GetListClientes();
    var listaClienteDTO = _mapper.Map<List<ClientesDTO>>(listaCliente);

    if (listaClienteDTO.Count > 0)
    {
        return Results.Ok(listaClienteDTO);
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapPost("cliente/guardar", async (
    ClientesDTO modelo,
    IClientesServices _clienteServicio,
    IMapper _mapper
    ) => {
        var _cliente = _mapper.Map<Cliente>(modelo);
        var _clientecreado = await _clienteServicio.AddCliente(_cliente);

        if (_clientecreado.IdCliente != 0)
        {
            return Results.Ok(_mapper.Map<ClientesDTO>(_clientecreado));
        }
        else {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
});


app.MapPut("cliente/actualizar/{idCliente}", async (
    int idCliente,
    ClientesDTO modelo,
    IClientesServices _clienteServicio,
    IMapper _mapper
    ) => {
        var _encontrado = await _clienteServicio.GetCliente(idCliente);
        if (_encontrado is null)
        {
            return Results.NotFound();
        }
        
        var _cliente = _mapper.Map<Cliente>(modelo);

        _encontrado.Nombre = _cliente.Nombre;
        _encontrado.Telefono = _cliente.Telefono;
        _encontrado.Email = _cliente.Email;
        _encontrado.CodigoPais = _cliente.CodigoPais;

        var respuesta = await _clienteServicio.UpdateCliente(_encontrado);

        if (respuesta)
        {
            return Results.Ok(_mapper.Map<ClientesDTO>(_encontrado));
        }
        else {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }

    });

//app.MapGet("/cliente/busqueda/{idCliente}", async (
//    int idCliente,
//    ClientesDTO modelo,
//    IClientesServices _clienteServicio,
//    IMapper _mapper
//    ) => {
//        var _encontrado = await _clienteServicio.GetCliente(idCliente);

//        if (_encontrado is null) return Results.NotFound();

//        var _cliente = _mapper.Map<Cliente>(modelo);

//        _encontrado.Nombre = _cliente.Nombre;
//        _encontrado.Telefono = _cliente.Telefono;
//        _encontrado.Email = _cliente.Email;
//        _encontrado.CodigoPais = _cliente.CodigoPais;

//        if (_encontrado is null)
//            return Results.StatusCode(StatusCodes.Status500InternalServerError);
//        else
//            return Results.Ok(_mapper.Map<ClientesDTO>(_encontrado));


//    });


app.MapDelete("cliente/eliminar/{idCliente}", async (
    int idCliente,
    IClientesServices _clienteServicio
    ) =>{
        var _encontrado = await _clienteServicio.GetCliente(idCliente);
        if (_encontrado is null)
        {
            return Results.NotFound();
        }

        var respuesta = await _clienteServicio.DeleteCliente(_encontrado);

        if (respuesta)
        {
            return Results.Ok();
        }
        else {
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
        }
    });


#endregion

app.UseCors("NuevaPolitica");
app.Run();