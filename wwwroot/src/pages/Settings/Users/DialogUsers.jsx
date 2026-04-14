import { useEffect, useState } from 'react';
import { 
  Dialog, DialogTitle, DialogContent,
  TextField, Grid, FormControl, InputLabel, 
  Select, MenuItem, FormControlLabel, Switch
} from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';

import { DataTableDialogActions } from "../../../components/DataTable/DataTableDialogAttribute.jsx";
import { getRecordsArray } from '../../../utils/getRecordsArray.js';  

// Упрощённые колонки для отображения в режиме удаления
  const deleteColumns = [
    { field: 'id', headerName: 'ID', width: 30},
    { field: 'login', headerName: 'Логин', width: 120 },
    { field: 'email', headerName: 'Email', width: 170 },
    { field: 'lastName', headerName: 'Фамилия', width: 100 },
    { field: 'firstName', headerName: 'Имя', width: 100 }
  ];
  const sexOptions = [
    { value: 'M', label: 'Мужской' },
    { value: 'F', label: 'Женский' }
  ];

const systemRoleOptions = [
    { value: 'Администратор', label: 'Администратор' },
    { value: 'Пользователь', label: 'Пользователь' },
    { value: 'Наблюдатель', label: 'Наблюдатель' }
  ];

  const getFormData = (record = {}) => ({
    id: record.id || null,
    firstName: record.firstName || '',
    lastName: record.lastName || '',
    middleName: record.middleName || '',
    login: record.login || '',
    email: record.email || '',
    jobPosition: record.jobPosition || '',
    sex: record.sex || '',
    systemRole: record.systemRole || '',
    isActive: record.isActive !== undefined ? record.isActive : 1,
    idWorkSchedule: record.idWorkSchedule != null ? record.idWorkSchedule : 0,
    password: record.password || ''
  });

const DialogUsers = ({ modalMode, currentRecord, schedules, handleClose, handleSave, handleDelete, handleAdd }) => {
  const [formData, setFormData] = useState(getFormData());

  useEffect(() => {
    if (modalMode === 'add') {
        setFormData(getFormData());
    } else if (modalMode === 'edit' && currentRecord) {
        setFormData(getFormData(currentRecord));
    }
  }, [modalMode]);
  
    const handleChange = (field) => (event) => {
        setFormData({
            ...formData,
            [field]: event.target.value
        });
    };
  
    // Адаптеры
    const onAdd = () =>{
      handleAdd(formData);
    }
  
    const onSave = () =>{
      const { password, ...updateDto } = formData; // убираем password
      handleSave(updateDto);
    } 

    return (
      <Dialog open={modalMode !== null} onClose={handleClose} disableRestoreFocus>
      <DialogContent>
        {(modalMode === 'delete' && currentRecord) 
          ? 
        (
          <>
            <DialogTitle>
              {currentRecord.size > 1 ? 'Удалить группу пользователей?' : `Удалить пользователя: ${currentRecord.name}?`}
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
        )
          : 
        (
          <>
            <DialogTitle children={modalMode === 'add' ? 'Добавить пользователя' : `Изменить пользователя`} />

            <Grid container spacing={2} size={{ mt: 0 }}>

              <Grid size={{xs:12, sm: 6}}>
                <TextField
                  autoFocus
                  required
                  label="Имя"
                  fullWidth
                  value={formData.firstName}
                  onChange={handleChange('firstName')}
                />
              </Grid>

              <Grid size={{xs:12, sm: 6}}>
                <TextField
                  required
                  label="Фамилия"
                  fullWidth
                  value={formData.lastName}
                  onChange={handleChange('lastName')}
                />
              </Grid>

              <Grid size={12}>
                <TextField
                  label="Отчество"
                  fullWidth
                  value={formData.middleName}
                  onChange={handleChange('middleName')}
                />
              </Grid>

              <Grid size={{xs:12, sm: 6}}>
                <TextField
                  required
                  label="Логин"
                  fullWidth
                  value={formData.login}
                  onChange={handleChange('login')}
                />
              </Grid>

              <Grid size={{xs:12, sm: 6}}>
                <TextField
                  required
                  label="Email"
                  type="email"
                  fullWidth
                  value={formData.email}
                  onChange={handleChange('email')}
                />
              </Grid>

              {modalMode === 'add' && (
                <Grid size={12}>
                  <TextField
                    required
                    label="Пароль"
                    fullWidth
                    type='password'
                    value={formData.password}
                    onChange={handleChange('password')}
                  />
                </Grid>
              )}

              <Grid size={{xs:6, sm: 6}}>
                <TextField
                  label="Должность"
                  fullWidth
                  value={formData.jobPosition}
                  onChange={handleChange('jobPosition')}
                />
              </Grid>
       
              <Grid size={{xs:6, sm: 6}}>
                <FormControl fullWidth>
                  <InputLabel>Системная роль</InputLabel>
                  <Select
                    value={formData.systemRole}
                    label="Системная роль"
                    onChange={handleChange('systemRole')}
                  >
                    {systemRoleOptions.map(option => (
                      <MenuItem key={option.value} value={option.value}>
                        {option.label}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
              </Grid>
          
              <Grid size={3}>
                <FormControl fullWidth>
                  <InputLabel>Пол</InputLabel>
                  <Select
                    value={formData.sex}
                    label="Пол"
                    required
                    onChange={handleChange('sex')}
                  >
                    
                    <MenuItem value={0}>Не выбран</MenuItem>
                    {sexOptions.map(option => (
                      <MenuItem key={option.value} value={option.value}>
                        {option.label}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
              </Grid>

              <Grid size={9}>
                <FormControl fullWidth>
                  <InputLabel>График работы</InputLabel>
                  <Select
                    value={formData.idWorkSchedule}
                    label="График работы"
                    required
                    onChange={handleChange('idWorkSchedule')}
                  >
                    <MenuItem value={0}>Не выбран</MenuItem>
                    {schedules.map(schedule => (
                      // Обрезаем секунды
                      <MenuItem key={schedule.id} value={schedule.id}>
                        {schedule.startTime.slice(0, 5)}-{schedule.endTime.slice(0, 5)} ({schedule.workShift}) 
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
              </Grid>

              <Grid size={12}>
                <FormControlLabel
                  control={
                    <Switch
                      checked={formData.isActive}
                      onChange={(e) => setFormData({...formData, isActive: e.target.checked})}
                    />
                  }
                  label="Активен"
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

export default DialogUsers;