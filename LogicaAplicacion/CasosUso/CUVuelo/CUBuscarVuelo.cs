using DTOs;
using LogicaAplicacion.InterfacesCU.InterfacesCUVuelo;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosUso.CUVuelo
{
    public class CUBuscarVuelo : IBuscarVuelo
    {
        private readonly IRepositorioVuelo RepoVuelo;

        public CUBuscarVuelo(IRepositorioVuelo repo)
        {
            RepoVuelo = repo;
        }

        public VueloDTO Buscar(int id)
        {

            VueloDTO vueloDto = null;

            Vuelo vuelo = RepoVuelo.FindById(id);
            if (vuelo != null)
            {
                vueloDto = new VueloDTO()
                {
                    Id= vuelo.Id,
                    Nombre = vuelo.Nombre,
                    Horario = vuelo.Horario,
                   
                };
            }
            return vueloDto;
        }
    }
}
