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
import { DataGrid } from "@mui/x-data-grid";

import ExportFormat from "./ExportFormat";

import { formatDate } from "../../utils/formatDate";
import { fetchGetData } from "../../api/fetch";

import '../Analytics/Statistics/statisticStyle.css';

const columns = [
  { field: "id", headerName: "ID", width: 70 },

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
      
      if (days <= 30) color = 'var(--main-error)'; // Red
      else if (days <= 60) color = 'var(--main-warning)'; // Yellow 
      return (
        <span style={{ color, fontWeight: Number(days) <= 60 ? 'var(--main-font-bold)' : 'var(--main-font-normal)' }}>
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
    categoryId: "",
    onlyWithStock: true
  });

  const loadData = useCallback(async () => {
    const baseUrl = "/api/reagent/expiring";
    const urlParams = new URLSearchParams();

    urlParams.append("DaysAhead", filters.daysAhead);
    urlParams.append("Status", filters.status);

    if (filters.categoryId)
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

  return (
    <Box>
      <Typography variant="h5" mb={2}>
        Отчет по срокам годности реагентов
      </Typography>

      {/* Блок фильтров */}
      <Paper sx={{ p: 2, mb: 3 }}>
        <Grid container spacing={2} alignItems="center">

          <Grid item xs={12} md={2}>
            <TextField
              type="number"
              name="daysAhead"
              label="Горизонт (дней)"
              value={filters.daysAhead}
              onChange={handleFilterChange}
              fullWidth
            />
          </Grid>

          <Grid item xs={12} md={3}>
            <TextField
              select
              name="status"
              label="Статус"
              value={filters.status}
              onChange={handleFilterChange}
              fullWidth
            >
              <MenuItem value={0}>Все</MenuItem>
              <MenuItem value={1}>Только просроченные</MenuItem>
              <MenuItem value={2}>Истекающие</MenuItem>
            </TextField>
          </Grid>

          <Grid item xs={12} md={3}>
            <TextField
              select
              name="categoryId"
              label="Категория"
              value={filters.categoryId}
              onChange={handleFilterChange}
              fullWidth
            >
              <MenuItem value="">Все категории</MenuItem>
              {categories.map((cat) => (
                <MenuItem key={cat.id} value={cat.id}>
                  {cat.name}
                </MenuItem>
              ))}
            </TextField>
          </Grid>

          <Grid item xs={12} md={2}>
            <FormControlLabel
              control={
                <Checkbox
                  name="onlyWithStock"
                  checked={filters.onlyWithStock}
                  onChange={handleFilterChange}
                />
              }
              label="Только с остатком"
            />
          </Grid>

          <Grid item xs={12} md={2}>
            <ExportFormat
              title="Отчет по срокам годности реагентов"
              columns={columns}
              rows={rows}
            />
          </Grid>

        </Grid>
      </Paper>

      {/* Таблица */}
      <Paper sx={{ height: 500 }}>
        <DataGrid
          rows={rows}
          columns={columns}
          pageSize={10}
          rowsPerPageOptions={[10]}
          disableRowSelectionOnClick
          density="compact"
        />
      </Paper>
    </Box>
  );
};

export default ExpiredReport;