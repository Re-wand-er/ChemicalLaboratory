import { useEffect, useState, useCallback } from "react";
import {
  Box,
  Grid,
  TextField,
  MenuItem,
  Paper,
  Typography,
  FormControlLabel,
  Checkbox
} from "@mui/material";
import { DataGrid } from "@mui/x-data-grid";

import ExportFormat from "./ExportFormat";

import { fetchGetData } from "../../api/fetch";

const columns = [
  { field: "name", headerName: "Реагент", flex: 1 },

  {
    field: "currentQuantity",
    headerName: "Остаток",
    width: 120,
    renderCell: (p) => <b>{p.value}</b>
  },

  { field: "avgConsumption", headerName: "Средн. расход", width: 150 },
  { field: "daysToZero", headerName: "Дней до 0", width: 120 },
  { field: "daysToExpiry", headerName: "До истечения", width: 140 },

  {
    field: "recommendedOrder",
    headerName: "Рекомендуемый заказ",
    width: 180,
    renderCell: (p) => (
      <span style={{ color: p.value > 0 ? "red" : "green", fontWeight: 600 }}>
        {p.value}
      </span>
    )
  }
];

const ForecastReport = () => {
  const [rows, setRows] = useState([]);

  const [filters, setFilters] = useState({
    forecastDays: 90,
    multiplier: 3,
    maxDaysToZero: 999,
    criticalOnly: false,
    onlyReorderNeeded: false
  });

  const loadData = useCallback(async () => {
    const params = new URLSearchParams();

    params.append("ForecastDays", filters.forecastDays);
    params.append("Multiplier", filters.multiplier);
    params.append("MaxDaysToZero", filters.maxDaysToZero);
    params.append("CriticalOnly", filters.criticalOnly);
    params.append("OnlyReorderNeeded", filters.onlyReorderNeeded);

    const url = `/api/reagent/forecast?${params.toString()}`;

    await fetchGetData(url, setRows);
  }, [filters]);

  useEffect(() => {
    loadData();
  }, [loadData]);

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;

    setFilters((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value
    }));
  };

  return (
    <Box>
      <Typography variant="h5" mb={2}>
        Прогноз расхода реагентов
      </Typography>

      {/* ФИЛЬТРЫ */}
      <Paper sx={{ p: 2, mb: 2 }}>
        <Grid container spacing={2}>

          <Grid item xs={12} md={2}>
            <TextField
              type="number"
              name="forecastDays"
              label="Прогноз (дней)"
              value={filters.forecastDays}
              onChange={handleChange}
              fullWidth
            />
          </Grid>

          <Grid item xs={12} md={2}>
            <TextField
              type="number"
              name="multiplier"
              label="Коэф. запаса"
              value={filters.multiplier}
              onChange={handleChange}
              fullWidth
            />
          </Grid>

          <Grid item xs={12} md={2}>
            <TextField
              type="number"
              name="maxDaysToZero"
              label="Макс. дней до 0"
              value={filters.maxDaysToZero}
              onChange={handleChange}
              fullWidth
            />
          </Grid>

          <Grid item xs={12} md={3}>
            <FormControlLabel
              control={
                <Checkbox
                  name="criticalOnly"
                  checked={filters.criticalOnly}
                  onChange={handleChange}
                />
              }
              label="Только критические"
            />
          </Grid>

          <Grid item xs={12} md={3}>
            <FormControlLabel
              control={
                <Checkbox
                  name="onlyReorderNeeded"
                  checked={filters.onlyReorderNeeded}
                  onChange={handleChange}
                />
              }
              label="Только требующие заказа"
            />
          </Grid>

          <Grid item xs={12} md={2}>
            <ExportFormat
              title="Отчет по прогнозу заказов"
              columns={columns}
              rows={rows}
            />
          </Grid>

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
          initialState={{
          sorting: {
            sortModel: [{ field: 'recommendedOrder', sort: 'desc' }],
          },
        }}
        />
      </Paper>
    </Box>
  );
};

export default ForecastReport;