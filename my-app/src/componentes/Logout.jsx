import React, { useState, useEffect,useContext } from 'react';
import { Box, Button, Typography, Dialog, DialogActions, DialogContent, DialogTitle } from '@mui/material';
import { useNavigate, useLocation } from 'react-router-dom';
import LogoutIcon from '@mui/icons-material/Logout';
import { UsuarioContext } from '../hooks/UsuarioContext';

const Logout = () => {
  const [open, setOpen] = useState(false); 
  const [mensaje, setMensaje] = useState('');
  const navigate = useNavigate();
  const { setToken, setUsuario } = useContext(UsuarioContext);  // Accede a los métodos del contexto
  const [token, setTokenState] = useState(localStorage.getItem('token'));
  const baseUrl = process.env.REACT_APP_API_URL;
   /* if (location.pathname === '/' ||location.pathname === ' /404' ||location.pathname === ' /politicaDePrivacidad' ) {
    return null;
  }*/

  // Obtener el token de localStorage
  useEffect(() => {
  
    if (token) {
      // Decodificar el token para obtener la fecha de expiración (si está disponible)
      const decodedToken = JSON.parse(atob(token.split('.')[1]));
      const expirationTime = decodedToken.exp * 1000; // Convertir a milisegundos
      if (Date.now() > expirationTime) {
        // Si el token ha expirado, borrarlo y redirigir
        localStorage.removeItem('token');
        setTokenState(null);
        setToken(null);  // Actualiza el estado del contexto
        navigate('/');
      }
    }
  }, [token,setToken,navigate]);
  
  const handleClickOpen = () => {
    setOpen(true); 
  };


  const handleClose = () => {
    setOpen(false); 
  };


  const cerrarSesion = () => {
    fetch(`${baseUrl}/Usuario/logout`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        "Authorization": `Bearer ${token}`,
      },
      body: JSON.stringify({
       token
        }),
    })
      .then((response) => {
        if (!response.ok) {
                 throw new Error('Error al cerrar sesión, por favor intente nuevamente.');
        }
        return response.json();  
      })
      .then((data) => {
        if (data.codigo === 200)
          console.log("RemoveItem",data.codigo);
          localStorage.removeItem('token');
          localStorage.removeItem('pasaporte');
          localStorage.removeItem('id');
          localStorage.removeItem('rol');
          
          setToken(null);  // Eliminar el token del contexto
          setUsuario(null);  // Limpiar el usuario del contexto
          setTokenState(null);  // Limpiar el estado local
          navigate('/');  // Redirigir a la página de inicio
          setOpen(false); 
        
      })
      .catch((error) => {
        setMensaje(error.message); 
      });
  };

  return (
    <Box
      sx={{
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        justifyContent: 'center',
        py: 3,
        px: 2,
        maxWidth: 300,
        mx: 'auto',
      }}
    >
      {/* Botón de Cerrar sesión sin fondo blanco ni elementos adicionales */}

       <Button
       onClick={handleClickOpen}
       startIcon={<LogoutIcon />}
       sx={{
        color: 'white',
        textTransform: 'none',
        fontSize: '13px',  
        letterSpacing: '0.2px', 
        whiteSpace: 'nowrap',  
       }}
     >
       Cerrar sesión
     </Button>

      {/* Mensaje de error */}
      {mensaje && (
        <Typography color="error" sx={{ mt: 2 }}>
          {mensaje}
        </Typography>
      )}

      {/* Dialog de Confirmación */}
      <Dialog
        open={open}
        onClose={handleClose} 
               sx={{
          '& .MuiDialog-paper': {
            width: '300px',
            borderRadius: '10px',
          },
        }}
      >
        <DialogTitle>Confirmar cierre de sesión</DialogTitle>
        <DialogContent>
          <Typography variant="body1" sx={{ color: 'rgba(25, 118, 210, 0.8)' }}>
            ¿Estás seguro de que quieres cerrar sesión?
          </Typography>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose} sx={{ color: '#rgba(25, 118, 210, 0.8)' }}>
            Cancelar
          </Button>
          <Button onClick={cerrarSesion} sx={{ bgcolor: '#1e3a8a', color: '#ffffff', '&:hover': { bgcolor: '#1565c0' } }}>
            Confirmar
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
};

export default Logout;
