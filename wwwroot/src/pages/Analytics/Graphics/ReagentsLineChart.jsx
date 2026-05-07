import { useState, useEffect, useCallback } from 'react';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import { Box, Paper, Typography, FormControl, Select, MenuItem, Stack, useTheme, InputLabel } from '@mui/material';

import { fetchGetData } from '../../../api/fetch.js';

const getAvailableSteps = (filters) => {
    switch (filters.period) {
      case 'Week': return [{ v: 'Day', n: 'День' }];
      case 'Month': return [{ v: 'Day', n: 'День' }, { v: 'Week', n: 'Неделя' }];
      case 'Quarter': return [{ v: 'Week', n: 'Неделя' }, { v: 'Month', n: 'Месяц' }];
      case 'HalfYear': return [{ v: 'Month', n: 'Месяц' }];
      case 'Year': return [{ v: 'Month', n: 'Месяц' }, {v: 'Quarter'}]; //, n: 'Квартал'
      default: return [{ v: 'Day', n: 'День' }];
    }
  };

/**
 * Потребление 5 самых часто используемых реагентов по периодам день/неделя/месяц
 * const data = [{ name: '<ис-мый диапазон дат>', uv: 4000, pv: 2400, amt: 2400, }, ...]
 * Использование в конкретный день/неделя/месяц
 */
const ReagentsLineChart = ({ title }) => {
  const theme = useTheme();
  const [data, setData] = useState({ topReagentNames: [], chartData: [] });
  const [filters, setFilters] = useState({
    period: "Month",
    step: "Week"
  });

  const COLORS = theme.palette.charts.variants;

  const loadData = useCallback(async () => {
    const baseUrl = '/api/reagent-operation/usage-trend';
     const urlParams = new URLSearchParams({
      period: filters.period,
      step: filters.step
    });

    const fullUrl = `${baseUrl}?${urlParams.toString()}`;

    try {
      await fetchGetData(fullUrl, setData);
    } catch (error) {
      console.error("Ошибка при получении отчета:", error);
    }
  }, [filters]);

  useEffect(() => {
    loadData();
  }, [loadData]);

  const handlePeriodChange = (e) => {
    const newPeriod = e.target.value;
    let defaultStep; 

    switch(newPeriod){
      case 'Week': { defaultStep = 'Day'; break; }
      case 'Month': { defaultStep = 'Day'; break; }
      case 'Quarter': { defaultStep = 'Week'; break; }
      case 'HalfYear': { defaultStep = 'Month'; break; }
      case 'Year': { defaultStep = 'Month'; break; }
    }

    setFilters({ period: newPeriod, step: defaultStep });
  };

  return (
    <Paper sx={{ p: 3, borderRadius: 2, display: 'flex', flexDirection: 'column', height: '100%' }}>
      
      <Stack 
        direction={{ xs: 'column', sm: 'row' }} 
        spacing={2} 
        justifyContent="space-between"
        alignItems="center" 
        sx={{ mb: 3 }}
      >
        <Typography variant="subtitle1" sx={{ fontWeight: 700 }}>{title}</Typography>
        
        <Stack direction="row" spacing={1}>
        <FormControl size="small" sx={{ minWidth: 120 }}>
          <InputLabel>Период</InputLabel>
          <Select
            value={filters.period}
            label="Период"
            onChange={handlePeriodChange}
          >
            <MenuItem value="Week">Неделя</MenuItem>
            <MenuItem value="Month">Месяц</MenuItem>
            <MenuItem value="Quarter">Квартал</MenuItem>
            <MenuItem value="HalfYear">Пол года</MenuItem>
            <MenuItem value="Year">Год</MenuItem>
          </Select>
        </FormControl>

        <FormControl size="small" sx={{ minWidth: 120 }}>
          <InputLabel>Шаг</InputLabel>
          <Select
            value={filters.step}
            label="Шаг"
            onChange={(e) => setFilters({...filters, step: e.target.value})}
          >
            {getAvailableSteps(filters).map(s => (
              <MenuItem key={s.v} value={s.v}>{s.n}</MenuItem>
            ))}
          </Select>
        </FormControl>
        </Stack>
      </Stack>

      <Box sx={{ width: '100%', height: 450 }}>
        <ResponsiveContainer>
          <LineChart data={data.chartData}>
            <CartesianGrid 
              strokeDasharray="3 3" 
              stroke='var(--mui-palette-charts-grid)' 
            />
            <XAxis 
              dataKey="name" 
              tick={{ fill: 'var(--mui-palette-charts-text)', fontSize: 12 }}
              tickLine={{ stroke: 'var(--mui-palette-charts-grid)' }}
            />
            <YAxis 
              tick={{ fill: 'var(--mui-palette-charts-text)', fontSize: 12 }}
              tickLine={{ stroke: 'var(--mui-palette-charts-grid)' }}
            />
            <Tooltip 
              contentStyle={{ 
                backgroundColor: 'var(--mui-palette-charts-tooltip-bg)',
                borderColor: 'var(--mui-palette-charts-tooltip-border)',
                boxShadow: 'var(--mui-palette-charts-tooltip-shadow)',
                borderRadius: '8px'
              }}
            />
            <Legend verticalAlign="top" height={36}/>
            
            {data.topReagentNames.map((name, index) => (
              <Line 
                key={name} 
                type="monotone" 
                dataKey={name} 
                stroke={COLORS[index % COLORS.length]} 
                strokeWidth={2}
                dot={{ r: 4 }}
                activeDot={{ r: 7, strokeWidth: 0 }} 
              />
            ))}
          </LineChart>
        </ResponsiveContainer>
      </Box>
    </Paper>
  );
};

export default ReagentsLineChart;