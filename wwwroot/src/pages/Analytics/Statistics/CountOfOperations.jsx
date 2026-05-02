import { useState, useEffect, useCallback } from 'react';
import { DataGrid } from '@mui/x-data-grid';

import { fetchGetData } from '../../../api/fetch.js';

const columns = (filters) => [
  { field: 'name', headerName: filters.groupBy === 'Type' ? 'Тип операции' : 'Пользователь', flex: 1 },
  { field: 'value', headerName: 'Кол-во записей', width: 150 },
];

/**
 * Таблица количество операций по типам – это просто число записей в ReagentOperations, сгруппированных по OperationTypeId.
 * Можно 2 варианта: по типам, по пользователям. Также select по периоду выборки
 */
const CountOfOperations = () => {
  const [rows, setRows] = useState([]);
  const [filters, setFilters] = useState({
    groupBy: 'Type',
    period: 'Month'
  });

  const loadData = useCallback(async () => {
    const baseUrl = '/api/reagent-operation/operations-stats';
    const urlParams = new URLSearchParams({
      groupBy: filters.groupBy,
      period: filters.period
    });

    const fullUrl = `${baseUrl}?${urlParams.toString()}`;

    try {
      await fetchGetData(fullUrl,  (data) => {
        setRows(data.map((item, index) => ({ id: index, ...item }))); // Временный id
      });
    } catch (error) {
      console.error("Ошибка при получении отчета:", error);
    }
  }, [filters]);

  useEffect(() => {
    loadData();
  }, [loadData]);

  const handleFilterChange = (e) => {
    const { name, value } = e.target;
    setFilters(prev => ({
      ...prev,
      [name]: name === 'asc' ? value === 'true' : value
    }));
  };

  return (
    <div style={{ width: '100%', maxWidth: '600px' }}>
      <div style={{ marginBottom: '10px', display: 'flex', gap: '10px' }}>
        <select name="groupBy" value={filters.groupBy} onChange={handleFilterChange}>
          <option value="Type">По категориям</option>
          <option value="User">По пользователям</option>
        </select>

        <select name="period" value={filters.period} onChange={handleFilterChange}>
          <option value="Month">30 дней</option>
          <option value="TwoMonth">60 дней</option>
          <option value="Quarter">90 дней</option>
        </select>
      </div>

      <DataGrid
        rows={rows}
        columns={columns(filters)}
        autoHeight
        hideFooter
        disableColumnMenu
        rowHeight={32}
        columnHeaderHeight={40}
        density="compact"
      />
        
    </div>
  );
};

export default CountOfOperations;