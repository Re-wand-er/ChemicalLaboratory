import { useEffect, useState, useCallback } from "react";
import {
  Grid,
  TextField,
  MenuItem,
} from "@mui/material";

import { formatDate } from "../../../utils/formatDate";
import { fetchGetData } from "../../../api/fetch";
import ReportTemplate from "../ReportTemplate";

import { jsPDF } from 'jspdf';
import autoTable from 'jspdf-autotable'; 
import { blue } from "@mui/material/colors";

const columns = [
  {
    field: "operationDate",
    headerName: "Дата",
    width: 110,
    valueFormatter: (params) => formatDate(params, 'date')
  },
  { field: "reagentName", headerName: "Реагент", flex: 1 },
  { field: "category", headerName: "Категория", flex: 1 },
  {
    field: "quantity",
    headerName: "Количество",
    width: 100,
    renderCell: (params) => (
      <span style={{ fontWeight: 600 }}>
        {params.value}
      </span>
    )
  },

  { field: "unit", headerName: "Ед. изм.", width: 80 },
  { field: "userName", headerName: "Пользователь", width: 160 },
  { field: "comment", headerName: "Комментарий", flex: 1 }
];

const ReagentOperationReport = ({ title, path }) => {
  const [rows, setRows] = useState([]);
  const [categories, setCategories] = useState([]);
  const [reagents, setReagents] = useState([]);

  const [filters, setFilters] = useState({
    dateFrom: "",
    dateTo: "",
    categoryId: 0,
    reagentId: 0,
    minQuantity: ""
  });

  const loadData = useCallback(async () => {
    const baseUrl = `/api/reagent-operation/${path}`;
    const urlParams = new URLSearchParams();

    if (filters.dateFrom)
      urlParams.append("DateFrom", filters.dateFrom);

    if (filters.dateTo)
      urlParams.append("DateTo", filters.dateTo);

    if (filters.categoryId !== 0)
      urlParams.append("CategoryId", filters.categoryId);

    if (filters.reagentId !== 0)
      urlParams.append("ReagentId", filters.reagentId);

    if (filters.minQuantity)
      urlParams.append("MinQuantity", filters.minQuantity);

    const fullUrl = `${baseUrl}?${urlParams.toString()}`;

    try {
      await fetchGetData(fullUrl, setRows);
    } catch (error) {
      console.error("Ошибка загрузки отчета:", error);
    }
  }, [filters]);

  useEffect(() => {
    fetchGetData("/api/category/name", setCategories);
    fetchGetData("/api/reagent/name", setReagents);
  }, []);

  useEffect(() => {
    loadData();
  }, [loadData]);

  const handleFilterChange = (e) => {
    const { name, value } = e.target;
    setFilters(prev => ({
      ...prev,
      [name]: value
    }));
  };


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
    
    const filterText1 = `Период: с ${filters?.dateFrom || '—'} по ${filters?.dateTo || '—'}  |  Категория ID: ${filters?.categoryId || 'Все'}  |  Реагент ID: ${filters?.reagentId || 'Все'}`;
    doc.text(filterText1, 14, 35);
    
    const filterText2 = `Мин. количество: ${filters?.minQuantity || 'Не указано'}  |  Сформирован: ${new Date().toLocaleDateString()}`;
    doc.text(filterText2, 14, 40);
  
    // 3. Строго ограничиваем вывод первыми 10 строками
    const limitedData = (data || []).slice(0, 10);
  
    // 4. Построение красивой таблицы через autoTable
    autoTable(doc, {
      startY: 46,
      head: [columns.map(col => col.headerName || col.field)],
      body: limitedData.map(row => columns.map(col => row[col.field])),
      
      theme: 'striped',
      headStyles: {
        fillColor: 'lightBlue', // Красивый синий цвет шапки таблицы
        textColor:'black',
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
  

  return (
    <ReportTemplate
      rows={rows}
      columns={columns}
      title={title}
      pdfGenerator={buildPdfContent}
      filters={filters}
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
          name="categoryId"
          label="Категория"
          value={filters.categoryId}
          onChange={handleFilterChange}
          size="small"
          fullWidth
          sx={{minWidth: '100px', maxWidth: '160px'}}
        >
          <MenuItem value={0}>Все категории</MenuItem>
          {categories.map(cat => (
            <MenuItem key={cat.id} value={cat.id}>
              {cat.name}
            </MenuItem>
          ))}
        </TextField>
      </Grid>
        
      <Grid>
        <TextField
          select
          name="reagentId"
          label="Реагент"
          value={filters.reagentId}
          onChange={handleFilterChange}
          size="small"
          fullWidth
          sx={{maxWidth: '150px'}}
        >
          <MenuItem value={0}>Все реагенты</MenuItem>
          {reagents.map(r => (
            <MenuItem key={r.id} value={r.id}>
              {r.name}
            </MenuItem>
          ))}
        </TextField>
      </Grid>
        
      <Grid>
        <TextField
          type="number"
          name="minQuantity"
          label="Кол-во"
          value={filters.minQuantity}
          onChange={handleFilterChange}
          size="small"
          fullWidth
          sx={{maxWidth: '80px'}}
        />
      </Grid>
    </ReportTemplate>
  );
};

export default ReagentOperationReport;