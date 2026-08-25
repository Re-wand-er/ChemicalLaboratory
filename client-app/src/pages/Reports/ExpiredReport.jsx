import { useEffect, useState, useCallback } from "react";
import {
  Box,
  Grid,
  TextField,
  MenuItem,
  Paper,
  Typography,
  Checkbox,
  FormControlLabel
} from "@mui/material";

import ReportTemplate from "./ReportTemplate";

import { autoTable } from 'jspdf-autotable'; 

import { formatDate } from "../../utils/formatDate";
import { fetchGetData } from "../../api/fetch";

const columns = [
  //{ field: "id", headerName: "ID", width: 70 },
  { field: "name", headerName: "Реагент", flex: 1 },
  { field: "category", headerName: "Категория", flex: 1 },
  {
    field: "expirationDate",
    headerName: "Срок годности",
    width: 140,
    valueFormatter: (params) => formatDate(params, 'date')  
  },
  {
    field: "currentQuantity",
    headerName: "Остаток",
    width: 120
  },
  { 
    field: 'daysRemaining', 
    headerName: 'Дней осталось', 
    width: 150,
    renderCell: (params) => {
      const days = params.value;
      let color = 'inherit'; 
      
      if (days <= 30) color = 'var(--mui-palette-error-main)'; // Red
      else if (days <= 60) color = 'var(--mui-palette-warning-main)'; // Yellow 
      return (
        <span style={{ color, fontWeight: Number(days) <= 60 ? 'bold' : 'normal' }}>
          {days < 0 ? `Просрочен (${Math.abs(days)})` : days}
        </span>
      );
    }
  }
];

const ExpiredReport = () => {
  const [rows, setRows] = useState([]);
  const [categories, setCategories] = useState([]);

  const [filters, setFilters] = useState({
    daysAhead: 90,
    status: 0,
    categoryId: 0,
    onlyWithStock: true
  });

  const loadData = useCallback(async () => {
    const baseUrl = "/api/reagent/expiring";
    const urlParams = new URLSearchParams();

    urlParams.append("DaysAhead", filters.daysAhead);
    urlParams.append("Status", filters.status);

    if (filters.categoryId !== 0)
      urlParams.append("CategoryId", filters.categoryId);

    urlParams.append("OnlyWithStock", filters.onlyWithStock);

    const fullUrl = `${baseUrl}?${urlParams.toString()}`;

    try {
      await fetchGetData(fullUrl, setRows);
    } catch (error) {
      console.error("Ошибка загрузки отчета:", error);
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
      title="Отчет по срокам годности реагентов"
    >
      <Grid>
        <TextField
          type="number"
          name="daysAhead"
          label="Горизонт"
          value={filters.daysAhead}
          onChange={handleFilterChange}
          size="small"
          fullWidth
          sx={{minWidth: '20px', maxWidth: '90px'}}

        />
      </Grid>

      <Grid>
        <TextField
          select
          name="status"
          label="Статус"
          value={filters.status}
          onChange={handleFilterChange}
          size="small"
          fullWidth
          sx={{minWidth: '150px'}}
        >
          <MenuItem value={0}>Все</MenuItem>
          <MenuItem value={1}>Просроченные</MenuItem>
          <MenuItem value={2}>Истекающие</MenuItem>
        </TextField>
      </Grid>

      <Grid>
        <TextField
          select
          name="categoryId"
          label="Категория"
          value={filters.categoryId}
          onChange={handleFilterChange}
          size="small"
          fullWidth
          sx={{minWidth: '150px'}}
        >
          <MenuItem value={0}>Все категории</MenuItem>
          {categories.map((cat) => (
            <MenuItem key={cat.id} value={cat.id}>
              {cat.name}
            </MenuItem>
          ))}
        </TextField>
      </Grid>

      <Grid>
        <FormControlLabel
          control={
            <Checkbox
              name="onlyWithStock"
              checked={filters.onlyWithStock}
              onChange={handleFilterChange}
            />
          }
          label="Остаток"
          sx={{minWidth: '10px'}}
        />
      </Grid>
    </ReportTemplate>
  );
};

export default ExpiredReport;