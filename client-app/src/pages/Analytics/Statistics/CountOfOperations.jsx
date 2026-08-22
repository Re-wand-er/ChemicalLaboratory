import { useState, useEffect, useCallback } from 'react';
import { Box, FormControl, MenuItem, Select, Stack, Typography } from '@mui/material';

import StatisticCardTemplate from './StatisticCardTemplate.jsx';

import { fetchGetData } from '../../../api/fetch.js';

const columns = (filters) => [
  { field: 'name', headerName: filters.groupBy === 'Type' ? 'Тип операции' : 'Пользователь', flex: 1 },
  { field: 'value', headerName: 'Кол-во записей', width: 150 },
];

/**
 * Таблица количество операций по типам – это просто число записей в ReagentOperations, сгруппированных по OperationTypeId.
 * Можно 2 варианта: по типам, по пользователям. Также select по периоду выборки
 */
const CountOfOperations = ({ title }) => {
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
    <StatisticCardTemplate 
      rows={rows} 
      columns={columns(filters)} 
      title={title}
      typographyColor='var(--mui-palette-primary-main)'
    >        

      <Box display='flex' gap={1}>
        <FormControl size='small'>
          <Select name="groupBy" value={filters.groupBy} onChange={handleFilterChange}>
            <MenuItem value="Type">По операциям</MenuItem>
            <MenuItem value="User">По пользователям</MenuItem>
          </Select>
        </FormControl>

        <FormControl size='small'>
          <Select name="period" value={filters.period} onChange={handleFilterChange}>
            <MenuItem value="Month">30 дней</MenuItem>
            <MenuItem value="TwoMonth">60 дней</MenuItem>
            <MenuItem value="Quarter">90 дней</MenuItem>
          </Select>
        </FormControl>
      </Box>

    </StatisticCardTemplate>
  );
};

export default CountOfOperations;