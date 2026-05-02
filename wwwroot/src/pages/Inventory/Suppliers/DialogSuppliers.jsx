import { useEffect, useState } from 'react';
import {
  Dialog, DialogTitle, DialogContent,
  TextField, Grid
} from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';

import { DataTableDialogActions, DataTableDialogLabel } from "../../../components/DataTable/DataTableDialogElements.jsx";
import { getRecordsArray } from '../../../utils/getRecordsArray.js';

// Упрощённые колонки для отображения в режиме удаления
const deleteColumns = [
  { field: 'id', headerName: 'ID', width: 50 },
  { field: 'name', headerName: 'Наименование', width: 200 }
];

// Функция формирования начальных данных формы
const getFormData = (record = {}) => ({
  id: record.id || null,
  name: record.name || '',
  contactInfo: record.contactInfo || '',
  address: record.address || ''
});

const DialogSuppliers = ({ modalMode, currentRecord, handleClose, handleSave, handleDelete, handleRestore, handleAdd }) => {
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
        {modalMode === 'delete' || modalMode === 'restore' && currentRecord ? (
          <>
            <DataTableDialogLabel
              modalMode={modalMode}
              size={currentRecord.size}
              deleteOne={`Удалить поставщика: ${currentRecord.name}`}
              deleteMany="Удалить группу поставщиков?"
              restoreOne={`Восстановить поставщика: ${currentRecord.name}`}
              restoreMany="Восстановить группу поставщиков?"
            />

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
            <DialogTitle children={modalMode === 'add' ? 'Добавить поставщика' : `Изменить поставщика`} />
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
                  label="Контактная информация"
                  fullWidth
                  value={formData.contactInfo}
                  onChange={handleChange('contactInfo')}
                />
              </Grid>
              <Grid size={12}>
                <TextField
                  label="Адрес"
                  fullWidth
                  multiline
                  rows={2}
                  value={formData.address}
                  onChange={handleChange('address')}
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
        handleRestore={handleRestore}
        handleSave={onSave}
        handleClose={handleClose}
      />
    </Dialog>
  );
};

export default DialogSuppliers;