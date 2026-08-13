import { useState } from 'react';
import { ruRU } from '@mui/x-data-grid/locales';
import { DataGrid, useGridApiRef } from '@mui/x-data-grid';

import styles from '../../../components/DataTable/dataTable.module.css'; // Неправильно, но делать правильно лень

/**
 * Прогноз просрочки отвечает на вопрос: «Какие реактивы и когда станут непригодными к использованию из-за истечения срока годности?»
 * 
 */ 
const ReagentForecastTable = ({ columns, rows, onSelectionCountChange }) => {
  const apiRef = useGridApiRef();
  const [rowSelectionModel, setRowSelectionModel] = useState([]);
  const [paginationModel, setPaginationModel] = useState({
    pageSize: 10,
    page: 0,
  });

  const handleSelectionChange = () => {
    setRowSelectionModel(apiRef.current.getSelectedRows());
    
    // Пробрасываем длину массива (количество выбранных строк) вверх
    if (apiRef.current.getSelectedRows()) {
      onSelectionCountChange(apiRef.current.getSelectedRows());
    }
  };

  return (
    <DataGrid 
      rows={rows} 
      columns={columns} 
      density="standard"
      checkboxSelection
      keepNonExistentRowsSelected
      localeText={ruRU.components.MuiDataGrid.defaultProps.localeText}
      initialState={{
        sorting: {
          sortModel: [{ field: 'recommendedOrder', sort: 'desc' }]
        },
      }}

      apiRef={apiRef}
      onRowSelectionModelChange={handleSelectionChange}

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