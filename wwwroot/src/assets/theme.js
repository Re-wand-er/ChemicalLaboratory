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
      outlet: '#f1f3f4a2',
      sidebar: '#dcdddd',
      oddRow: '#00000006',
    },
    error: {
      main: '#d32f2f',
      light: '#fae7e7',
    },
    ok:{
      main: '#2e7d32',
    },
    warning:{
      main: '#dfb004',
    },
    text: {
      primary: '#1a1a1a',
      secondary: '#666666',
      footer: '#9e9e9e',
      footerAuthor: '#616161',
    },
    status: {
      ownRed: '#f44336',
      ownBlue: '#2196f3',
      ownOrange: '#ff9800',
      ownGreen: '#4caf50',
      ownPurple: '#9c27b0',
      ownLightBlue: '#00bcd4',
    },
    charts: {
      main: '#8884d8',    
      grid: '#e2e8f0',    
      text: '#64748b',    
      cursor: '#0000000d', 
      variants: [
        '#10b981', // emerald
        '#e4a908', // amber
        '#3b82f6', // blue
        '#82ca9d', // light green
        '#ff7300', // orange
      ],
      hover: '#ae4343',
      
      tooltip: {
        bg: 'rgba(255, 255, 255, 0.95)',
        border: '#cbd5e1',
        text: '#333333',
        shadow: '0 4px 6px -1px rgba(0, 0, 0, 0.2)',
      }
    },
    logo: {
      main: '#5f6368',
    }
  },
  padding:{
    main: '16px',
    half: '8px',
    quarter: '4px',
  }
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
