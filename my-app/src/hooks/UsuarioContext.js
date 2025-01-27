import React, { createContext, useState, useEffect } from "react";

export const UsuarioContext = createContext();

export const UsuarioProvider = ({ children }) => {
  const [usuario, setUsuario] = useState(null);  // Estado inicial en null
  const [loading, setLoading] = useState(true);   // Estado de carga
  const [error, setError] = useState(null);       // Estado de error
  const [token, setToken] = useState(localStorage.getItem('token'));
  const baseUrl = process.env.REACT_APP_API_URL;
  const id = localStorage.getItem("id");

  console.log("id en contexto", id);

  useEffect(() => {
    // Solo cargamos los datos del usuario cuando es necesario
    if (token && !usuario) {
      // Si el usuario no está completo, obtenemos más información desde la API
      fetch(`${baseUrl}/Usuario/${id}`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
         // "Authorization": `Bearer ${token}`,
        },
      })
        .then((response) => response.json())
        .then((data) => {
          console.log("usu en contexto", usuario);
          setUsuario({
            id: localStorage.getItem("id"),
            primerNombre: data.primerNombre,
            segundoNombre: data.segundoNombre,
            primerApellido: data.primerApellido,
            segundoApellido: data.segundoApellido,
            email: data.email,
            pasaporte: localStorage.getItem("pasaporte"),
            passwordHash: data.passwordHash,
            telefono: data.telefono,
            fechaNac: data.fechaNac,
            rol: data.rol,
            estado: data.estado,
            debeCambiarContrasena: data.debeCambiarContrasena,
            gruposComoViajero: data.gruposComoViajero,
            gruposComoCoordinador: data.gruposComoCoordinador,
            pasaporteDocumentoRuta: data.pasaporteDocumentoRuta,
            visaDocumentoRuta: data.visaDocumentoRuta,
            vacunasDocumentoRuta: data.vacunasDocumentoRuta,
            seguroDeViajeDocumentoRuta: data.seguroDeViajeDocumentoRuta,
            pasaporteDocumento: data.pasaporteDocumento,
            visaDocumento: data.visaDocumento,
            vacunasDocumento: data.vacunasDocumento,
            seguroDeViajeDocumento: data.seguroDeViajeDocumento,
          });
          console.log("usu en contexto", usuario);
          setLoading(false);  // Cambiar estado de carga a false
        })
        .catch((error) => {
          setError(error.message);  // Establecer mensaje de error
          setLoading(false);  // Cambiar estado de carga a false incluso si hay error
        });
    } else {
      setLoading(false);  // Si ya tenemos usuario, no hacemos más fetch
    }
  }, [token, id]); // Reaccionar si hay cambios en el usuario o el token
  useEffect(() => {
    if (usuario) {
      console.log("Usuario actualizado:", usuario);
    }
  }, [usuario]); 
  return (
    <UsuarioContext.Provider value={{ usuario, setUsuario, loading, error, setToken }}>
      {children}
    </UsuarioContext.Provider>
  );
};
