import { useState, useEffect, useCallback } from 'react';
import { BarChart, Bar, XAxis, YAxis, Legend, ResponsiveContainer, Tooltip } from "recharts";
import { Box, Paper, Typography, FormControl, Select, MenuItem, Stack, InputLabel, useTheme } from '@mui/material';

import { fetchGetData } from '../../../api/fetch.js';

/**
 * Аналитический компонент потребления реагентов
 */
const ReagentsBarChart = ({ startPeriod = 'Month', localeWidth = '800px', filterBar = false, title }) => {
  const [chartData, setChartData] = useState([]);
  const [filters, setFilters] = useState({
    period: startPeriod,
    count: 5,
    asc: false
  });

  const loadData = useCallback(async () => {
    const baseUrl = '/api/reagent-operation/top-usage';
    const urlParams = new URLSearchParams({
      period: filters.period,
      count: filters.count,
      asc: filters.asc
    });

    const fullUrl = `${baseUrl}?${urlParams.toString()}`;

    try {
      await fetchGetData(fullUrl, setChartData);
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
    <Box>
      <Stack 
        direction="row"  
        alignItems="center" 
        sx={{ 
          mb: 2, 
          justifyContent: title && filterBar ? "space-between" : "center" 
        }}
      >
        <Typography variant="subtitle1" sx={{ fontWeight: 700 }} color='var(--mui-palette-text-secondary)'>
          {title}
        </Typography>
        
        {filterBar && (
          <Stack direction="row" spacing={1}>
            <FormControl size="small" sx={{ minWidth: 100 }}>
              <InputLabel>Период</InputLabel>
              <Select
                name="period"
                label="Период"
                value={filters.period}
                onChange={handleFilterChange}
              >
                <MenuItem value="Day">День</MenuItem>
                <MenuItem value="Week">Неделя</MenuItem>
                <MenuItem value="Month">Месяц</MenuItem>
                <MenuItem value="Year">Год</MenuItem>
              </Select>
            </FormControl>

            <FormControl size="small" sx={{ minWidth: 70 }}>
              <InputLabel>Топ</InputLabel>
              <Select
                name="count"
                label="Топ"
                value={filters.count}
                onChange={handleFilterChange}
              >
                <MenuItem value="5">5</MenuItem>
                <MenuItem value="10">10</MenuItem>
                <MenuItem value="15">15</MenuItem>
              </Select>
            </FormControl>

            <FormControl size="small" sx={{ minWidth: 140 }}>
              <InputLabel>Сортировка</InputLabel>
              <Select
                name="asc"
                label="Сортировка"
                value={filters.asc.toString()}
                onChange={handleFilterChange}
              >
                <MenuItem value="false">По убыванию</MenuItem>
                <MenuItem value="true">По возрастанию</MenuItem>
              </Select>
            </FormControl>
          </Stack>
        )}
      </Stack>

      <Box sx={{ height: 400, width: '100%' }}>
        <ResponsiveContainer width="100%" height="100%">
          <BarChart 
            data={chartData} 
            layout="vertical" 
            margin={{ left: 20, right: 30 }}
          >
            <XAxis 
              type="number" 
              tick={{ fill:'var(--mui-palette-charts-text)', fontSize:12 }}
              stroke='var(--mui-palette-charts-grid)'
            />
            <YAxis 
              dataKey="name" 
              type="category" 
              tick={{ fill: 'var(--mui-palette-text-primary)', fontSize: 13 }}
              stroke='var(--mui-palette-charts-grid)'
            />
            <Tooltip 
              cursor={{ fill: 'var(--mui-palette-charts-cursor)' }}
              contentStyle={{ 
                backgroundColor: 'var(--mui-palette-charts-tooltip-bg)',
                borderColor: 'var(--mui-palette-charts-tooltip-border)',
                boxShadow: 'var(--mui-palette-charts-tooltip-shadow)',
                borderRadius: '8px'
              }}
            />
            {/* <Legend verticalAlign="bottom" align="center" height={32} /> */}
            <Bar 
              name="Потреблено (кол.)" 
              dataKey="value" 
              fill='var(--mui-palette-charts-main)' 
              radius={[0, 5, 5, 0]}
              isAnimationActive={true} 
            />
          </BarChart>
        </ResponsiveContainer>
      </Box>
    </Box>
  );
};

export default ReagentsBarChart;
