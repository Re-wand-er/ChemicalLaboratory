import { useEffect, useState } from 'react';
import {
  Dialog, DialogTitle, DialogContent,
  TextField, Grid, FormControl, InputLabel,
  Select, MenuItem, FormControlLabel, Switch
} from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';

import { DataTableDialogActions } from "../../../components/DataTable/DataTableDialogElements.jsx";
import { formatDate } from "../../../utils/formatDate.js";
import { getRecordsArray } from '../../../utils/getRecordsArray.js';

// Упрощённые колонки для отображения в режиме удаления
const deleteColumns = [
  { field: 'id', headerName: 'ID', width: 30 },
  { field: 'notificationType', headerName: 'Тип', width: 150 },
  { field: 'message', headerName: 'Заголовок', width: 150 },
  { field: 'createdAt', headerName: 'Создан', width: 120, valueFormatter: (value) => formatDate(value, 'date') },
  { 
    field: 'isRead',
        headerName: 'Активен',
        width: 40,
        type: 'boolean',
        renderCell: (params) => (
        <span
            style={{
            color: params.value ? 'green' : 'red',
            fontWeight: 'bold',
            }}
        >
            {params.value ? '✓ Да' : '✗ Нет'}
        </span>
        ),
  }
];

// Варианты типов уведомлений
const notificationTypeOptions = [
  { value: 'info', label: 'Информация' },
  { value: 'warning', label: 'Предупреждение' },
  { value: 'error', label: 'Ошибка' },
  { value: 'success', label: 'Успех' }
];

// Функция формирования начальных данных формы
const getFormData = (record = {}) => ({
  id: record.id || null,
  reagentId: record.reagentId || 0, 
  userId: record.userId || 0, 
  title: record.title || '',
  message: record.message || '',
  type: record.notificationType || 'info',
  isRead: record.isRead !== undefined ? record.isRead : false,
  createdAt: record.createdAt || ''
});

const DialogNotifications = ({ modalMode, currentRecord, reagents, users, handleClose, handleSave, handleDelete, handleAdd }) => {
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
    // При редактировании не отправляем createdAt и isRead (если нужно)
    const { createdAt, ...updateDto } = formData;
    handleSave(updateDto);
  };

  return (
    <Dialog open={modalMode !== null} onClose={handleClose} disableRestoreFocus>
      <DialogContent>
        {modalMode === 'delete' && currentRecord ? (
          <>
            <DialogTitle>
              {currentRecord.size > 1 ? 'Удалить группу уведомлений?' : `Удалить уведомление: ${currentRecord.title || currentRecord.name || '?'}`}
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
            <DialogTitle children={modalMode === 'add' ? 'Добавить уведомление' : `Изменить уведомление`} />
            <Grid container spacing={2} sx={{ mt: 0 }}>
              <Grid size={12}>
                <TextField
                  autoFocus
                  required
                  label="Заголовок"
                  fullWidth
                  value={formData.title}
                  onChange={handleChange('title')}
                />
              </Grid>
              <Grid size={12}>
                <TextField
                  required
                  label="Текст уведомления"
                  fullWidth
                  multiline
                  rows={4}
                  value={formData.message}
                  onChange={handleChange('message')}
                />
              </Grid>
              
              <Grid size={4}>
                <FormControl fullWidth>
                  <InputLabel>Пользователь</InputLabel>
                  <Select
                    value={formData.userId}
                    label="Пользователь"
                    onChange={handleChange('userId')}
                  >
                    <MenuItem value={0}>Не выбрано</MenuItem>
                    {users.map(us => (
                      <MenuItem key={us.id} value={us.id}>{us.name}</MenuItem>
                    ))}
                  </Select>
                </FormControl>
              </Grid>

              <Grid size={4}>
                <FormControl fullWidth>
                  <InputLabel>Реагент</InputLabel>
                  <Select
                    value={formData.reagentId}
                    label="Реагент"
                    onChange={handleChange('reagentId')}
                    required
                    >
                    <MenuItem value={0}>Не выбрано</MenuItem>
                    {reagents.map(reg => (
                      <MenuItem key={reg.id} value={reg.id}>{reg.name}</MenuItem>
                    ))}
            
                  </Select>
                </FormControl>
              </Grid>
              
              <Grid size={4}>
                <FormControl fullWidth>
                  <InputLabel>Тип уведомления</InputLabel>
                  <Select
                    value={formData.type}
                    label="Тип уведомления"
                    onChange={handleChange('type')}
                  >
                    {notificationTypeOptions.map(option => (
                      <MenuItem key={option.value} value={option.value}>
                        {option.label}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
              </Grid>

              {/* <Grid size={4}>
                <FormControlLabel
                  control={
                    <Switch
                      checked={formData.isRead}
                      onChange={(e) => setFormData({ ...formData, isRead: e.target.checked })}
                    />
                  }
                  label="Прочитано"
                />
              </Grid> */}

              {modalMode === 'edit' && formData.createdAt && (
                <Grid size={12}>
                  <TextField
                    label="Дата создания"
                    fullWidth
                    disabled
                    value={formatDate(formData.createdAt, 'datetime')}
                  />
                </Grid>
              )}
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

export default DialogNotifications;