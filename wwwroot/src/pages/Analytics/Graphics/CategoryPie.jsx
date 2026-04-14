import { useState, useEffect } from 'react';
import { PieChart, Pie, Tooltip, ResponsiveContainer, Legend } from "recharts";

import { fetchGetData } from '../../../api/fetch.js';

/**
* Диаграмма состоит из 2-ух частей:
* Внут-яя - все категории (data01 = [{name:<категория>,value:<сумма по всем реагентам>}, ...])
* Внешняя - реагенты для конкретной категории (data02 = [{name:<назв. реагента>, value:<кол-во>}, ...])
* Сумма кол-ва реагента = кол-ву его категории
*/
const CategoryPie = () => {
  const [reportData, setReportData] = useState({ categories: [], reagents: [] });

  useEffect(() => {
    fetchGetData('/api/reagent/stock-distribution', setReportData);
  }, []);

  return (
    <div style={{ width: '400px', height: '400px', minWidth: '400px', minHeight: '400px' }}>
      <ResponsiveContainer>
        <PieChart>
          {/* Внутренний круг: Категории (data01) */}
          <Pie
            data={reportData.categories}
            dataKey="value"
            nameKey="name"
            cx="50%"
            cy="50%"
            outerRadius="50%"
            fill='var(--main-graphic-color)'
          />
          {/* Внешний круг: Реагенты (data02) */}
          <Pie
            data={reportData.reagents}
            dataKey="value"
            nameKey="name"
            cx="50%"
            cy="50%"
            innerRadius="60%"
            outerRadius="80%"
            fill='var(--pie-fill-sub)'
            label
          />
          <Tooltip />
          {/* <Legend /> */}
        </PieChart>
      </ResponsiveContainer>
    </div>
  );
}

export default CategoryPie;