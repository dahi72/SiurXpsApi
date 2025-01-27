import React, { useRef, useState,useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Box, Paper, Typography, Button, TextField, InputAdornment, IconButton} from '@mui/material';
import { Layout } from './Layout';
import { useScreenSize } from '../hooks/useScreenSize'; 
import { UsuarioContext } from "../hooks/UsuarioContext";
import { Visibility, VisibilityOff } from '@mui/icons-material';


const Login = () => {
  const { setUsuario } = useContext(UsuarioContext);
  const passwordRef = useRef(null);
  const pasaporteRef = useRef(null);  // Referencia para el campo de pasaporte
  const navigate = useNavigate();
  const baseUrl = process.env.REACT_APP_API_URL;
  const [mostrarContrasena, setMostrarContrasena] = useState(false);
  const [userData, setData] = useState();
  const { isXs, isSm, isMd, isLg, isXl } = useScreenSize();
  

  const [mensaje, setMensaje] = useState("");
  const [camposVacios, setCamposVacios] = useState(true);


  const ingresar = (e) => {
    e.preventDefault(); 
    const password = passwordRef.current.value;
    const pasaporte = pasaporteRef.current.value;  

    if (!password || !pasaporte) {
      setMensaje("Debe proporcionar pasaporte y contraseña");
      return;
    }

    fetch(`${baseUrl}/Usuario/login`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        password,
        pasaporte,
      }),
    })
      .then((response) => response.json())  
      .then((data) => {
        if (data.token) {  
          console.log("Respuesta del login:", data);
           localStorage.setItem("token", data.token);
          localStorage.setItem("pasaporte", data.pasaporte);
          localStorage.setItem("id", data.id);
          localStorage.setItem("rol",data.rol);
     
          if (data.debeCambiarContrasena) {
            navigate("/cambiar-contrasena");
          } else {
            navigate("/dashboard");
          }
        } else {
          setMensaje("Usuario y/o contraseña incorrectos");
        }
      })
      .catch((error) => {
        if (error.message === "Failed to fetch") {
          setMensaje("Ocurrió un error en el servidor. Inténtelo de nuevo más tarde.");
        } else {
          setMensaje(`Error: ${error.message}`);
        }
      });
  };

  const handleMostrarContrasena = () => {
    setMostrarContrasena(!mostrarContrasena);
  };


  const handleChange = () => {
    const passwordValue = passwordRef.current.value;
    const pasaporteValue = pasaporteRef.current.value;
    setCamposVacios(!passwordValue || !pasaporteValue);  
  };
  

  return (
    <Layout>
      <Box sx={{ 
        display: 'flex', 
        justifyContent: 'center', 
        alignItems: 'center', 
        Height: 'auto', 
        flexDirection: 'column',
        padding: 2,
        marginTop: '0', 
      }}>
        <Paper sx={{ 
          padding: 3, 
          width: '100%', 
          maxWidth: isXs ? 300 : 400, 
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center', // Asegura que el contenido se ajuste verticalmente
          gap: 2, // Espaciado uniforme entre los elementos dentro del Paper
          height:'60vh',
        }}>
          <Box sx={{ textAlign: 'center', mb: 2 }}>
            <Typography variant="h5" gutterBottom>Inicio de Sesión</Typography>
          </Box>

          <form onSubmit={ingresar}>
            <TextField
              fullWidth
              label="Pasaporte"
              inputRef={pasaporteRef}  // Campo de pasaporte
              onChange={handleChange}
              required
              margin="normal"
              autoComplete="username"
            />
            <TextField
  fullWidth
  label="Contraseña"
  type={mostrarContrasena ? "text" : "password"}  // Cambio dinámico de tipo
  inputRef={passwordRef}
  onChange={handleChange}
  required
  margin="normal"
  autoComplete="current-password" 
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
            {mensaje && <Typography color="error" sx={{ mt: 2 }}>{mensaje}</Typography>}

            <Button 
              fullWidth 
              variant="contained" 
              sx={{ mt: 2 }} 
              type="submit" 
              disabled={camposVacios}
            >
              Ingresar
            </Button>
          </form>

          <Box sx={{ mt: 2, textAlign: 'center' }}>
            <Link to="/registro" className="link">
              ¿Aún no estás registrado?
            </Link>
          </Box>
        </Paper>
      </Box>
    </Layout>
  );
};

export default Login;
