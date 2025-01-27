import React, { useState, useEffect } from "react";
import { Box, Paper, Typography, Button, TextField, Alert, Container, FormControl, InputLabel, Select, MenuItem } from "@mui/material";
import { Layout } from "./Layout";
import { useNavigate, useParams } from "react-router-dom";

const AgregarViajeroAGrupo = () => {
  const { grupoId } = useParams(); // Obtener el ID del grupo desde los parámetros de la URL
  const [grupoSeleccionado, setGrupoSeleccionado] = useState(grupoId || "");
  const [viajero, setViajero] = useState({ primerNombre: "", primerApellido: "", pasaporte: "" });
  const [grupos, setGrupos] = useState([]);
  const [success, setSuccess] = useState(false);
  const [error, setError] = useState(null);
  const baseUrl = process.env.REACT_APP_API_URL;

  const navigate = useNavigate();

  useEffect(() => {
    // Cargar grupos solo si no se pasa grupoId
    if (!grupoId) {
      fetch(`${baseUrl}/GrupoDeViaje/coordinador/${localStorage.getItem("idUsuario")}/grupos`, {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
      })
        .then((response) => response.json())
        .then((data) => setGrupos(data))
        .catch(() => setError("Error al cargar los grupos disponibles."));
    }
  }, [grupoId, baseUrl]);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!grupoSeleccionado) {
      setError("Por favor selecciona un grupo.");
      return;
    }

    fetch(`${baseUrl}/GrupoDeViaje/agregarViajero/${grupoSeleccionado}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${localStorage.getItem("token")}`,
      },
      body: JSON.stringify(viajero), // Enviar las propiedades correctas
    })
      .then((response) => {
        if (!response.ok) {
          return response.json().then((data) => {
            throw new Error(data.mensaje || "Ocurrió un error al agregar al viajero.");
          });
        }
        return response.json();
      })
      .then((data) => {
        setSuccess(true);
        setError(null);
        setViajero({ primerNombre: "", primerApellido: "", pasaporte: "" }); // Limpiar formulario
      })
      .catch((err) => {
        setError(err.message);
        setSuccess(false);
      });
  };

  return (
    <Layout>
      <Container maxWidth="sm" sx={{ mt: 4 }}>
        <Paper sx={{ p: 4 }}>
          <Typography variant="h5" gutterBottom>
            Agregar Pasajero al Grupo
          </Typography>
          <form onSubmit={handleSubmit}>
            {/* Selección de Grupo (solo si grupoId no está presente) */}
            {!grupoId && (
              <FormControl fullWidth margin="normal" required>
                <InputLabel>Grupo de Viaje</InputLabel>
                <Select
                  value={grupoSeleccionado}
                  onChange={(e) => setGrupoSeleccionado(e.target.value)}
                  label="Grupo de Viaje"
                >
                  {grupos.map((grupo) => (
                    <MenuItem key={grupo.id} value={grupo.id}>
                      {grupo.nombre}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            )}

            {/* Datos del Viajero */}
            <TextField
              fullWidth
              label="Primer Nombre"
              value={viajero.primerNombre}
              onChange={(e) => setViajero({ ...viajero, primerNombre: e.target.value })}
              required
              margin="normal"
            />
            <TextField
              fullWidth
              label="Primer Apellido"
              value={viajero.primerApellido}
              onChange={(e) => setViajero({ ...viajero, primerApellido: e.target.value })}
              required
              margin="normal"
            />
            <TextField
              fullWidth
              label="Número de Pasaporte"
              value={viajero.pasaporte}
              onChange={(e) => setViajero({ ...viajero, pasaporte: e.target.value })}
              required
              margin="normal"
            />
            <Button type="submit" fullWidth variant="contained" sx={{ mt: 2 }}>
              Agregar Pasajero
            </Button>
          </form>
          {success && <Alert severity="success" sx={{ mt: 2 }}>Pasajero agregado correctamente.</Alert>}
          {error && <Alert severity="error" sx={{ mt: 2 }}>{error}</Alert>}
        </Paper>
      </Container>
    </Layout>
  );
};

export default AgregarViajeroAGrupo;
