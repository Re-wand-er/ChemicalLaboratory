import { useEffect, useState, useCallback } from "react";
import {
  Box, Grid, TextField, MenuItem, Typography,
  Paper, Button
} from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";

import ExportFormat from "./ExportFormat";
import { fetchGetData } from "../../api/fetch";

const columns = [
  { field: 'id', headerName: 'ID', width: 70 },
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
    categoryId: '',
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

    if (filters.categoryId)
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

  return (
    <Box>
      <Typography variant="h5">
        Топ используемых реагентов
      </Typography>

      <Paper sx={{ p: 2, mb: 3 }}>
        <Grid container spacing={2} alignItems="center">

          <Grid item xs={12} md={2}>
            <TextField
              type="date"
              name="dateFrom"
              label="С"
              fullWidth
              InputLabelProps={{ shrink: true }}
              value={filters.dateFrom}
              onChange={handleFilterChange}
            />
          </Grid>

          <Grid item xs={12} md={2}>
            <TextField
              type="date"
              name="dateTo"
              label="По"
              fullWidth
              InputLabelProps={{ shrink: true }}
              value={filters.dateTo}
              onChange={handleFilterChange}
            />
          </Grid>

          <Grid item xs={12} md={2}>
            <TextField
              select
              name="top"
              label="Топ"
              fullWidth
              value={filters.top}
              onChange={handleFilterChange}
            >
              <MenuItem value={5}>Топ 5</MenuItem>
              <MenuItem value={10}>Топ 10</MenuItem>
              <MenuItem value={20}>Топ 20</MenuItem>
              <MenuItem value={50}>Топ 50</MenuItem>
            </TextField>
          </Grid>

          <Grid item xs={12} md={3}>
            <TextField
              select
              name="categoryId"
              label="Категория"
              fullWidth
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

          <Grid item xs={12} md={2}>
            <TextField
              type="number"
              name="minUsage"
              label="Мин. расход"
              fullWidth
              value={filters.minUsage}
              onChange={handleFilterChange}
            />
          </Grid>

          <ExportFormat
            title="Топ используемых реагентов"
            columns={columns}
            rows={rows}
          />

        </Grid>
      </Paper>

      <Paper sx={{ height: 500 }}>
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


export default TopUsageReport;