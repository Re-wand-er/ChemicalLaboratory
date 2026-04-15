import { useState, useEffect, useCallback } from 'react';
import { BarChart, Bar, XAxis, YAxis, Legend, ResponsiveContainer, Tooltip } from "recharts";

import { fetchGetData } from '../../../api/fetch.js';

/**
 * Аналитический компонент потребления реагентов
 */
const ReagentsBarChart = ({ startPeriod = 'Week', localeWidth = '800px'}) => {
  // 1. Состояние для данных и фильтров
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
    <div style={{ minHeight: '400px', width: '100%',maxWidth: localeWidth }}>
      {!startPeriod && (
        <div style={{ display: 'flex', justifyContent: 'center' }}>
          <label>
            Период:
            <select name="period" value={filters.period} onChange={handleFilterChange}>
              <option value="Day">День</option>
              <option value="Week">Неделя</option>
              <option value="Month">Месяц</option>
              <option value="Year">Год</option>
            </select>
          </label>

          <label>
            Топ:
            <select name="count" value={filters.count} onChange={handleFilterChange}>
              <option value="5">5</option>
              <option value="10">10</option>
              <option value="15">15</option>
            </select>
          </label>

          <label>
            Сортировка:
            <select name="asc" value={filters.asc.toString()} onChange={handleFilterChange}>
              <option value="false">По убыванию</option>
              <option value="true">По возрастанию</option>
            </select>
          </label>
        </div>
      )}

      <div style={{ height: '400px', minHeight: '400px' }}>
        <ResponsiveContainer >
          <BarChart
            data={chartData}
            layout="vertical"
          >
            <XAxis type="number" />
            <YAxis 
              dataKey="name" 
              type="category" 
              width={100} 
              tick={{ fontSize: 14 }}
            />
            <Tooltip />
            <Legend />
            <Bar
              name="Потреблено (ед)"
              dataKey="value"
              fill='var(--main-graphic-color)'
              radius={[0, 5, 5, 0]}
              isAnimationActive={true}
            />
          </BarChart>
        </ResponsiveContainer>
      </div>
    </div>
  );
};

export default ReagentsBarChart;
