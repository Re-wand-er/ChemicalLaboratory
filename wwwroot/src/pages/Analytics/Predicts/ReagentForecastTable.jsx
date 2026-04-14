import { useEffect, useState } from 'react';
import { DataGrid } from '@mui/x-data-grid';
import { Chip } from '@mui/material';

import { fetchGetData } from '../../../api/fetch';

const columns = [
  { field: 'id', headerName: 'ID', width: 50 },
  { field: 'name', headerName: 'Реактив', flex: 1 },
  { field: 'currentQuantity', headerName: 'Остаток', type: 'number', width: 100 },
  { field: 'minQuantity', headerName: 'Мин. кол-во', type: 'number', width: 100 },
  { field: 'unit', headerName: 'Ед. изм.', width: 90 },
  { field: 'avgConsumption', headerName: 'Среднее потребление в день', width: 230 },
  { field: 'daysToExpiry', headerName: 'Дней до просрочки', width: 150 },
  { 
    field: 'daysToZero', 
    headerName: 'Дней до исчерпания (Остаток/Ср. потр.)', 
    width: 150,
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
  { 
    field: 'orderDeadline', 
    headerName: 'Крайний срок', 
    width: 130,
  },
];

/**
 * Прогноз просрочки отвечает на вопрос: «Какие реактивы и когда станут непригодными к использованию из-за истечения срока годности?»
 * 
 */
const ReagentForecastTable = () => {
  const [rows, setRows] = useState([]);
  
  useEffect(() => {
    fetchGetData('/api/reagent/forecast', setRows);
  }, []);

  return (
    <div style={{ height: '90%', width: '100%' }}>
      <DataGrid 
        rows={rows} 
        columns={columns} 
        initialState={{
          sorting: {
            sortModel: [{ field: 'recommendedOrder', sort: 'desc' }],
          },
        }}
      />

    </div>
  );
}


export default ReagentForecastTable;