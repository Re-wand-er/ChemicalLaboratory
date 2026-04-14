import { 
    Box, Button, Stack, 
    Typography, CircularProgress, 
    Toolbar
} from '@mui/material';
import { Add as AddIcon, Refresh as RefreshIcon, Delete as DeleteIcon } from '@mui/icons-material';

const CustomToolbar = ({ isLoading, selectedCount, onCreate, onRefresh, onDelete }) => {
    return (
        <Toolbar 
            sx={{ 
                borderBottom: '1px solid', 
                borderColor: 'divider',
                width: '100%', 
                display: 'flex'
            }}
        >
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
                >
                    Удалить {selectedCount > 0 && `(${selectedCount})`}
                </Button>
            
            
                <Typography variant="caption" >
                    {selectedCount > 0 ? `✓ Выбрано: ${selectedCount}` : 'Выберите строки'}
                </Typography>
                {/* <Button 
                    variant="outlined" 
                    startIcon={isLoading ? <CircularProgress size={16} /> : <RefreshIcon />} 
                    onClick={onRefresh} 
                    disabled={isLoading} 
                    size="small"
                >
                    Обновить
                </Button> */}
            </Stack>
        </Toolbar>
    );
};
export default CustomToolbar;