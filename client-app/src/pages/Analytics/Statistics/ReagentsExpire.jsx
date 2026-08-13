import { useState, useEffect } from 'react';

import StatisticCardTemplate from './StatisticCardTemplate.jsx';  

import { formatDate } from "../../../utils/formatDate.js";  
import { fetchGetData } from '../../../api/fetch.js'; 

const columns = [
  { field: 'id', headerName: 'ID', width: 50 },
  { field: 'name', headerName: 'Реагент', flex: 1 },
  { 
    field: 'expirationDate', 
    headerName: 'Срок до', 
    width: 110,
    valueFormatter: (params) => formatDate(params, 'date') 
  },
  { 
    field: 'daysRemaining', 
    headerName: 'Дней осталось', 
    width: 150,
    renderCell: (params) => {
      const days = params.value;
      let color = 'inherit'; 
      
      if (days <= 30) color = 'var(--mui-palette-error-main)'; 
      else if (days <= 60) color = 'var(--mui-palette-warning-main)';
      return (
        <span style={{ color, fontWeight: Number(days) <= 60 ? 'bold' : 'normal' }}>
          {days < 0 ? `Просрочен (${Math.abs(days)})` : days}
        </span>
      );
    }
  }
];

/**
 * Список реактивов с истекающим сроком (ближайшие 30/60/90 дней)
 * Цвет текста поля с ближ. сроком минимума - красный?
 */
const ReagentsExpire = ({ title }) => {
  const [rows, setRows] = useState([]);
  
    useEffect(() => {
      fetchGetData('/api/reagent/expiring', setRows);
    }, []);

  return (
    <StatisticCardTemplate
      title={title}
      rows={rows}
      columns={columns}
      typographyColor='var(--mui-palette-warning-main)'
    />
  );
};

export default ReagentsExpire;