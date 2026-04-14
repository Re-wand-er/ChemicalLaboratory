import { useState, useEffect } from 'react';
import SimpleDataTable from '../../../components/DataTable/SimpleDataTable.jsx';

import { fetchGetData } from '../../../api/fetch.js';

import './statisticStyle.css';

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
      <b style={{ color: params.value > 1 ? 'var(--main-ok)' : 'var(--main-error)' }}>
        {params.value}
      </b>
    )
  },
];

/**
 * Коэффициент оборачиваемости 
 * за определенный период месяц/квартал/год/все время
 */
const ReagentsTurnOverRatio = () => {
  const [rows, setRows] = useState([]);
  const [period, setPeriod] = useState('Month');

  useEffect(() => {
    fetchGetData(`/api/reagent-operation/turnover?period=${period}`, setRows);
  }, [period]);

  return (
    <div style={{ width: '100%', maxWidth: '600px' }}>
      <div style={{ marginBottom: '10px' }}>
        <label>Анализ оборачиваемости: </label>
        <select value={period} onChange={(e) => setPeriod(e.target.value)}>
          <option value="Month">Месяц</option>
          <option value="Quarter">Квартал</option>
          <option value="Year">Год</option>
          <option value="AllTime">Все время</option>
        </select>
      </div>

      <SimpleDataTable
        rows={rows}
        columns={columns}
      />
      
    </div>
  );
};

export default ReagentsTurnOverRatio;