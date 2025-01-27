import { Typography, Paper, Box } from '@mui/material';  
import React, { useContext } from 'react';
import { Layout } from './Layout';
import { UsuarioContext } from '../hooks/UsuarioContext';
import { useNavigate } from "react-router-dom";

const Dashboard = () => {
    const { usuario, loading, error  } = useContext(UsuarioContext);
    const rol = localStorage.getItem("rol"); 
    const navigate = useNavigate();
    console.log("Usuario en Dashboard:", usuario); 
    if (loading) {
        return <div>Cargando datos del usuario...</div>;  // Mostrar una pantalla de carga
    }

    if (error) {
        return <div>Error al cargar el usuario: {error}</div>;  // Mostrar un mensaje de error si falla la carga
    }

    if (!usuario) {
        return <div>No estás autenticado.</div>;  // Si no hay usuario, mostramos un mensaje
    }
    // Opciones de menú para coordinador
    const opcionesCoordinador = [
        { label: 'Crear grupo de Viaje 🌍👥', color: '#E0F7FA', action: '/crearGrupo' , font:'bold',},
        { label: 'Crear Itinerario 🏙️', color: '#E0F7FA' },
        { label: 'Calendario 📅', color: '#E0F7FA' },
        { label: 'Mapa ¿Dónde estoy? 🗺️', color: '#E0F7FA' },
        { label: 'Mis grupos 👥', color: '#E0F7FA', action: '/misGrupos' , font:'bold' },
        { label: 'Cargar catálogo 📄', color: '#E0F7FA' }
    ];

    // Opciones de menú para viajero 
    const opcionesViajero = [
        { label: 'Calendario 📅', color: '#E0F7FA' },
        { label: 'Mapa ¿Dónde estoy? 🗺️', color: '#E0F7FA' }
    ];

    const handleClick = (path) => {
        if (path) navigate(path);
    };

    // Verificar que usuario esté definido antes de acceder a sus propiedades
    const nombreUsuario = usuario ? `${usuario.primerNombre} ${usuario.primerApellido}` : "No estás autenticado";
console.log("nombre de usuario en el dash:",nombreUsuario);
    // Elige las opciones según el rol
    const opciones = rol === 'Coordinador' ? opcionesCoordinador : opcionesViajero;

    return (
        <Layout>
            <Box sx={{
                minHeight: '100vh',
                display: 'flex',
                flexDirection: 'column',
                padding: '1rem'
            }}>
                <Box sx={{ display: 'flex', flexDirection: 'column', justifyContent: 'center', alignItems: 'center', height: '100%' }}>
                    <Typography variant="h6" sx={{ mb: 1 }}>
                        Bienvenido a SiurXp
                    </Typography>
                    <Typography variant="h6" sx={{ mb: 2 }}>
                        {nombreUsuario}
                    </Typography>
                </Box>
                <Box
                    sx={{
                        display: 'grid',
                        gridTemplateColumns: { xs: '1fr', sm: '1fr 1fr', md: 'repeat(3, 1fr)' },
                        gap: 2,
                        flexGrow: 1
                    }}
                >
                    {opciones.map((opcion, index) => (
                        <Paper
                            key={index}
                            sx={{
                                padding: 3,
                                display: 'flex',
                                justifyContent: 'center',
                                alignItems: 'center',
                                backgroundColor: "#F5F5F5",
                                cursor: 'pointer',
                                                               '&:hover': { backgroundColor: 'rgba(25, 118, 210, 0.4)' }
                            }}
                            onClick={() => handleClick(opcion.action)}
                        >
                            <Typography variant="h6">{opcion.label}</Typography>
                        </Paper>
                    ))}
                </Box>
            </Box>
        </Layout>
    );
};

export default Dashboard;
