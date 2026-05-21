import { useState } from 'react';
import { ruRU } from '@mui/x-data-grid/locales';
import { DataGrid, useGridApiRef } from '@mui/x-data-grid';
import { Stack, IconButton, useTheme, Paper } from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import RestoreFromTrashIcon from '@mui/icons-material/RestoreFromTrash';
import RemoveCircleOutlineIcon from '@mui/icons-material/RemoveCircleOutline';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';

import CustomToolBar from './CustomToolBar.jsx';

import styles from './dataTable.module.css';

const columnsWithActions = (props) => [
  ...props.columns, // существующие колонки
  {
    field: 'actions',
    headerName: 'Действия',
    width: 200,
    sortable: false,
    renderCell: (params) => {

      const handleWriteOff = () => {
        if (props.onWriteOff) props.onWriteOff(params.row);
        else console.error('Не найден обработчик для обновления записи');
      };

      const handleIncome = () => {
        if (props.onIncome) props.onIncome(params.row);
        else console.error('Не найден обработчик для обновления записи');
      };

      const handleEdit = () => {
        if (props.onEdit) props.onEdit(params.row);
        else console.error('Не найден обработчик для обновления записи');
      };

      const handleDelete = () => {
        if (props.onDelete) props.onDelete(params.row);
        else console.error('Не найден обработчик для удаления записи');
      };

      const handleRestore = () => {
        if (props.onRestore) props.onRestore(params.row);
        else console.error('Не найден обработчик для восстановления записи');
      };

      return (
        <Stack direction="row" spacing={1}>
          <IconButton
            color="warning"
            onClick={handleWriteOff}
            size="medium"
          >
            <RemoveCircleOutlineIcon />
          </IconButton>

          <IconButton
            color="success"
            onClick={handleIncome}
            size="medium"
          >
            <AddCircleOutlineIcon />
          </IconButton>

          <IconButton
            color="primary"
            onClick={handleEdit}
            size="medium"
          >

            <EditIcon />
          </IconButton>

          {params.row.isActive &&
            <IconButton
              color="error"
              onClick={handleDelete}
              size="medium"
            >

              <DeleteIcon />
            </IconButton>
          }

          {(props.isSuperAdmin && !params.row.isActive) &&
            <IconButton 
              color="success" 
              onClick={handleRestore} 
              size="medium"
            >
              <RestoreFromTrashIcon />
            </IconButton>
          }
        </Stack>
      );
    }    
  }
];

const DataTable = (props) => {
  const apiRef = useGridApiRef();
  const theme = useTheme();

  const [rowSelectionModel, setRowSelectionModel] = useState([]);
  const [localLoading, setLocalLoading] = useState(false);

  const isLoading = props.loading || localLoading;

  ///// Служат адапторами для шапки ///////////////////
  const handleCreate = () => props.onAdd?.();
  
  const handleRefresh = async (value) => {
    setLocalLoading(true);

    try { await props.onRefresh?.(value); } 
    finally { setLocalLoading(false); }
  };

  const handleDeleteClick = () => {
    const rows = apiRef.current.getSelectedRows(); 
    if (rows.size > 0) { props.onDelete?.(rows);}
  };

  const handleWriteOff = () => {
    const selectedMap = apiRef.current.getSelectedRows(); 
    if (selectedMap.size > 0) { 
      const selectedArray = Array.from(selectedMap.values());
      props.onWriteOff?.(selectedArray); 
    }
  };

  const handleOrder = () => {
    const selectedMap = apiRef.current.getSelectedRows(); 
    if (selectedMap.size > 0) { 
      const selectedArray = Array.from(selectedMap.values());
      props.onOrder?.(selectedArray); 
    }
  };

  const handleIncome = () => {
    const rows = apiRef.current.getSelectedRows(); 
    if (rows.size > 0) { props.onIncome?.(rows);}
  };
  /////////////////////////////////////////////////////

  return (
    <Paper 
      elevation={0} 
      sx={{ 
        width: '100%',
        border: '1px solid',
        borderColor: 'divider',
        borderRadius: 3, 
        overflow: 'hidden',
        boxShadow: '0 4px 20px rgba(0, 0, 0, 0.05)'
      }}
    >
      <DataGrid
        apiRef={apiRef}
        rows={props.rows}
        columns={columnsWithActions(props)}
        autoHeight={true}
        localeText={ruRU.components.MuiDataGrid.defaultProps.localeText}
        density="standard"
        checkboxSelection
        disableRowSelectionOnClick
        onRowSelectionModelChange={setRowSelectionModel}
        keepNonExistentRowsSelected
        loading={isLoading}
        showToolbar
        slots={{ toolbar: CustomToolBar }}
        slotProps={{
          toolbar: {
            isLoading,
            selectedCount: apiRef.current ? apiRef.current.getSelectedRows().size : 0,
            onCreate: handleCreate,
            onRefresh: handleRefresh,
            onDelete: handleDeleteClick,
            onWriteOff: handleWriteOff,
            onIncome: handleIncome,
            onOrder: handleOrder,
            onQrIncome: props.onQrIncome,
            rows: props.rows,
            columns: columnsWithActions(props)
          }
        }}
        sx={{
          border: 'none',
          '& .MuiDataGrid-columnHeaders': {
            backgroundColor: 'var(--mui-palette-background-default)',
            borderBottom: '2px solid var(--mui-palette-divider)',
          },
          '& .MuiDataGrid-cell': {
            borderBottom: '1px solid var(--mui-palette-divider)',
          },
          '& .MuiDataGrid-cell:focus': {
            outline: 'none',
          },
        }}
        getRowClassName={(params) => 
          params.indexRelativeToCurrentPage % 2 === 0 ? styles.evenRow : styles.oddRow
        }
        initialState={{
          pagination: { paginationModel: { pageSize: 10 } }
        }}
        pageSizeOptions={[10, 20, 50]}
      />
    </Paper>
  );
};

export default DataTable;