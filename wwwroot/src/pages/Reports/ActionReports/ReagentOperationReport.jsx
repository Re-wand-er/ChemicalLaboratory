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
    categoryId: "",
    reagentId: "",
    minQuantity: ""
  });

  const loadData = useCallback(async () => {
    const baseUrl = `/api/reagent-operation/${path}`;
    const urlParams = new URLSearchParams();

    if (filters.dateFrom)
      urlParams.append("DateFrom", filters.dateFrom);

    if (filters.dateTo)
      urlParams.append("DateTo", filters.dateTo);

    if (filters.categoryId)
      urlParams.append("CategoryId", filters.categoryId);

    if (filters.reagentId)
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
    <Box>
      <Typography variant="h5" mb={2}>
        {title}
      </Typography>

      <Paper sx={{ p: 2, mb: 3 }}>
        <Grid container spacing={2}>

          <Grid item xs={12} md={2}>
            <TextField
              type="date"
              name="dateFrom"
              label="С"
              InputLabelProps={{ shrink: true }}
              value={filters.dateFrom}
              onChange={handleFilterChange}
              fullWidth
            />
          </Grid>

          <Grid item xs={12} md={2}>
            <TextField
              type="date"
              name="dateTo"
              label="По"
              InputLabelProps={{ shrink: true }}
              value={filters.dateTo}
              onChange={handleFilterChange}
              fullWidth
            />
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
              {categories.map(cat => (
                <MenuItem key={cat.id} value={cat.id}>
                  {cat.name}
                </MenuItem>
              ))}
            </TextField>
          </Grid>

          <Grid item xs={12} md={3}>
            <TextField
              select
              name="reagentId"
              label="Реагент"
              value={filters.reagentId}
              onChange={handleFilterChange}
              fullWidth
            >
              <MenuItem value="">Все реагенты</MenuItem>
              {reagents.map(r => (
                <MenuItem key={r.id} value={r.id}>
                  {r.name}
                </MenuItem>
              ))}
            </TextField>
          </Grid>

          <Grid item xs={12} md={2}>
            <TextField
              type="number"
              name="minQuantity"
              label="Мин. объем"
              value={filters.minQuantity}
              onChange={handleFilterChange}
              fullWidth
            />
          </Grid>

          <Grid item xs={12}>
            <ExportFormat
              title={title}
              columns={columns}
              rows={rows}
            />
          </Grid>

        </Grid>
      </Paper>

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

export default ReagentOperationReport;