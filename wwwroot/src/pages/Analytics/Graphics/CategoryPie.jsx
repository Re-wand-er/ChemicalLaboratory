import { useState, useEffect } from 'react';
import { Box, Paper, Typography, useTheme } from '@mui/material';
import { PieChart, Pie, Tooltip, ResponsiveContainer, Legend, Cell } from "recharts";

import { fetchGetData } from '../../../api/fetch.js';

const COLORS = 'var(--mui-palette-charts-variants)';

/**
* Диаграмма состоит из 2-ух частей:
* Внут-яя - все категории (data01 = [{name:<категория>,value:<сумма по всем реагентам>}, ...])
* Внешняя - реагенты для конкретной категории (data02 = [{name:<назв. реагента>, value:<кол-во>}, ...])
* Сумма кол-ва реагента = кол-ву его категории
*/
const CategoryPie = ({ title }) => {
  const [reportData, setReportData] = useState({ categories: [], reagents: [] });

  useEffect(() => {
    fetchGetData('/api/reagent/stock-distribution', setReportData);
  }, []);

  return (
    <Paper 
      elevation={0} 
      sx={{ 
        p: 2, 
        border: '1px solid', 
        borderColor: 'divider', 
        borderRadius: 2,
        //width: 'fit-content', // Оборачиваем по размеру контента
        height: '100%',
        display: 'flex',
        flexDirection: 'column'
      }}
    >
      <Typography variant="subtitle1" sx={{ fontWeight: 700, mb: 2, textAlign: 'center' }}>
        {title}
      </Typography>

      <Box sx={{ width: '100%', height: '100%' }}>
        <ResponsiveContainer width="100%" height="100%">
          <PieChart>
            <Pie
              data={reportData.categories}
              dataKey="value"
              nameKey="name"
              cx="50%"
              cy="50%"
              outerRadius="50%"
              fill='var(--mui-palette-charts-main)'
            />

            <Pie
              data={reportData.reagents}
              dataKey="value"
              nameKey="name"
              cx="50%"
              cy="50%"
              innerRadius="60%"
              outerRadius="80%"
              fill='var(--mui-palette-charts-variants-3)'
              label
            />

            <Tooltip 
              cursor={{ fill: 'var(--mui-palette-charts-cursor)' }}
              contentStyle={{ 
                backgroundColor: 'var(--mui-palette-charts-tooltip-bg)',
                borderColor: 'var(--mui-palette-charts-tooltip-border)',
                boxShadow: 'var(--mui-palette-charts-tooltip-shadow)',
                borderRadius: '8px'
              }}
              itemStyle={{ color: 'var(--mui-palette-charts-tooltip-text)' }}
            />
          </PieChart>
        </ResponsiveContainer>
      </Box>
    </Paper>
  );
}

export default CategoryPie;