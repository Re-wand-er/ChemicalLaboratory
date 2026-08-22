import { DataGrid } from '@mui/x-data-grid';

const SimpleDataTable = ({ rows, columns }) => {
  return (
      <DataGrid
        rows={rows}
        columns={columns}
        autoHeight
        hideFooter
        disableColumnMenu
        rowHeight={32}
        columnHeaderHeight={40}
        density="compact"
      />
  );
};

export default SimpleDataTable;