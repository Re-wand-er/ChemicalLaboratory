import { useEffect, useState } from 'react';
import {
  Dialog, DialogTitle, DialogContent,
  TextField, Grid
} from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';

import { DataTableDialogActions } from "../../../components/DataTable/DataTableDialogActions.jsx";
import { getRecordsArray } from '../../../utils/getRecordsArray.js';

// Упрощённые колонки для отображения в режиме удаления
const deleteColumns = [
  { field: 'id', headerName: 'ID', width: 60 },
  { field: 'name', headerName: 'Наименование', width: 220 }
];

// Функция формирования начальных данных формы
const getFormData = (record = {}) => ({
  id: record.id || null,
  name: record.name || '',
  description: record.description || ''
});

const DialogCategories = ({ modalMode, currentRecord, handleClose, handleSave, handleDelete, handleAdd }) => {
  const [formData, setFormData] = useState(getFormData());

  useEffect(() => {
    if (modalMode === 'add') {
      setFormData(getFormData());
    } else if (modalMode === 'edit' && currentRecord) {
      setFormData(getFormData(currentRecord));
    }
  }, [modalMode, currentRecord]);

  const handleChange = (field) => (event) => {
    setFormData({
      ...formData,
      [field]: event.target.value
    });
  };

  // Адаптеры для действий
  const onAdd = () => {
    handleAdd(formData);
  };

  const onSave = () => {
    handleSave(formData);
  };

  return (
    <Dialog open={modalMode !== null} onClose={handleClose} disableRestoreFocus>
      <DialogContent>
        {modalMode === 'delete' && currentRecord ? (
          <>
            <DialogTitle>
              {currentRecord.size > 1
                ? 'Удалить группу категорий?'
                : `Удалить категорию: ${currentRecord.name || currentRecord.title || '?'}`}
            </DialogTitle>

            <DataGrid
              rows={getRecordsArray(currentRecord)}
              columns={deleteColumns}
              autoHeight
              hideFooter
              disableRowSelectionOnClick
              disableColumnMenu
              rowHeight={28}
              columnHeaderHeight={44}
            />
          </>
        ) : (
          <>
            <DialogTitle children={modalMode === 'add' ? 'Добавить категорию' : `Изменить категорию`} />
            <Grid container spacing={2} sx={{ mt: 0 }}>
              <Grid size={12}>
                <TextField
                  autoFocus
                  required
                  label="Наименование"
                  fullWidth
                  value={formData.name}
                  onChange={handleChange('name')}
                />
              </Grid>
              <Grid size={12}>
                <TextField
                  label="Описание"
                  fullWidth
                  multiline
                  rows={3}
                  value={formData.description}
                  onChange={handleChange('description')}
                />
              </Grid>
            </Grid>
          </>
        )}
      </DialogContent>

      <DataTableDialogActions
        modalMode={modalMode}
        handleAdd={onAdd}
        handleDelete={handleDelete}
        handleSave={onSave}
        handleClose={handleClose}
      />
    </Dialog>
  );
};

export default DialogCategories;