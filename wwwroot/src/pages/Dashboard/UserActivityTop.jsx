import { useState, useEffect } from 'react';
import { FormControl, MenuItem, Select } from '@mui/material';

import StatisticCardTemplate from '../Analytics/Statistics/StatisticCardTemplate';

import { fetchGetData } from '../../api/fetch';

const UserActivityTop = ({ title }) => {
  const [rows, setRows] = useState([]);
  const [days, setDays] = useState(7);

  useEffect(() => {
    fetchGetData(`/api/dashboard/user-activity?days=${days}`, setRows);
  }, [days]);

  const columns = [
    { field: 'fullName', headerName: 'Сотрудник', flex: 1 },
    { 
      field: 'operationsCount', 
      headerName: 'Операций', 
      width: 120, 
      align: 'center',
      renderCell: (params) => (
        <b style={{ color: 'var(--primary-main)' }}>{params.value}</b>
      )
    }
  ];

  return (
    <StatisticCardTemplate
      rows={rows}
      columns={columns}
      title={title}
      typographyColor='var(--mui-palette-text-secondary)'
    >
      <FormControl
        size='small'
      >
        <Select 
          //style={{ fontSize: '0.75rem', padding: '2px' }} 
          value={days} 
          onChange={(e) => setDays(e.target.value)}
        >
          <MenuItem value={1}>Сегодня</MenuItem>
          <MenuItem value={7}>За неделю</MenuItem>
        </Select>
      </FormControl>
    </StatisticCardTemplate>
  );
};

export default UserActivityTop;
