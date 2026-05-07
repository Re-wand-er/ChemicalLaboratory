import { DataGrid } from '@mui/x-data-grid';

import styles from './dataTable.module.css';

const ReportDataTable = ({ rows, columns }) => {
  return (
    <DataGrid
      rows={rows}
      columns={columns}
      pageSize={10}
      initialState={{
        pagination: { paginationModel: { pageSize: 10 } }
      }}
      pageSizeOptions={[10, 20, 50]}
      rowsPerPageOptions={[10]}
      disableRowSelectionOnClick
      density="compact"
      autoHeight
    
      getRowClassName={(params) => 
        params.indexRelativeToCurrentPage % 2 === 0 ? styles.evenRow : styles.oddRow
      }
    />
  );
};

export default ReportDataTable;