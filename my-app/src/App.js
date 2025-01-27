import React from 'react';
import './App.css';
import './estilos.css';
import NoEncontrado from './componentes/NoEncontrado';
import Login from './componentes/Login'; 
import Registro from './componentes/Registro'; 
import CambiarContrasena from './componentes/CambioContraseña';
import Logout from './componentes/Logout';
import Dashboard from './componentes/Dashboard';
import { BrowserRouter, Routes, Route} from 'react-router-dom'; 
import VerMisDatos from './componentes/VerMisDatos';
import MisDatos from './componentes/MisDatos';
import { UsuarioProvider } from './hooks/UsuarioContext';
import CrearGrupoDeViaje from './componentes/CrearGrupoDeViaje';
import { PaisProvider } from './hooks/PaisContext';
import { CiudadProvider } from './hooks/CiudadContext';
import PoliticaDePrivacidad from   './componentes/PoliticaDePrivacidad';
import TerminosYCondiciones from   './componentes/TerminosYCondiciones'; 
import RutaProtegida from './componentes/RutaProtegida'; 
import AgregarViajeroAGrupo from './componentes/AgregarViajeroAGrupo'
import MisGrupos from './componentes/Misgrupos';


const App = () => {
  return (
    <UsuarioProvider>
      <BrowserRouter future={{ v7_relativeSplatPath: true }}>
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/404" element={<NoEncontrado />} />
          <Route path="*" element={<NoEncontrado />} />
          <Route path="/politicaDePrivacidad" element={<PoliticaDePrivacidad />} />
          <Route path="/terminos" element={<TerminosYCondiciones />} />

          <Route path="/dashboard" element={<RutaProtegida><Dashboard /></RutaProtegida>} />
          <Route path="/verMisDatos" element={<RutaProtegida><VerMisDatos /></RutaProtegida>} />
          <Route path="/misDatos" element={<RutaProtegida><MisDatos /></RutaProtegida>} />
          <Route path="/logout" element={<RutaProtegida><Logout /></RutaProtegida>} />
          <Route path="/misGrupos" element={<RutaProtegida><MisGrupos /></RutaProtegida>} />


          {/* Aquí solo el componente CrearGrupoDeViaje tiene acceso a los contextos de Pais y Ciudad */}
          <Route path="/crearGrupo" element={
            <RutaProtegida>
              <PaisProvider>
                <CiudadProvider>
                  <CrearGrupoDeViaje />
                </CiudadProvider>
              </PaisProvider>
            </RutaProtegida>
          } />

          <Route path="/cambiar-contrasena" element={<RutaProtegida><CambiarContrasena /></RutaProtegida>} />
          <Route path="/registro" element={<RutaProtegida><Registro /></RutaProtegida>} />
          <Route path="/agregarViajero/:grupoId" element={<RutaProtegida><AgregarViajeroAGrupo /></RutaProtegida>} />
        </Routes>
      </BrowserRouter>
    </UsuarioProvider>
  );
}

export default App;