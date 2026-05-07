import { useEffect, useState, useCallback } from "react";
import {
  Box,
  Grid,
  TextField,
  MenuItem,
  Paper,
  Typography
} from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";
import ExportFormat from "../ExportFormat";

import { formatDate } from "../../../utils/formatDate";
import { fetchGetData } from "../../../api/fetch";
import ReportTemplate from "../ReportTemplate";

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

  return (
    <ReportTemplate
      rows={rows}
      columns={columns}
      title={title}
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