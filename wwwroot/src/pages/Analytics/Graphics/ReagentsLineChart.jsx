import { useState, useEffect, useCallback } from 'react';
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';

import { fetchGetData } from '../../../api/fetch.js';

import "./graphicStyles.css";

const colors = [
  'var(--main-graphic-color)', 
  'var(--chart-line-sub)', 
  'var(--chart-line-sub2)', 
  'var(--pie-fill-sub2)', 
  'var(--chart-line-sub3)'
];

const getAvailableSteps = (filters) => {
    switch (filters.period) {
      case 'Week': return [{ v: 'Day', n: 'День' }];
      case 'Month': return [{ v: 'Day', n: 'День' }, { v: 'Week', n: 'Неделя' }];
      case 'Quarter': return [{ v: 'Week', n: 'Неделя' }, { v: 'Month', n: 'Месяц' }];
      case 'HalfYear': return [{ v: 'Month', n: 'Месяц' }];
      case 'Year': return [{ v: 'Month', n: 'Месяц' }, {v: 'Quarter', n: 'Квартал'}];
      default: return [{ v: 'Day', n: 'День' }];
    }
  };

/**
 * Потребление 5 самых часто используемых реагентов по периодам день/неделя/месяц
 * const data = [{ name: '<ис-мый диапазон дат>', uv: 4000, pv: 2400, amt: 2400, }, ...]
 * Использование в конкретный день/неделя/месяц
 */
const ReagentsLineChart = () => {
  const [data, setData] = useState({ topReagentNames: [], chartData: [] });
  const [filters, setFilters] = useState({
    period: "Month",
    step: "Week"
  });

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
    <div style={{ width: '100%', height: '420px', marginBottom: '20px',}}> 
      <div style={{display: 'flex', justifyContent: 'center'}}>
        <label>
        Период:
        <select value={filters.period} onChange={handlePeriodChange}>
          <option value="Week">Неделя</option>
          <option value="Month">Месяц</option>
          <option value="Quarter">Квартал</option>
          <option value="HalfYear">Пол года</option>
          <option value="Year">Год</option>
        </select>
      </label>

      <label>
        Шаг:
        <select value={filters.step} onChange={(e) => setFilters({...filters, step: e.target.value})}>
          {getAvailableSteps(filters).map(s => (
            <option key={s.v} value={s.v}>{s.n}</option>
          ))}
        </select>
      </label>
      </div>

      <ResponsiveContainer>
        <LineChart 
          data={data.chartData}>

          <CartesianGrid strokeDasharray="3 3" stroke="var(--chart-grid)" />
          <XAxis dataKey="name" />
          <YAxis />
          <Tooltip />
          <Legend />
          {/* Генерируем линии на основе полученных имен */}
          {data.topReagentNames.map((name, index) => (
            <Line
              key={name}
              type="monotone"
              dataKey={name}
              stroke={colors[index % colors.length]}
              activeDot={{ r: 8 }}
            />
          ))}
        </LineChart>
      </ResponsiveContainer>
    </div>
  );
};

export default ReagentsLineChart;