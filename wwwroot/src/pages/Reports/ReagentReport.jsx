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

import ExportFormat from "./ExportFormat";

import { fetchGetData } from "../../api/fetch";

const columns = [
  { field: "id", headerName: "ID", width: 70 },
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
    categoryId: ""
  });

  // загрузка данных
  const loadData = useCallback(async () => {
    const baseUrl = "/api/reagent/report";

    const params = new URLSearchParams();

    if (filters.categoryId) {
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

  return (
    <Box>
      <Typography variant="h5" mb={2}>
        Отчет по реагентам
      </Typography>

      {/* ФИЛЬТРЫ */}
      <Paper sx={{ p: 2, mb: 2 }}>
        <Grid container spacing={2}>

          <Grid item xs={12} md={4}>
            <TextField
              select
              name="categoryId"
              label="Категория"
              value={filters.categoryId}
              onChange={handleChange}
              fullWidth
            >
              <MenuItem value="">Все категории</MenuItem>
              {categories.map((c) => (
                <MenuItem key={c.id} value={c.id}>
                  {c.name}
                </MenuItem>
              ))}
            </TextField>
          </Grid>

          <ExportFormat
            title="Остатки реагентов"
            columns={columns}
            rows={rows}
          />
        
        </Grid>
      </Paper>

      {/* ТАБЛИЦА */}
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

export default ReagentReport;