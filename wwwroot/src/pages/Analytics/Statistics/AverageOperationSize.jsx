import { useState, useEffect } from 'react';
import SimpleDataTable from '../../../components/DataTable/SimpleDataTable.jsx';

import { fetchGetData } from '../../../api/fetch.js';

const columns = [
  { field: 'name', headerName: 'Тип операции', flex: 1 },
  { 
    field: 'value', 
    headerName: 'Средний размер', 
    width: 180,
    valueFormatter: (value) => `${value.toFixed(2)} ед.`
  },
];

/**
 * Средний размер одной операции (по типам)
 * за определенный период месяц/квартал/год/все время
 */
const AverageOperationSize = () => {
  const [rows, setRows] = useState([]);
  const [period, setPeriod] = useState('Month');

  useEffect(() => {
    fetchGetData(`/api/reagent-operation/average-size?period=${period}`, (data) => {
      setRows(data.map((item, index) => ({ id: index, ...item })));
    });
  }, [period]);

  return (
    <div style={{ width: '100%', maxWidth: '450px' }}>
      <div style={{ marginBottom: '10px' }}>
        <label>Период анализа: </label>
        <select value={period} onChange={(e) => setPeriod(e.target.value)}>
          <option value="Month">Месяц</option>
          <option value="Quarter">Квартал (90 дн)</option>
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

export default AverageOperationSize;