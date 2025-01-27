import React from 'react';
import MenuIcon from '@mui/icons-material/Menu';
import { AppBar, Toolbar, IconButton, Box, Button, useMediaQuery, useTheme, Drawer, List, ListItem, ListItemText } from '@mui/material';
import xperiencias from '../Imagenes/xperiencias.png';
import siurBlanco from '../Imagenes/SiurBlanco.png';
import Logout from './Logout';

export const Header = ({ open, handleDrawerToggle}) => {
  const theme = useTheme();
  

  const isSmallScreen = useMediaQuery(theme.breakpoints.down('md'));
  const isAuthenticated = !!localStorage.getItem("token");
  return (
    <>
      <AppBar
        position="static"
        sx={{
          width: '100%',
          zIndex: (theme) => theme.zIndex.drawer + 1,
          bgcolor: 'rgba(25, 118, 210, 0.7)', 
          height:'12vh'
        }}
      >
        <Toolbar sx={{ width: '100%', padding: { xs: '0 16px', sm: '0 32px' } }}>
          
          {/* Icono de hamburguesa solo en pantallas pequeñas */}
          {isAuthenticated && isSmallScreen &&(
            <IconButton
              color="inherit"
              aria-label="open drawer"
              edge="start"
              onClick={handleDrawerToggle}
              sx={{
                mr: 2,
              }}
            >
              <MenuIcon />
            </IconButton>
          )}

           {/* Contenedor para mostrar las imágenes en columna */}
           <Box
            sx={{
              display: 'flex',
              flexGrow: 1,
              flexDirection: 'column', 
                        marginLeft:'150px'
             
            }}
          >
            {/* Logo Siur */}
            <img
              src={siurBlanco}
              alt="Siur Logo"
              style={{ width: '150px', margin: '4px' }}
            />
            {/* Logo Xperiencias */}
            <img
              src={xperiencias}
              alt="Xperiencias Logo"
              style={{ width: '150px', margin: '4px' }}
            />
          </Box>

          {/* Botón de Logout */}
          {isAuthenticated && (
            <Button
              onClick={() => {}}
              sx={{
                color: 'white',
                textTransform: 'none',
                fontWeight: 'bold',
                mr:'120px',
                '&:hover': {
                  backgroundColor: 'transparent',
                },
              }}
            >
              <Logout />
            </Button>
          )}
        </Toolbar>
      </AppBar>
    </>
  );
};