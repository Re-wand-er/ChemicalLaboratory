import { useEffect, useState, useCallback } from "react";
import {
  Grid, TextField, MenuItem, 
  FormControlLabel, Checkbox
} from "@mui/material";
import { fetchGetData } from "../../api/fetch";
import ReportTemplate from "./ReportTemplate";

import { autoTable } from 'jspdf-autotable'; 

const columns = [
  //{ field: 'id', headerName: 'ID', width: 50 },
  { field: 'name', headerName: 'Реагент', flex: 1 },
  { 
    field: 'currentQuantity', 
    headerName: 'Остаток', 
    width: 90,
    renderCell: (params) => (
      <span style={{ color: 'var(--main-error)', fontWeight: 'var(--main-font-bold)',  }}>
        {params.value}
      </span>
    )
  },
  { 
    field: 'minQuantity', 
    headerName: 'Мин.', 
    width: 70
  },
  { 
    field: 'unit', 
    headerName: 'Ед. изм.', 
    width: 80
  },
  { 
    field: 'criticalPercent', 
    headerName: 'Процент остатка', 
    width: 90,
    renderCell: (params) => (
      <span style={{ color: 'var(--mui-palette-error-main)', fontWeight: 'bold'  }}>
        {params.value}
      </span>
    )
  },
];

const BelowMinReport = () => {
  const [rows, setRows] = useState([]);
  const [categories, setCategories] = useState([]);
  const [filters, setFilters] = useState({
    categoryId: 0,
    criticalPercent: 100,
    excludeExpired: false
  });

  const loadData = useCallback(async () => {
    const baseUrl = "/api/reagent/low-stock";
    const urlParams = new URLSearchParams();

    if (filters.categoryId !== 0) 
      urlParams.append("CategoryId", filters.categoryId);

    urlParams.append("CriticalPercent", filters.criticalPercent);
    urlParams.append("ExcludeExpired", filters.excludeExpired);

    const fullUrl = `${baseUrl}?${urlParams.toString()}`;

    try {
      await fetchGetData(fullUrl, setRows);
    } catch (error) {
      console.error("Ошибка при получении отчета:", error);
    }
  }, [filters]);


  useEffect(() => {
    fetchGetData("/api/category/name", setCategories);
  }, []);

  useEffect(() => {
    loadData();
  }, [loadData]);

  const handleFilterChange = (e) => {
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
      title='Отчет по реагентам ниже минимального уровня'
      exportTitle='Отчет реагентов ниже нуля'
    >
      <Grid size={4}>
        <TextField
          select
          name="categoryId"
          label="Категория"
          value={filters.categoryId}
          onChange={handleFilterChange}
          size="small"
          fullWidth
        >
          <MenuItem value={0}>Все категории</MenuItem>
          {categories.map((cat) => (
            <MenuItem key={cat.id} value={cat.id}>
              {cat.name}
            </MenuItem>
          ))}
        </TextField>
      </Grid>

      <Grid size={3}>
        <TextField
          type="number"
          name="criticalPercent"
          label="Критичность (%)"
          fullWidth
          value={filters.criticalPercent}
          onChange={handleFilterChange}
          size="small"
        />
      </Grid>

      <Grid size={4}>   
        <FormControlLabel
          control={
            <Checkbox
              name="excludeExpired"
              checked={filters.excludeExpired}
              onChange={handleFilterChange}
            />
          }
          label="Искл. просрок"
        />
      </Grid> 
    </ReportTemplate>
  );
};

export default BelowMinReport;