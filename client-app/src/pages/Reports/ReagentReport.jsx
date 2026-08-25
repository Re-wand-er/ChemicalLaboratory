import { useEffect, useState, useCallback } from "react";
import {
  TextField,
  MenuItem,
} from "@mui/material";

import ReportTemplate from "./ReportTemplate";

import { autoTable } from 'jspdf-autotable'; 

import { fetchGetData } from "../../api/fetch";

const columns = [
  //{ field: "id", headerName: "ID", width: 70 },
  { field: "name", headerName: "Реагент", flex: 1 },
  { field: "category", headerName: "Категория", flex: 1 },

  {
    field: "currentQuantity",
    headerName: "Остаток",
    width: 120,
    renderCell: (params) => (
      <b>{params.value}</b>
    )
  },

  {
    field: "minQuantity",
    headerName: "Мин.",
    width: 100
  },

  { field: "unit", headerName: "Ед. изм.", width: 100 }
];

const ReagentReport = () => {
  const [rows, setRows] = useState([]);
  const [categories, setCategories] = useState([]);

  const [filters, setFilters] = useState({
    categoryId: 0,
  });

  // загрузка данных
  const loadData = useCallback(async () => {
    const baseUrl = "/api/reagent/report";

    const params = new URLSearchParams();

    if (filters.categoryId !== 0) {
      params.append("categoryId", filters.categoryId);
    }

    const url = `${baseUrl}?${params.toString()}`;

    try {
      await fetchGetData(url, setRows);
    } catch (e) {
      console.error("Ошибка загрузки отчета", e);
    }
  }, [filters]);

  // категории
  useEffect(() => {
    fetchGetData("/api/category/name", setCategories);
  }, []);

  // данные
  useEffect(() => {
    loadData();
  }, [loadData]);

  const handleChange = (e) => {
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
      title="Отчет по остаткам реагентов"
      exportTitle="Остатки реагентов"
    >
      <TextField
        select
        name="categoryId"
        label="Категория"
        value={filters.categoryId || 0}
        onChange={handleChange}
        size="small"
        fullWidth
      >
        
        <MenuItem value={0}>Все категории</MenuItem>
        {categories.map((c) => (
          <MenuItem key={c.id} value={c.id}>
            {c.name}
          </MenuItem>
        ))}
      </TextField>
    </ReportTemplate>
  );
};

export default ReagentReport;