﻿using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCU.InterfacesCUVuelo
{
    public interface IBuscarVuelo
    {
        VueloDTO Buscar(int id);
    }
}
