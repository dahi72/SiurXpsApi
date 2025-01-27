import React, { useContext } from "react";
import { Navigate } from "react-router-dom";
import { UsuarioContext } from "../hooks/UsuarioContext";

const RutaProtegida = ({ children }) => {
  const { usuario, loading} = useContext(UsuarioContext);
 
const token =localStorage.getItem('token');
console.log("token en ruta:", token);
console.log("usuario en ruta:", usuario);

if (loading) {
    return <div>Loading...</div>;  // Puedes mostrar un cargando o spinner mientras se obtienen los datos
  }
  // Verifica si el usuario está autenticado y si tiene un token válido
  if (!usuario || token == null) {
    return <Navigate to="/" />;  // Redirige al login si no hay usuario ni token
  }

  return children;  // Muestra el contenido protegido si todo está bien
};

export default RutaProtegida;