import React, { useEffect, useState } from 'react';
import { Box, CssBaseline, Drawer, Toolbar, List, ListItem, ListItemText, AppBar, IconButton } from '@mui/material';
import CloseIcon from '@mui/icons-material/Close'; // Importa el ícono de cierre
import { Header } from './Header';
import { Footer } from './Footer';
import { Link } from 'react-router-dom';
import { useScreenSize } from '../hooks/useScreenSize';

export const Layout = ({ children }) => {
  const [open, setOpen] = useState(false);
  const [rol, setRol] = useState(null);
  const { isUpMd} = useScreenSize();

  useEffect(() => {
    const token = localStorage.getItem("token");
    const storedRol = localStorage.getItem("rol");

    if (token && storedRol) {
      setRol(storedRol);
    }

  }, []);

  const handleDrawerToggle = () => {
    setOpen(!open);
  };

  const drawerItems = ['Mis datos', 'Cambiar contrasena', 'Modificar mis datos'];
  const drawerItemsCoordinador = ['Registrar nuevo Coordinador'];
  const isAuthenticated = !!localStorage.getItem("token");
  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
      <CssBaseline />

      <Header open={open} handleDrawerToggle={handleDrawerToggle} isAuthenticated={isAuthenticated} />

      {/* Navbar para pantallas grandes */}
      {isUpMd && isAuthenticated && (
        <AppBar position="static" sx={{ boxShadow: 0, bgcolor: 'white', height: 'auto' }}>
          <Toolbar sx={{ display: 'flex', justifyContent: 'center', padding: '0 16px' }}>
            <Box sx={{ display: 'flex', gap: 2, alignItems: 'center' }}>
              {rol && (
                <>
                  {drawerItems.map((text, index) => {
                    let path = `/path-for-${text}`;
                    if (text === 'Mis datos') path = '/verMisDatos';
                    if (text === 'Cambiar contrasena') path = '/cambiar-contrasena';
                    if (text === 'Modificar mis datos') path = '/misDatos';

                    return (
                      <ListItem
                        button
                        key={index}
                        component={Link}
                        to={path}
                        sx={{
                          '& .MuiListItemText-primary': {
                            fontSize: '15px',
                            color: 'rgba(25, 118, 210, 0.6)',
                            fontWeight: 'bold',
                          },
                          '&:hover': {
                            backgroundColor: 'transparent',
                            color: '#00bcd4',
                          },
                        }}
                      >
                        <ListItemText primary={text} />
                      </ListItem>
                    );
                  })}

                  {/* Mostrar opciones adicionales si el rol es Coordinador */}
                  {rol === 'Coordinador' && drawerItemsCoordinador.map((text, index) => (
                    <ListItem
                      button
                      key={index}
                      component={Link}
                      to="/registro"
                      sx={{
                        '& .MuiListItemText-primary': {
                          fontSize: '15px',
                          color: 'rgba(25, 118, 210, 0.6)',
                          fontWeight: 'bold',
                        },
                        '&:hover': {
                          backgroundColor: 'transparent',
                          color: '#00bcd4',
                        },
                      }}
                    >
                      <ListItemText primary={text} />
                    </ListItem>
                  ))}
                </>
              )}
            </Box>
          </Toolbar>
        </AppBar>
      )}


      {isAuthenticated && (
        <Drawer
          variant="temporary"
          open={open}
          onClose={handleDrawerToggle}
          sx={{
            '& .MuiDrawer-paper': {
              width: 240,
              boxSizing: 'border-box',
              fontSize: '15px',
              color: 'rgba(25, 118, 210, 0.8)',
              marginTop:'109px'
            },
          }}
        >
          <Toolbar>
            {/* Ícono de cierre en la esquina superior derecha del Drawer */}
            <IconButton
              edge="start"
              color="inherit"
              onClick={handleDrawerToggle}
              aria-label="close"
              sx={{ ml: 'auto' }} // Alinear el ícono de cierre a la derecha
            >
              <CloseIcon />
            </IconButton>
          </Toolbar>
          <Box sx={{ overflow: 'auto' }}>
            <List>
              {rol && (
                <>
                  {/* Opciones de navegación del Drawer */}
                  {drawerItems.map((text, index) => {
                    let path = `/path-for-${text}`;
                    if (text === 'Mis datos') path = '/verMisDatos';
                    if (text === 'Cambiar contrasena') path = '/cambiar-contrasena';
                    if (text === 'Modificar mis datos') path = '/misDatos';

                    return (
                      <ListItem
                        button
                        key={index}
                        component={Link}
                        to={path}
                        onClick={handleDrawerToggle} 
                      >
                        <ListItemText primary={text} />
                      </ListItem>
                    );
                  })}

                  {/* Opciones adicionales si el rol es Coordinador */}
                  {rol === 'Coordinador' && drawerItemsCoordinador.map((text, index) => (
                    <ListItem
                      button
                      key={index}
                      component={Link}
                      to="/registro"
                      onClick={handleDrawerToggle} // Cerrar el Drawer al hacer clic en una opción
                    >
                      <ListItemText primary={text} />
                    </ListItem>
                  ))}
                </>
              )}
            </List>
          </Box>
        </Drawer>
      )}

<Box
  component="main"
  sx={{
    flexGrow: 1,
    p: 3,
    ml: { xs: '14px', sm: '120px' },  
    mr: { xs: '14px', sm: '120px' }, 
  
   
  }}
>
  {children}
</Box>

      <Footer />
    </Box>
  );
};
