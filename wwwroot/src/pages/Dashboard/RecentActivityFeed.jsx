import { useState, useEffect } from 'react';
import { Box, Typography } from '@mui/material';

import SimpleDataTable from '../../components/DataTable/SimpleDataTable';

import { fetchGetData } from '../../api/fetch';
import StatisticCardTemplate from '../Analytics/Statistics/StatisticCardTemplate';

const RecentActivityFeed = ({ title }) => {
  const [rows, setRows] = useState([]);
	const load = () => fetchGetData('/api/dashboard/recent-activity', setRows);

  useEffect(() => {
		load();
		
    // Обновляем данные каждые 5 минут для актуальности "RelativeTime"
    const interval = setInterval(load, 300000);

    return () => clearInterval(interval);
  }, []);

  const columns = [
  { 
    field: 'userFullName', 
    headerName: 'Сотрудник', 
    width: 150,
    renderCell: (params) => (
      <b style={{ fontSize: '0.85rem' }}>{params.value}</b>
    )
  },
  { 
    field: 'actionDetails', 
    headerName: 'Действие', 
		flex: 1,
    // width: 400,
    renderCell: (params) => (
      <span style={{ fontSize: '0.85rem' }}>{params.value}</span>
    )
  },
  { 
    field: 'relativeTime', 
    headerName: 'Время', 
    width: 100,
    align: 'right',
    renderCell: (params) => (
      <span style={{ color: 'gray', fontSize: '0.75rem' }}>
        {params.value}
      </span>
    )
  }
];

  return (
    <StatisticCardTemplate
      rows={rows}
      columns={columns}
      title={title}
      typographyColor="var(--mui-palette-text-secondary)"
    />
  );
};

export default RecentActivityFeed;
