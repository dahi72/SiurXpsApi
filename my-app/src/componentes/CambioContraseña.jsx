import React, { useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { Box, TextField, Button, Typography, IconButton, Alert, InputAdornment } from '@mui/material';
import { Layout } from './Layout';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import { Visibility, VisibilityOff } from '@mui/icons-material';
import { UsuarioContext } from '../hooks/UsuarioContext';

const CambioContrasena = () => {
  const { usuario } = useContext(UsuarioContext);
  const [password, setNuevaContrasena] = useState('');
  const [confirmPassword, setConfirmarContrasena] = useState('');
  const [mensaje, setMensaje] = useState('');
  const [success, setSuccess] = useState(false);
  const [error, setError] = useState(null);
  const [mostrarContrasena, setMostrarContrasena] = useState(false);
  const [contrasenaActual, setContrasenaActual] = useState('');
  const navigate = useNavigate();
  const baseUrl = process.env.REACT_APP_API_URL;

  const manejarCambioContrasena = (e) => {
    e.preventDefault();
    setMensaje('');

    if (password !== confirmPassword) {
      setMensaje('Las contraseñas no coinciden.');
      return;
    }
    const token = localStorage.getItem('token');
    fetch(`${baseUrl}/Usuario/cambiarContrasena`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`,
      },
      body: JSON.stringify({ pasaporte:usuario.pasaporte,
        contrasenaActual: contrasenaActual,
        nuevaPassword: password,
        confirmPassword: confirmPassword 
      }),
    })
      .then((response) => {
        if (!response.ok) {
          return response.json().then((errorData) => {
            throw new Error(errorData.message);
          });
        }
      })
      .then(() => {
        setSuccess(true);
        setError(null);

        setTimeout(() => {
          setSuccess(false);
          navigate("/dashboard");
        }, 2000);
      })
      .catch((error) => {
        setSuccess(false);
        setError(error.message);

        setTimeout(() => {
          setError(null);
        }, 3000);
      });
  };

  const handleBackClick = () => {
    navigate(-1);
  };

  const handleMostrarContrasena = () => {
    setMostrarContrasena(!mostrarContrasena);
  };

  return (
    <Layout>
      <IconButton 
        onClick={handleBackClick} 
        sx={{
          color: 'rgb(25, 118, 210)',
          display: 'flex', 
          alignItems: 'center',
          gap: 1,
          '&:hover': {
            color: 'rgb(21, 101, 192)',
            cursor: 'pointer',
          }
        }}
      >
        <ArrowBackIcon />
        <Typography variant="body1">Volver</Typography>
      </IconButton>
      <Box 
        sx={{
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
           padding: '16px',
        }}
      >
        <Box 
          sx={{
            width: '100%',
            maxWidth: 400,
            padding: '32px',
            bgcolor: 'white',
            borderRadius: 2,
            boxShadow: 3,
          }}
        >
          <Typography variant="h5" align="center" sx={{ mb: 3 }}>
            Cambiar Contraseña
          </Typography>
          <form onSubmit={manejarCambioContrasena}>
            <TextField
              label="Pasaporte"
              variant="outlined"
              fullWidth
              value={usuario.pasaporte}
              disabled
              sx={{ mb: 2 }}
            />
            <TextField
  label="Contraseña Actual"
  type="password"
  variant="outlined"
  fullWidth
  value={contrasenaActual}
  onChange={(e) => setContrasenaActual(e.target.value)}
  sx={{ mb: 2 }}
  required
/>
            <TextField
              label="Nueva Contraseña"
              type={mostrarContrasena ? 'text' : 'password'}
              variant="outlined"
              fullWidth
              value={password}
              onChange={(e) => setNuevaContrasena(e.target.value)}
              sx={{ mb: 2 }}
              required
              InputProps={{
                endAdornment: (
                  <InputAdornment position="end">
                    <IconButton onClick={handleMostrarContrasena}>
                      {mostrarContrasena ? <VisibilityOff /> : <Visibility />}
                    </IconButton>
                  </InputAdornment>
                ),
              }}
            />
            <TextField
              label="Confirmar Contraseña"
              type={mostrarContrasena ? 'text' : 'password'}
              variant="outlined"
              fullWidth
              value={confirmPassword}
              onChange={(e) => setConfirmarContrasena(e.target.value)}
              sx={{ mb: 3 }}
              required
              InputProps={{
                endAdornment: (
                  <InputAdornment position="end">
                    <IconButton onClick={handleMostrarContrasena}>
                      {mostrarContrasena ? <VisibilityOff /> : <Visibility />}
                    </IconButton>
                  </InputAdornment>
                ),
              }}
            />
            <Button 
              variant="contained" 
              color="primary" 
              fullWidth 
              type="submit"
            >
              Cambiar Contraseña
            </Button>
            {mensaje && (
              <Typography variant="body2" color={mensaje.includes('exitosamente') ? 'green' : 'red'} align="center" sx={{ mt: 2 }}>
                {mensaje}
              </Typography>
            )}
          </form>
          {success && <Alert severity="success" sx={{ mt: 2 }}>Se actualizó la contraseña</Alert>}
          {error && <Alert severity="error" sx={{ mt: 2 }}>{error}</Alert>}
        </Box>
      </Box>
    </Layout>
  );
};

export default CambioContrasena;
