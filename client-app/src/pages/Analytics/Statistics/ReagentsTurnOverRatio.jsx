import { useState, useEffect } from 'react';
import { Box, FormControl, Select, MenuItem } from '@mui/material';

import StatisticCardTemplate from './StatisticCardTemplate.jsx';

import { fetchGetData } from '../../../api/fetch.js';

const columns = [
  { field: 'id', headerName: 'ID', width: 50 },
  { field: 'name', headerName: 'Реагент', flex: 1 },
  { field: 'totalConsumption', headerName: 'Расход', width: 100 },
  { field: 'averageStock', headerName: 'Сред. запас', width: 110 },
  { 
    field: 'turnoverRatio', 
    headerName: 'Коэфф.', 
    width: 90,
    renderCell: (params) => (
      <b style={{ color: params.value > 1 ? 'var(--mui-palette-ok-main)' : 'var(--mui-palette-error-main)' }}>
        {params.value}
      </b>
    )
  },
];

/**
 * Коэффициент оборачиваемости 
 * за определенный период месяц/квартал/год/все время
 */
const ReagentsTurnOverRatio = ({ title }) => {
  const [rows, setRows] = useState([]);
  const [period, setPeriod] = useState('Month');

  useEffect(() => {
    fetchGetData(`/api/reagent-operation/turnover?period=${period}`, setRows);
  }, [period]);

  return (
    <StatisticCardTemplate
      rows={rows}
      columns={columns}
      title={title}
      typographyColor='var(--mui-palette-text-secondary)'
    >
      <FormControl size="small">
        <Select value={period} onChange={(e) => setPeriod(e.target.value)}>
          <MenuItem value="Month">Месяц</MenuItem>
          <MenuItem value="Quarter">Квартал</MenuItem>
          <MenuItem value="Year">Год</MenuItem>
          <MenuItem value="AllTime">Все время</MenuItem>
        </Select>
      </FormControl>

    </StatisticCardTemplate>
  );
};

export default ReagentsTurnOverRatio;