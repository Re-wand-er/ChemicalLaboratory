import { createTheme } from '@mui/material/styles';

export const theme = createTheme({
  cssVariables: true,
  palette: {
    primary: {
      main: '#1976d2', 
      light: '#42a5f5',
      dark: '#1565c0',
    },
    background: {
      default: '#f8f9fa', 
      paper: '#ffffff',  
      hover: '#f1f3f4',
      border: '#cdcdcd',
    },
    error: {
      main: '#d32f2f',
      light: '#fae7e7',
    },
    text: {
      primary: '#1a1a1a',
      secondary: '#666666',
      footer: '#9e9e9e',
      footerAuthor: '#616161',
    },
    logo: {
      main: '#5f6368',
    }
  },
  // shape: {
  //   borderRadius: 8, 
  // },
  // typography: {
  //   fontFamily: '"Inter", "Roboto", "Arial", sans-serif',
  //   button: {
  //     textTransform: 'none', 
  //     fontWeight: 600,
  //   },
  // },
  // Глобальная настройка компонентов
  // components: {
  //   MuiButton: {
  //     styleOverrides: {
  //       root: {
  //         boxShadow: 'none', 
  //         '&:hover': {
  //           boxShadow: 'none',
  //         },
  //       },
  //     },
  //   },
  // },
});
