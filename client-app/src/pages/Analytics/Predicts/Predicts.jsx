import { useEffect, useState } from 'react';
import { Box, Typography, Stack, Button, Paper, Chip } from '@mui/material';
import PictureAsPdfIcon from '@mui/icons-material/PictureAsPdf';

import ReagentForecastTable from "./ReagentForecastTable";
import ExportFormat from '../../Reports/ExportFormat';

import { getRecordsArray } from '../../../utils/getRecordsArray';
import { fetchGetData, fetchPostData } from '../../../api/fetch';

const columns = [
  // { field: 'id', headerName: 'ID', width: 50 },
  { field: 'name', headerName: 'Реактив', flex: 3 },
  { field: 'currentQuantity', headerName: 'Остаток', type: 'number', width: 100 },
  { field: 'minQuantity', headerName: 'Мин. кол-во', type: 'number', width: 100 },
  { field: 'unit', headerName: 'Ед. изм.', width: 90 },
  { field: 'avgConsumption', headerName: 'Среднее потр. в день', minWidth: 200, flex: 1 },
  { field: 'daysToExpiry', headerName: 'Дней до просрочки', width: 150 },
  { 
    field: 'daysToZero', 
    headerName: 'Дней до исчерпания (Остаток/Ср. потр.)', 
    width: 170,
    renderCell: (params) => (
       params.value != 999 ? params.value :'–'
    ),
  },
  { 
    field: 'recommendedOrder', 
    headerName: 'Заказ', 
    renderCell: (params) => (
      params.value > 0 ? <Chip label={params.value} color="error" /> : "—"
    ), 
    width: 100 
  },
  // { 
  //   field: 'orderDeadline', 
  //   headerName: 'Крайний срок', 
  //   width: 130,
  // },
];

/**
 * Формат: таблица с расчётными полями, цветовая индикация срочности, возможно, шкала времени (Gantt) до наступления события.
 */
const Predicts = () => {
  const [rows, setRows] = useState([]);
  const [selectedRows, setSelectedRows] = useState();

  useEffect(() => {
    fetchGetData('/api/reagent/forecast', setRows);
  }, []);

  const selectedCount = selectedRows ? selectedRows.size : 0;

  const handleOrder = async (record) => {
    try {   
      let payloadData = [];
  
      const selectedRowsArray = getRecordsArray(record);
  
      if (selectedRowsArray && selectedRowsArray.length > 0) {
        payloadData = selectedRowsArray.map(row => ({
          id: row.id,
          quantity: Number(row.recommendedOrder || 0)
        }));
      } else {
        const autoOrderRows = rows.filter(row => Number(row.recommendedOrder || 0) > 0);
  
        payloadData = autoOrderRows.map(row => ({
          id: row.id,
          quantity: Number(row.recommendedOrder)
        }));
      }
  
      if (payloadData.length === 0) {
        alert("Нет реагентов для формирования заказа.");
        return;
      }

      const response = await fetchPostData('/api/reagent/export-order-pdf', payloadData, true);
  
      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(errorText || `Статус сервера: ${response.status}`);
      }
  
      // Скачивание PDF файла в браузере (Blob)
      const blob = await response.blob();
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      
      const today = new Date();
      const formattedDate = `${String(today.getDate()).padStart(2, '0')}_${String(today.getMonth() + 1).padStart(2, '0')}_${today.getFullYear()}`;
      link.setAttribute('download', `Заявка_Закупка_${formattedDate}.pdf`); 
      
      document.body.appendChild(link);
      link.click();
      
      link.parentNode.removeChild(link);
      window.URL.revokeObjectURL(url);
    
    } catch (error) {
      console.error("Не удалось сгенерировать PDF заказа:", error.message);
      alert(`Ошибка при генерации отчета заказа: ${error.message}`);
    }
  };

  return (
    <Box
      sx={{ 
        width: '100%', 
        maxWidth:'1400px', 
        display:'flex',
        flexDirection:'column',
        margin:'0 auto',
        justifyContent:'center',
      }}
    >
      <Box sx={{ display: 'flex', justifyContent: 'space-between', width: '100%', alignItems: 'center', mb:4 }}>
        <Typography variant="h4" sx={{ fontWeight: 'bold', color: 'text.primary' }}>
          Прогноз
        </Typography>
        
        <Paper
          elevation={0}
          sx={{
            border: 1,
            borderColor: 'divider',
            borderRadius: 1,
            bgcolor: 'background.paper',
            p: 0.5,
          }}
        >
          <Stack direction="row" spacing={1}>
            <Button 
              variant="contained" 
              color="secondary" 
              startIcon={<PictureAsPdfIcon />}
              onClick={() => handleOrder(selectedRows)} 
              size="small"
            >
              Заказать {selectedCount === 0 ? 'дефицитные' : `(${selectedCount})`}
            </Button>

            <ExportFormat title={'Прогноз'} columns={columns} rows={rows} />
          </Stack> 
        </Paper>
      </Box>

      <ReagentForecastTable 
        columns={columns} 
        rows={rows}  
        onSelectionCountChange={setSelectedRows}
      />
    </Box>
  );
};
export default Predicts;