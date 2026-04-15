import { useState, useEffect } from 'react';

import SimpleDataTable from '../../components/DataTable/SimpleDataTable';

import { fetchGetData } from '../../api/fetch';

const RecentActivityFeed = () => {
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
    <div style={{ width: '100%' }}> {/*backgroundColor: 'var(--surface-base)'*/}
			<label>Последние пользовательские операции</label>
      <SimpleDataTable 
        rows={rows}
        columns={columns}
        />
    </div>
  );
};

export default RecentActivityFeed;
