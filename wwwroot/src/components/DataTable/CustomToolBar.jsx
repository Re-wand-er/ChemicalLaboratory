import { 
    Box, Button, Stack, 
    Typography, CircularProgress, 
    Toolbar
} from '@mui/material';
import { Add as AddIcon, Refresh as RefreshIcon, Delete as DeleteIcon } from '@mui/icons-material';
import RemoveCircleOutlineIcon from '@mui/icons-material/RemoveCircleOutline';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import QrCodeScannerIcon from '@mui/icons-material/QrCodeScanner';
import PictureAsPdfIcon from '@mui/icons-material/PictureAsPdf';

import ExportFormat from '../../pages/Reports/ExportFormat';

const CustomToolbar = ({ isLoading, selectedCount, onCreate, onDelete, onWriteOff, onIncome, onOrder, onQrIncome, columns, rows }) => {
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
            variant="contained" 
            color="success" 
            startIcon={<QrCodeScannerIcon />} 
            onClick={onQrIncome} 
            size="small"
          >
            Внести по Qr
          </Button>

          <Button 
            variant="contained" 
            color="warning" 
            startIcon={<RemoveCircleOutlineIcon />} 
            onClick={onWriteOff} 
            size="small"
            disabled={selectedCount === 0}
          >
            Списание {selectedCount > 0 && `(${selectedCount})`}
          </Button>

          <Button 
            variant="contained" 
            color="success" 
            startIcon={<AddCircleOutlineIcon />} 
            onClick={onIncome} 
            size="small"
            disabled={selectedCount === 0}
          >
            Внесение {selectedCount > 0 && `(${selectedCount})`}
          </Button>

          <Button 
            variant="contained" 
            color="secondary" 
            startIcon={<PictureAsPdfIcon />}
            onClick={onOrder} 
            size="small"
            disabled={selectedCount === 0}
          >
            Заказать {selectedCount > 0 && `(${selectedCount})`}
          </Button>
        </Stack>

        <Stack direction="row" spacing={1}>
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

          <ExportFormat title={'Отчет'} columns={columns} rows={rows} />
          {/* <Button 
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
          </Button> */}
        </Stack>
      </Box>
    </Toolbar>
  );
};
export default CustomToolbar;