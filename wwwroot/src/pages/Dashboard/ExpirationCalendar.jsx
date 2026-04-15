import { useState, useEffect } from 'react';

import SimpleDataTable from '../../components/DataTable/SimpleDataTable';

import { fetchGetData } from '../../api/fetch';

const ExpirationCalendar = () => {
  const [rows, setRows] = useState([]);

  useEffect(() => {
    fetchGetData('/api/dashboard/expiration-calendar', setRows);
  }, []);

  const columns = [
		{ 
			field: 'name', 
			headerName: 'Реагент', 
			flex: 1,
			renderCell: (params) => (
				<span style={{ fontSize: '0.85rem' }}>{params.value}</span>
			)
		},
    { 
      field: 'daysRemaining', 
      headerName: 'Дата', 
      width: 140,
      // renderCell: (params) => {

			// 	if (!params.row || !params.row.formattedDate) return null;

			// 	const dateStr = params.row.formattedDate;
			// 	const isUrgent = dateStr.includes('Сегодня') || dateStr.includes('Завтра');

			// 	return (
			// 		<span style={{ 
			// 			color: isUrgent ? '#d32f2f' : 'inherit', 
			// 			fontWeight: isUrgent ? 'bold' : 'normal',
			// 			fontSize: '0.85rem'
			// 		}}>
			// 			{dateStr}
			// 		</span>
			// 	);
			// }
    },
		// {
		// 	field: 'expirationDate', 
    //   headerName: 'Дата2', 
    //   width: 140,
		// },
  ];

  return (
    <div style={{ width: '100%', maxWidth: '400px' }}>
			<label>Самые близкие к просрочке</label>
			<SimpleDataTable
				rows={rows}
        columns={columns}
			/>
    </div>
  );
};

export default ExpirationCalendar;
