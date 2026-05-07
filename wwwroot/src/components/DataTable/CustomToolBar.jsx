import { 
    Box, Button, Stack, 
    Typography, CircularProgress, 
    Toolbar
} from '@mui/material';
import { Add as AddIcon, Refresh as RefreshIcon, Delete as DeleteIcon } from '@mui/icons-material';

const CustomToolbar = ({ isLoading, selectedCount, onCreate, onDelete, onCsvFileCreate, onJsonFileCreate }) => {
  return (
    <Toolbar 
      sx={{ 
        borderBottom: '1px solid', 
        borderColor: 'divider',
        width: '100%', 
      }}
    >
      <Box sx={{ display: 'flex', justifyContent: 'space-between', width: '100%', alignItems: 'center' }}>
        <Stack direction="row" spacing={1}>
          <Button 
            variant="contained" 
            startIcon={<AddIcon />} 
            onClick={onCreate} 
            disabled={isLoading} 
            size="small"
          >
            Создать
          </Button>
          
          <Button 
            variant="outlined" 
            color="error" 
            startIcon={<DeleteIcon />} 
            onClick={onDelete} 
            size="small"
            disabled={selectedCount === 0}
          >
            Удалить {selectedCount > 0 && `(${selectedCount})`}
          </Button>
        </Stack>

        <Stack direction="row" spacing={1}>
          <Button 
            variant="outlined" 
			  		color="warning"
            onClick={onCsvFileCreate} 
            size="small"
          >
            Экспорт в csv 
          </Button>

          <Button 
            variant="outlined" 
            onClick={onJsonFileCreate} 
            size="small"
          >
            Экспорт в json 
          </Button>
        </Stack>
      </Box>
    </Toolbar>
  );
};
export default CustomToolbar;