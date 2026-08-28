import { useEffect, useState, useCallback } from "react";
import {
  Grid,
  TextField,
  FormControlLabel,
  Checkbox
} from "@mui/material";

import ReportTemplate from "./ReportTemplate";

import { autoTable } from 'jspdf-autotable'; 

import { fetchGetData } from "../../api/fetch";

const columns = [
  { field: "name", headerName: "Реагент", flex: 5 },

  {
    field: "currentQuantity",
    headerName: "Остаток",
    width: 70,
    flex: 1,
    renderCell: (p) => <b>{p.value}</b>
  },

  {
    field: "minQuantity",
    headerName: "Мин. кол-во",
    width: 70,
    flex: 1,
    renderCell: (p) => <b>{p.value}</b>
  },

  { field: "avgConsumption", headerName: "Средн. расход", minWidth: 70, flex: 1, },
  { field: "daysToZero", headerName: "Дней до 0", minWidth: 70, flex: 1, },
  { field: "daysToExpiry", headerName: "До истечения", minWidth: 70, flex: 1, },

  {
    field: "recommendedOrder",
    headerName: "Реком. заказ",
    minWidth: 100,
    renderCell: (p) => (
      <span style={{ color: p.value > 0 ? "red" : "green", fontWeight: 600 }}>
        {p.value}
      </span>
    )
  }
];

const ForecastReport = () => {
  const [rows, setRows] = useState([]);

  const [filters, setFilters] = useState({
    forecastDays: 90,
    multiplier: 3,
    maxDaysToZero: 999,
    criticalOnly: false,
    onlyReorderNeeded: false
  });

  const loadData = useCallback(async () => {
    const params = new URLSearchParams();

    params.append("ForecastDays", filters.forecastDays);
    params.append("Multiplier", filters.multiplier);
    params.append("MaxDaysToZero", filters.maxDaysToZero);
    params.append("CriticalOnly", filters.criticalOnly);
    params.append("OnlyReorderNeeded", filters.onlyReorderNeeded);

    const url = `/api/reagent/forecast?${params.toString()}`;

    await fetchGetData(url, setRows);
  }, [filters]);

  useEffect(() => {
    loadData();
  }, [loadData]);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;

    setFilters((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value
    }));
  };

  ///
  const buildPdfContent = (doc, title, columns, data, filters) => {
  
    // Устанавливаем ваш шрифт Robot для всего текстового наполнения документа
    doc.setFont("Roboto", "normal");
  
    // 1. Главный заголовок (Профессиональный ERP-стиль)
    doc.setFontSize(18);
    doc.setTextColor(26, 35, 126); // Темно-синий цвет MUI Primary Dark
    doc.text(`${title || 'ОТЧЕТ СИСТЕМЫ'}`, 14, 20);
    
    // Подзаголовок системы
    doc.setFontSize(8);
    doc.setTextColor(158, 158, 158);
    doc.text('ХИМИЧЕСКАЯ ЛАБОРАТОРИЯ — СИСТЕМА УПРАВЛЕНИЯ', 14, 25);
  
    // Линия-разделитель под шапкой
    doc.setDrawColor(25, 118, 210);
    doc.setLineWidth(0.5);
    doc.line(14, 28, 196, 28);
  
    // 2. Блок примененных фильтров (на русском языке)
    doc.setFontSize(9);
    doc.setTextColor(66, 66, 66);
     
    // 3. Строго ограничиваем вывод первыми 10 строками
    const limitedData = (data || []).slice(0, 10);
  
    // 4. Построение красивой таблицы через autoTable
    autoTable(doc, {
      startY: 46,
      head: [columns.map(col => col.headerName || col.field)],
      body: limitedData.map(row => columns.map(col => row[col.field])),
      
      theme: 'striped',
      headStyles: {
        fillColor: [25, 118, 210], // Красивый синий цвет шапки таблицы
        textColor: [255, 255, 255],
        fontSize: 9,
        font: 'Roboto', // Применяем русский Robot к шапке
        fontStyle: 'normal'
      },
      styles: {
        font: 'Roboto',          // Применяем русский Robot к ячейкам
        fontStyle: 'normal',
        overflow: 'linebreak', // Автоперенос длинных строк
        fontSize: 8,           
        cellPadding: 3,
      },
      margin: { left: 14, right: 14 },
      tableWidth: 'auto', // Таблица автоматически растянется ровно по ширине А4
    });
  };
  ///

  return (
    <ReportTemplate
      rows={rows}
      columns={columns}
      pdfGenerator={buildPdfContent}
      title="Отчет по прогнозу расхода реагентов"
      exportTitle="Отчет по прогнозу заказов"
    >
      <Grid>
        <TextField
          type="number"
          name="forecastDays"
          label="Прогноз (дней)"
          value={filters.forecastDays}
          onChange={handleChange}
          size="small"
          fullWidth
          sx={{maxWidth: '140px'}}
        />
      </Grid>

      <Grid>
        <TextField
          type="number"
          name="multiplier"
          label="Коэф. запаса"
          value={filters.multiplier}
          onChange={handleChange}
          size="small"
          fullWidth
          sx={{maxWidth: '140px'}}
        />
      </Grid>

      <Grid>
        <TextField
          type="number"
          name="maxDaysToZero"
          label="Дней до 0"
          value={filters.maxDaysToZero}
          onChange={handleChange}
          size="small"
          fullWidth
          sx={{maxWidth: '100px'}}
        />
      </Grid>

      <Grid>
        <FormControlLabel
          control={
            <Checkbox
              name="criticalOnly"
              checked={filters.criticalOnly}
              onChange={handleChange}
            />
          }
          label="Срочные"
          sx={{minWidth: '10px', maxWidth: '100px'}}
        />
      </Grid>

      <Grid>
        <FormControlLabel
          control={
            <Checkbox
              name="onlyReorderNeeded"
              checked={filters.onlyReorderNeeded}
              onChange={handleChange}
            />
          }
          label="Треб. заказ"
          sx={{minWidth: '10px'}}
        />
      </Grid>
    </ReportTemplate>
  );
};

export default ForecastReport;