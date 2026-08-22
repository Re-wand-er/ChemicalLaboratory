import { useState, useEffect } from 'react';
import { Box, FormControl, Typography, Select, MenuItem } from '@mui/material';

import StatisticCardTemplate from './StatisticCardTemplate.jsx';

import { fetchGetData } from '../../../api/fetch.js';

const columns = [
  { field: 'name', headerName: 'Тип операции', flex: 1 },
  { 
    field: 'value', 
    headerName: 'Средний размер', 
    width: 180,
    valueFormatter: (value) => `${value.toFixed(2)} ед.`
  },
];

/**
 * Средний размер одной операции (по типам)
 * за определенный период месяц/квартал/год/все время
 */
const AverageOperationSize = ({ title }) => {
  const [rows, setRows] = useState([]);
  const [period, setPeriod] = useState('Month');

  useEffect(() => {
    fetchGetData(`/api/reagent-operation/average-size?period=${period}`, (data) => {
      setRows(data.map((item, index) => ({ id: index, ...item })));
    });
  }, [period]);

  return (
    <StatisticCardTemplate
      title={title}
      rows={rows}
      columns={columns}
      typographyColor='var(--mui-palette-primary-main)'
    >
      <Box display='flex' alignItems='center'>
        <Typography variant="body2" color="var(--mui-palette-text-secondary)">Период анализа: </Typography>
        <FormControl size='small'>
          <Select value={period} onChange={(e) => setPeriod(e.target.value)}>
            <MenuItem value="Month">Месяц</MenuItem>
            <MenuItem value="Quarter">Квартал (90 дн)</MenuItem>
            <MenuItem value="Year">Год</MenuItem>
            <MenuItem value="AllTime">Все время</MenuItem>
          </Select>
        </FormControl>
      </Box>
    </StatisticCardTemplate>
  );
};

export default AverageOperationSize;