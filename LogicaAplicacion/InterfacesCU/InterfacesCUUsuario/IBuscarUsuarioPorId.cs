using LogicaNegocio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOs;

namespace LogicaAplicacion.InterfacesCU.InterfacesCUUsuario
{
    public interface IBuscarUsusarioPorId
    {
        UsuarioDTO Buscar(int id);
    }
}
