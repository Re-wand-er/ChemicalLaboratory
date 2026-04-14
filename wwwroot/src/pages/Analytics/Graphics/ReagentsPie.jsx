import { useState, useEffect, useCallback } from 'react';
import { PieChart, Pie, Legend, Tooltip, ResponsiveContainer } from 'recharts';

import { fetchGetData } from '../../../api/fetch.js';

import "./graphicStyles.css";

/**
 * Используеммые реагенты за определенное время: 30/60/90/180 дней
 * const data = [{name:'<название_реактива>', value:<кол-во потребления за период>}, ..., {<аналогично 1-му>} ]
 */
const ReagentsPie = () => {
  const [data, setData] = useState([]);
  const [filters, setFilters] = useState({
    period: "Month"
  });

  const handleFilterChange = (field) => (event) => {
    setFilters({
      ...filters,
      [field]: event.target.value 
    });
  };

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


  return (
    <div style={{ width:'100%', maxWidth: '400px', height:'400px'}}>
      <div style={{display: 'flex', justifyContent: 'center', marginBottom: '10px'}}>
        <label>
          Период:
          <select name="period" value={filters.period} onChange={handleFilterChange('period')}>
            <option value="Month">30</option>
            <option value="TwoMonth">60</option>
            <option value="Quarter">90</option>
            <option value="HalfYear">180</option>
          </select>
          дней
        </label>
      </div>
    
      <ResponsiveContainer width='100%' height='100%'>
        <PieChart>

          <Pie 
            data={data}
            dataKey="value"
            nameKey="name"
            fill='var(--main-graphic-color)'
            activeShape={{
              fill: 'var(--pie-fill-hover)',
            }}
            label
          />  

          <Tooltip
              cursor={{
                stroke: 'var(--chart-cursor)'
              }}
              contentStyle={{
                backgroundColor: 'var(--tooltip-bg)',
                borderColor: 'var(--tooltip-border)',
                boxShadow: 'var(--tooltip-shadow)'
              }}
              itemStyle={{
                color:'var(--tooltip-item-color)'
              }}
            /> 
        </PieChart>
      </ResponsiveContainer>

    </div>  
  );
}

export default ReagentsPie;