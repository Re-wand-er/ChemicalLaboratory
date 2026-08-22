import { Paper, Typography, Box } from '@mui/material';

const CardContainer = ({ title, children, action, color = 'primary.main', sx = {} }) => {
  return (
    <Paper 
      elevation={0} 
      sx={{ 
        p: 2, 
        border: '1px solid', 
        borderColor: 'divider', 
        height: '100%', 
        borderRadius: 2,
        display: 'flex',
        flexDirection: 'column',
        ...sx 
      }}
    >
      {title &&
        <Box 
          sx={{ 
            mb: 2, 
            display: 'flex', 
            justifyContent: action ? 'space-between' : 'center', 
            alignItems: 'center' 
          }}>
          <Typography variant="subtitle1" sx={{ fontWeight: 700 }}>
            {title}
          </Typography>
          {action}
        </Box>
      }
      
      <Box sx={{ flexGrow: 1 }}>
        {children}
      </Box>
    </Paper>
  );
};

export default CardContainer;