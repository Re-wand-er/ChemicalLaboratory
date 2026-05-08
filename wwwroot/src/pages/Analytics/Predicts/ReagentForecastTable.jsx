import { useEffect, useState } from 'react';
import { Chip } from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';

import { fetchGetData } from '../../../api/fetch';

import styles from '../../../components/DataTable/dataTable.module.css'; // Неправильно, но делать правильно лень
import { Box } from '@mui/system';

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
 * Прогноз просрочки отвечает на вопрос: «Какие реактивы и когда станут непригодными к использованию из-за истечения срока годности?»
 * 
 */
const ReagentForecastTable = () => {
  const [rows, setRows] = useState([]);
  const [paginationModel, setPaginationModel] = useState({
    pageSize: 10,
    page: 0,
  });
  
  useEffect(() => {
    fetchGetData('/api/reagent/forecast', setRows);
  }, []);

  return (
    <DataGrid 
      rows={rows} 
      columns={columns} 
      density="standard"
      keepNonExistentRowsSelected
      initialState={{
        sorting: {
          sortModel: [{ field: 'recommendedOrder', sort: 'desc' }]
        },
      }}
      paginationModel={paginationModel}
      onPaginationModelChange={setPaginationModel}
      pageSizeOptions={[10, 20, 50]}
      getRowClassName={(params) => 
        params.indexRelativeToCurrentPage % 2 === 0 ? styles.evenRow : styles.oddRow
      }

      sx={{ 
        borderRadius:'12px', 
        boxShadow:'0 4px 20px rgba(0, 0, 0, 0.05)' 
      }}
    />
  );
}


export default ReagentForecastTable;