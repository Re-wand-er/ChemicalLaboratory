import { useEffect, useState, useCallback } from "react";
import {
  Box, Grid, TextField, MenuItem, Typography,
  Paper, Button
} from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";

import ReportTemplate from "./ReportTemplate";
import ExportFormat from "./ExportFormat";

import { autoTable } from 'jspdf-autotable'; 

import { fetchGetData } from "../../api/fetch";

const columns = [
  //{ field: 'id', headerName: 'ID', width: 70 },
  { field: 'name', headerName: 'Реагент', flex: 1 },
  { field: 'category', headerName: 'Категория', flex: 1 },
  { field: 'totalUsed', headerName: 'Израсходовано', width: 150 },
  { field: 'unit', headerName: 'Ед. изм.', width: 120 },
  { 
    field: 'usageCount', 
    headerName: 'Кол-во операций', 
    width: 160,
    
    renderCell: (params) => (
      <span style={{ fontWeight: 800 }}>
        {params.value}
      </span>
    )
   }
];

const TopUsageReport = () => {
  const [rows, setRows] = useState([]);
  const [categories, setCategories] = useState([]);
  const [filters, setFilters] = useState({
    dateFrom: '',
    dateTo: '',
    top: 5,
    categoryId: 0,
    minUsage: ''
  });

  const loadData = useCallback(async () => {
    const baseUrl = "/api/reagent-operation/top-used-report";
    const urlParams = new URLSearchParams();

    if (filters.dateFrom)
      urlParams.append("DateFrom", filters.dateFrom);

    if (filters.dateTo)
      urlParams.append("DateTo", filters.dateTo);

    urlParams.append("Top", filters.top);

    if (filters.categoryId !== 0)
      urlParams.append("CategoryId", filters.categoryId);

    if (filters.minUsage)
      urlParams.append("MinUsage", filters.minUsage);

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
    const { name, value } = e.target;

    setFilters((prev) => ({
      ...prev,
      [name]: value
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
      title="Отчет по топу используемых реагентов"
    >
      <Grid>
          <TextField
            type="date"
            name="dateFrom"
            label="С"
            InputLabelProps={{ shrink: true }}
            value={filters.dateFrom}
            onChange={handleFilterChange}
            size="small"
            fullWidth
            sx={{maxWidth: '130px'}}
          />
        </Grid>

        <Grid>
          <TextField
            type="date"
            name="dateTo"
            label="По"
            InputLabelProps={{ shrink: true }}
            value={filters.dateTo}
            onChange={handleFilterChange}
            size="small"
            fullWidth
            sx={{maxWidth: '130px'}}
          />
        </Grid>

        <Grid>
          <TextField
            select
            name="top"
            label="Топ"
            value={filters.top}
            onChange={handleFilterChange}
            size="small"
            fullWidth
            sx={{maxWidth: '80px'}}
          >
            <MenuItem value={5}>5</MenuItem>
            <MenuItem value={10}>10</MenuItem>
            <MenuItem value={20}>20</MenuItem>
            <MenuItem value={50}>50</MenuItem>
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
            sx={{minWidth: '150px'}} //maxWidth:'150px'
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
          <TextField
            type="number"
            name="minUsage"
            label="Мин. кол-во"
            value={filters.minUsage}
            onChange={handleFilterChange}
            size="small"
            fullWidth
            sx={{maxWidth: '115px'}}
          />
        </Grid>
    </ReportTemplate>
  );
};


export default TopUsageReport;