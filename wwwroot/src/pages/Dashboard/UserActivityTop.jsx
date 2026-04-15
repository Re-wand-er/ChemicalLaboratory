import { useState, useEffect } from 'react';
import { DataGrid } from '@mui/x-data-grid';

import SimpleDataTable from '../../components/DataTable/SimpleDataTable';

import { fetchGetData } from '../../api/fetch';

const UserActivityTop = () => {
  const [rows, setRows] = useState([]);
  const [days, setDays] = useState(1);

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
    <div style={{ width: '100%', maxWidth: '400px' }}>
      <div style={{ marginBottom: '8px', textAlign: 'right' }}>
				<label>Сумма операций пользователей за период</label>
        <select 
           style={{ fontSize: '0.75rem', padding: '2px' }} 
           value={days} 
           onChange={(e) => setDays(Number(e.target.value))}
        >
          <option value={1}>Сегодня</option>
          <option value={7}>За неделю</option>
        </select>
      </div>

      <SimpleDataTable
			  rows={rows}
        columns={columns}
		  />  
    </div>
  );
};

export default UserActivityTop;
