import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { 
  Grid, TextField, Button, MenuItem, FormControl, 
  InputLabel, Select, Paper, Typography, Divider 
} from '@mui/material';

import { useAuth } from '../context/AuthContext';

import { fetchGetData, fetchPutData } from '../api/fetch';

const ProfileForm = () => {
  const { user, logout } = useAuth(); 
  const navigate = useNavigate()
  const [data, setData] = useState(user);
  const [schedules, setSchedules] = useState([]);

  const isAdmin = data.systemRole === 'Администратор';

  useEffect(() => {
    fetchGetData('/api/work-schedule', setSchedules);
  }, []);

  const handleChange = (prop) => (event) => {
    setData({ ...data, [prop]: event.target.value });
  };

  const handleSave = async () => {
    const result = await fetchPutData(`api/user/${data.id}`, data, true);
    
    if(result){      
      setData(result);
      alert('Данные обновлены.');
    }
  };

  const handleRedirect = () =>{
    if(confirm("Перейти на страницу смены пароля?") === true)
      navigate('/reset-password');
  };

  return (
    <Paper sx={{ p: 4, maxWidth: 900, m: '20px auto' }}>
      <Typography variant="h5" gutterBottom>Профиль пользователя: {data.login}</Typography>
      <Typography variant="body2" color="textSecondary" mb={3}>
        Роль в системе: {data.systemRole}
      </Typography>
      
      <Grid container spacing={3}>

        <Grid size={4}>
          <TextField
            label="Фамилия"
            fullWidth
            disabled={!isAdmin}
            value={data.lastName}
            onChange={handleChange('lastName')}
          />
        </Grid>

        <Grid size={4}>
          <TextField
            label="Имя"
            fullWidth
            disabled={!isAdmin}
            value={data.firstName}
            onChange={handleChange('firstName')}
          />
        </Grid>

        <Grid size={4}>
          <TextField
            label="Отчество"
            fullWidth
            disabled={!isAdmin}
            value={data.middleName}
            onChange={handleChange('middleName')}
          />
        </Grid>

        {/* Системные данные */}
        <Grid size={6}>
          <TextField
            label="Email"
            fullWidth
            disabled 
            value={data.email}
          />
        </Grid>

        <Grid size={6}>
          <FormControl fullWidth disabled={!isAdmin}>
            <InputLabel>Пол</InputLabel>
            <Select value={data.sex} label="Пол" onChange={handleChange('sex')}>
              <MenuItem value="M">Мужской</MenuItem>
              <MenuItem value="F">Женский</MenuItem>
            </Select>
          </FormControl>
        </Grid>

        <Grid size={6}>
          <TextField
            label="Должность"
            fullWidth
            disabled={!isAdmin}
            value={data.jobPosition}
            onChange={handleChange('jobPosition')}
          />
        </Grid>

        <Grid size={6}>
          <FormControl fullWidth disabled={!isAdmin}>
            <InputLabel>Рабочая смена</InputLabel>
            <Select
              label="Рабочая смена"
              value={
                schedules.some(s => s.id === data.idWorkSchedule)
                  ? data.idWorkSchedule 
                  : (schedules.length > 0 ? schedules[0].id : '') 
              }
              onChange={handleChange('idWorkSchedule')}
            >
              {schedules.map(s => (
                <MenuItem key={s.id} value={s.id}>
                  {s.startTime.slice(0, 5)}-{s.endTime.slice(0, 5)} ({s.workShift}) 
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>

        <Grid size={12}><Divider sx={{ my: 1 }}>Безопасность</Divider></Grid>

        <Grid size={12}>

          <Typography size={8} variant="caption" color={data.isActive ? "success.main" : "error.main"}>
            Статус: {data.isActive ? "Активен" : "Заблокирован"}
          </Typography>

          <Button variant="contained" size="large" onClick={handleRedirect}>
            Изменить пароль
          </Button>

          <Button variant="contained" size="large" onClick={handleSave}>
            Сохранить данные
          </Button>
        </Grid>
      </Grid>
    </Paper>
  );
};

export default ProfileForm;