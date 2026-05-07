import { useState, useEffect, useCallback } from 'react';
import { PieChart, Pie, Legend, Tooltip, ResponsiveContainer } from 'recharts';
import { Box, Paper, Typography, FormControl, Select, MenuItem, Stack, useTheme } from '@mui/material';

import { fetchGetData } from '../../../api/fetch.js';

/**
 * Используеммые реагенты за определенное время: 30/60/90/180 дней
 * const data = [{name:'<название_реактива>', value:<кол-во потребления за период>}, ..., {<аналогично 1-му>} ]
 */
const ReagentsPie = ({ title }) => {
  const [data, setData] = useState([]);
  const [filters, setFilters] = useState({
    period: "Month"
  });

  const loadData = useCallback(async () => {
    const baseUrl = '/api/reagent-operation/top-usage';
     const urlParams = new URLSearchParams({
      period: filters.period,
      count: 50,
      asc: true
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


  const handleFilterChange = (event) => {
    const { value } = event.target;
    setFilters(prev => ({ 
      ...prev, 
      period: value 
    }));
  };

  return (
    <Paper 
      sx={{
        p:2, 
        borderRadius:2, 
        height:'100%', 
        //width:'50vh', 
        display:'flex', 
        flexDirection: 'column'
      }}>

      <Stack direction="row" justifyContent="space-between" alignItems="center" sx={{ mb: 2 }}>
        <Typography variant="subtitle1" sx={{ fontWeight: 700 }}>
          {title}
        </Typography>
        
        <Box sx={{ display: 'flex', alignItems: 'center' }}>
          <Typography variant="body2" color="var(--mui-palette-text-secondary)">Период:</Typography>
          <FormControl size="small" sx={{ minWidth: 100 }}>
            <Select
              value={filters.period}
              onChange={handleFilterChange}
              sx={{ fontSize: '0.875rem' }}
            >
              <MenuItem value="Month">30 дней</MenuItem>
              <MenuItem value="TwoMonth">60 дней</MenuItem>
              <MenuItem value="Quarter">90 дней</MenuItem>
              <MenuItem value="HalfYear">180 дней</MenuItem>
            </Select>
          </FormControl>
        </Box>
      </Stack>
    
      <Box sx={{ width: '100%', height: '100%' }}>
        <ResponsiveContainer width="100%" height="100%">
          <PieChart>

            <Pie 
              data={data}
              dataKey="value"
              nameKey="name"
              fill='var(--mui-palette-charts-main)'
              activeShape={{
                fill: 'var(--mui-palette-charts-hover)',
              }}
              label
            />  

            <Tooltip
              cursor={{
                stroke: 'var(--mui-palette-charts-cursor)'
              }}
              contentStyle={{
                backgroundColor: 'var(--mui-palette-charts-tooltip-bg)',
                borderColor: 'var(--mui-palette-charts-tooltip-border)',
                boxShadow: 'var(--mui-palette-charts-tooltip-shadow)',
                borderRadius: 4
              }}
              itemStyle={{
                color:'var(--mui-palette-charts-tooltip-text)'
              }}
            /> 
          </PieChart>
        </ResponsiveContainer>
      </Box>

    </Paper>  
  );
}

export default ReagentsPie;