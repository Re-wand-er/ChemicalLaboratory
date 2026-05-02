import { useEffect, useState, useCallback, Fragment } from "react";
import {
  Box, Grid, TextField, MenuItem, Button, Typography,
  Paper, Input, FormControlLabel, Checkbox
} from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";

import ReagentsBelowMinimum from "../Analytics/Statistics/ReagentsBelowMinimum";
import ExportFormat from "./ExportFormat";

import { fetchGetData } from "../../api/fetch";

const columns = [
  { field: 'id', headerName: 'ID', width: 50 },
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
      <span style={{ color: 'var(--main-error)', fontWeight: 'var(--main-font-bold)',  }}>
        {params.value}
      </span>
    )
  },
];

const BelowMinReport = () => {
  const [rows, setRows] = useState([]);
  const [categories, setCategories] = useState([]);
  const [exportSelect, setExportSelect] = useState("pdf");
  const [filters, setFilters] = useState({
    categoryId: '',
    criticalPercent: 100,
    excludeExpired: false
  });

  const loadData = useCallback(async () => {
    const baseUrl = "/api/reagent/low-stock";
    const urlParams = new URLSearchParams();

    if (filters.categoryId) 
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

  return (
    <Box>
      <Typography variant="h5">
        Реагенты ниже минимального уровня
      </Typography>

      {/* Блок параметров */}
      <Paper sx={{ p: 2, mb: 3 }}>
        <Grid container spacing={2} >
          <Grid xs={12} md={3}>
            <TextField
              select
              name="categoryId"
              label="Категория"
              value={filters.categoryId}
              onChange={handleFilterChange}
            >
              <MenuItem value=''>Все категории</MenuItem>
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
              name="criticalPercent"
              label="Критичность (%)"
              fullWidth
              style={{width: '120px'}}
              value={filters.criticalPercent}
              onChange={handleFilterChange}
            />
          </Grid>

          <Grid item xs={12} md={3}>   
            <FormControlLabel
              control={
                <Checkbox
                  name="excludeExpired"
                  checked={filters.excludeExpired}
                  onChange={handleFilterChange}
                />
              }
              label="Исключ. просрок"
            />
          </Grid> 

          <ExportFormat 
            title="Отчет по складским остаткам" 
            columns={columns} 
            rows={rows} 
          />

        </Grid>
      </Paper>

      <Paper sx={{ height: 450 }}>
        <DataGrid
          rows={rows}
          columns={columns}
          pageSize={10}
          rowsPerPageOptions={[10]}
          disableRowSelectionOnClick
        />
      </Paper>
    </Box>
  );
};

export default BelowMinReport;