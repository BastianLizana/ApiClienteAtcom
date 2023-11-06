using AutoMapper;
using ClientesApi.DTOs;
using ClientesApi.Modelo;
using System.Globalization;

namespace ClientesApi.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            #region Pais
            CreateMap<Pais, PaisDTO>().ReverseMap();
            #endregion

            #region Clientes
            CreateMap<Cliente, ClientesDTO>()
                .ForMember(destino =>
                destino.NombrePais,
                opt => opt.MapFrom(origen => origen.CodigoPaisNavigation.NombrePais));

            CreateMap<ClientesDTO, Cliente>()
                .ForMember(destino =>
                destino.CodigoPaisNavigation,
                opt => opt.Ignore());
            #endregion


        }
    }
}
