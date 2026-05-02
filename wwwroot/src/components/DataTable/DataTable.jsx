import { useState } from 'react';
import { ruRU } from '@mui/x-data-grid/locales';
import { DataGrid, useGridApiRef } from '@mui/x-data-grid';
import { Stack, IconButton } from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import RestoreFromTrashIcon from '@mui/icons-material/RestoreFromTrash';

import CustomToolBar from './CustomToolBar.jsx';
import { exportCsvFile, exportJsonFile } from '../../utils/dataExportFotmat.js';

import styles from './dataTable.module.css';

const columnsWithActions = (props) => [
  ...props.columns, // существующие колонки
  {
    field: 'actions',
    headerName: 'Действия',
    width: 150,
    sortable: false,
    renderCell: (params) => {
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

  const [rowSelectionMode, setRowSelectionModel] = useState([]);
  const [localLoading, setLocalLoading] = useState(false);

  const isLoading = props.loading || localLoading;
  const useAutoHeight = props.rows.length < 10;


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

	const handleCsvExportClick = () => {
		exportCsvFile(props.fileName || "data", props.rows);
	}

	const handleJsonExportClick = () => {
		exportJsonFile(props.fileName || "data", props.rows);
	}
  /////////////////////////////////////////////////////
  
  return (
    <div className={styles.centerAlignment}>
      <div style={{ 
        width: 'fit-content', 
        maxWidth: '1400px',
        height: useAutoHeight ? 'auto' : 696,
        overflow: useAutoHeight ? 'visible' : 'auto'
      }}>
        <DataGrid
          apiRef={apiRef}
          rows={props.rows}
          columns={columnsWithActions(props)}
          autoHeight={useAutoHeight}
          localeText={ruRU}
          density="standard"

          initialState={{ 
            pagination: { paginationModel: { pageSize: 10 } } 
          }}
          pageSizeOptions={[5, 10, 20]}

          checkboxSelection
          disableRowSelectionOnClick
          onRowSelectionModelChange={setRowSelectionModel}
         
          slots={{ toolbar: CustomToolBar }}
          slotProps={{
            toolbar: {
              isLoading,
              selectedCount: apiRef.current ? apiRef.current.getSelectedRows().size : 0,
              onCreate: handleCreate,
              onRefresh: handleRefresh,
              onDelete: handleDeleteClick,
							onCsvFileCreate: handleCsvExportClick,
							onJsonFileCreate: handleJsonExportClick,
            }
          }}
          
          keepNonExistentRowsSelected
          loading={isLoading}
          showToolbar
        />
      </div>
    </div>
  );
};

export default DataTable;