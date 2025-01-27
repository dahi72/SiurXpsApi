import React, { useEffect, useState, useContext } from 'react';
import { Box, Typography, Paper } from '@mui/material';
import { UsuarioContext } from "../hooks/UsuarioContext";
import { useNavigate } from 'react-router-dom';
import { Layout } from './Layout';

const MisGrupos = () => {
    const [grupos, setGrupos] = useState([]);
    const { usuario } = useContext(UsuarioContext);
    const baseUrl = process.env.REACT_APP_API_URL;
    const navigate = useNavigate();

    useEffect(() => {
        if (!usuario || !usuario.id) {
            console.error('El ID del coordinador no está disponible.');
            return;
        }

        const coordinadorId = usuario.id;

        fetch(`${baseUrl}/GrupoDeViaje/coordinador/${coordinadorId}/grupos`)
            .then(response => {
                if (!response.ok) {
                    throw new Error('Error en la solicitud: ' + response.status);
                }
                return response.json();
            })
            .then(data => setGrupos(data))
            .catch(error => console.error('Error al obtener los grupos:', error));
    }, [usuario, baseUrl]);

    const handleAgregarViajero = (grupoId) => {
        navigate(`/agregarViajero/${grupoId}`);
    };

    const formatFechaCorta = (fecha) => {
        const opciones = { year: 'numeric', month: 'short', day: 'numeric' };
        return new Date(fecha).toLocaleDateString('es-ES', opciones);
    };

    return (
        <Layout>    
        <Box sx={{ padding: '2rem', backgroundColor: '#f9f9f9' }}>
            <Typography variant="h4" sx={{ mb: 4, textAlign: 'center' }}>
                Mis Grupos
            </Typography>

            {grupos.length === 0 ? (
                <Typography variant="body1" sx={{ textAlign: 'center' }}>
                    No tienes grupos de viaje.
                </Typography>
            ) : (
                <Box
                    sx={{
                        display: 'grid',
                        gridTemplateColumns: { xs: '1fr', sm: '1fr 1fr', md: 'repeat(3, 1fr)' },
                        gap: 3,
                    }}
                >
                    {grupos.map((grupo) => (
                        <Paper
                            key={grupo.id}
                            sx={{
                                padding: '1.5rem',
                                display: 'flex',
                                flexDirection: 'column',
                                alignItems: 'center',
                                textAlign: 'center',
                                backgroundColor: '#ffffff',
                                boxShadow: '0px 4px 8px rgba(0, 0, 0, 0.1)',
                                '&:hover': { backgroundColor: 'rgba(25, 118, 210, 0.1)' },
                            }}
                        >
                            <Typography variant="h6" sx={{ mb: 1 }}>
                                {grupo.nombre}
                            </Typography>
                            <Typography variant="body2" sx={{ mb: 1 }}>
                                Fechas: {formatFechaCorta(grupo.fechaInicio)} - {formatFechaCorta(grupo.fechaFin)}
                            </Typography>
                            <Typography variant="body2" sx={{ mb: 2 }}>
                                Ciudades: {grupo.ciudadesNombre && grupo.ciudadesNombre.length > 0 
                                    ? grupo.ciudadesNombre.join(', ') 
                                    : 'No especificadas'}
                            </Typography>
                            <button
                                style={{
                                    padding: '0.5rem 1rem',
                                    backgroundColor: '#1976d2',
                                    color: '#ffffff',
                                    border: 'none',
                                    borderRadius: '4px',
                                    cursor: 'pointer',
                                }}
                                onClick={() => handleAgregarViajero(grupo.id)}
                            >
                                Agregar Viajero
                            </button>
                        </Paper>
                    ))}
                </Box>
            )}
        </Box>
        </Layout>
    );
};

export default MisGrupos;
