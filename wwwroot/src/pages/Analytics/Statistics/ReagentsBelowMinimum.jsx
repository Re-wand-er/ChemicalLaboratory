import { useState, useEffect } from 'react'; 

import StatisticCardTemplate from './StatisticCardTemplate.jsx';

import { fetchGetData } from '../../../api/fetch.js';

const errorRender = (params) =>
(
  <span style={{ color: 'var(--mui-palette-error-main)', fontWeight: 'bold',  }}>
    {params.value}
  </span>
);  

const columns = [
  { field: 'id', headerName: 'ID', width: 50 },
  { field: 'name', headerName: 'Реагент', flex: 1 },
  { 
    field: 'currentQuantity', 
    headerName: 'Остаток', 
    width: 90,
    renderCell: errorRender
  },
  { 
    field: 'minQuantity', 
    headerName: 'Мин.', 
    width: 70
  },
  { 
    field: 'unit', 
    headerName: 'Ед. изм.', 
    width: 80
  },
  { 
    field: 'criticalPercent', 
    headerName: 'Процент остатка', 
    width: 90,
    renderCell: errorRender
  },
];

/**
 * Список реактивов с количеством ниже минимума
 * Цвет текста поля с кол-вом меньше минимума - красный
 */
const ReagentsBelowMinimum = ({ title }) => {
  const [rows, setRows] = useState([]);

  useEffect(() => {
    fetchGetData('/api/reagent/low-stock', setRows);
  }, []);

  return (
    <StatisticCardTemplate
      title={title}
      rows={rows}
      columns={columns}
      typographyColor='var(--mui-palette-error-main)'
    />
  );
};

export default ReagentsBelowMinimum;