﻿using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU.InterfacesCUUsuario
{
    public interface IModificarInfoPersonal
    {
        void Modificar(int id,ModificarUsuarioDTO usu);
    }
}
