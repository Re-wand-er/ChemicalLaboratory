import { useState, useEffect } from 'react';

import StatisticCardTemplate from '../Analytics/Statistics/StatisticCardTemplate';

import { fetchGetData } from '../../api/fetch';

const ExpirationCalendar = ({ title }) => {
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

export default ExpirationCalendar;
