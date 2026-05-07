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
import { flex, maxWidth, minWidth } from "@mui/system";
import { DataGrid } from "@mui/x-data-grid";

import ReportTemplate from "./ReportTemplate";
import ExportFormat from "./ExportFormat";

import { fetchGetData } from "../../api/fetch";

const columns = [
  { field: "name", headerName: "Реагент", flex: 5 },

  {
    field: "currentQuantity",
    headerName: "Остаток",
    width: 70,
    flex: 1,
    renderCell: (p) => <b>{p.value}</b>
  },

  {
    field: "minQuantity",
    headerName: "Мин. кол-во",
    width: 70,
    flex: 1,
    renderCell: (p) => <b>{p.value}</b>
  },

  { field: "avgConsumption", headerName: "Средн. расход", minWidth: 70, flex: 1, },
  { field: "daysToZero", headerName: "Дней до 0", minWidth: 70, flex: 1, },
  { field: "daysToExpiry", headerName: "До истечения", minWidth: 70, flex: 1, },

  {
    field: "recommendedOrder",
    headerName: "Реком. заказ",
    minWidth: 100,
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
    <ReportTemplate
      rows={rows}
      columns={columns}
      title="Прогноз расхода реагентов"
      exportTitle="Отчет по прогнозу заказов"
    >
      <Grid>
        <TextField
          type="number"
          name="forecastDays"
          label="Прогноз (дней)"
          value={filters.forecastDays}
          onChange={handleChange}
          size="small"
          fullWidth
          sx={{maxWidth: '140px'}}
        />
      </Grid>

      <Grid>
        <TextField
          type="number"
          name="multiplier"
          label="Коэф. запаса"
          value={filters.multiplier}
          onChange={handleChange}
          size="small"
          fullWidth
          sx={{maxWidth: '140px'}}
        />
      </Grid>

      <Grid>
        <TextField
          type="number"
          name="maxDaysToZero"
          label="Дней до 0"
          value={filters.maxDaysToZero}
          onChange={handleChange}
          size="small"
          fullWidth
          sx={{maxWidth: '100px'}}
        />
      </Grid>

      <Grid>
        <FormControlLabel
          control={
            <Checkbox
              name="criticalOnly"
              checked={filters.criticalOnly}
              onChange={handleChange}
            />
          }
          label="Срочные"
          sx={{minWidth: '10px', maxWidth: '100px'}}
        />
      </Grid>

      <Grid>
        <FormControlLabel
          control={
            <Checkbox
              name="onlyReorderNeeded"
              checked={filters.onlyReorderNeeded}
              onChange={handleChange}
            />
          }
          label="Треб. заказ"
          sx={{minWidth: '10px'}}
        />
      </Grid>
    </ReportTemplate>
    // <Box>
    //   <Typography variant="h5" mb={2}>
    //     Прогноз расхода реагентов
    //   </Typography>

    //   <Paper sx={{ p: 2, mb: 2 }}>
    //     <Grid container spacing={1} alignItems="center">

    //       <Grid size={{ xs: 6, md: 1.5 }}>
    //         <TextField
    //           type="number"
    //           name="forecastDays"
    //           label="Прогноз (дней)"
    //           value={filters.forecastDays}
    //           onChange={handleChange}
    //           //size="small"
    //           fullWidth
    //         />
    //       </Grid>

    //       <Grid size={{ xs: 6, md: 1.5 }}>
    //         <TextField
    //           type="number"
    //           name="multiplier"
    //           label="Коэф. запаса"
    //           value={filters.multiplier}
    //           onChange={handleChange}
    //           fullWidth
    //         />
    //       </Grid>

    //       <Grid size={{ xs: 6, md: 1.5 }}>
    //         <TextField
    //           type="number"
    //           name="maxDaysToZero"
    //           label="Макс. дней до 0"
    //           value={filters.maxDaysToZero}
    //           onChange={handleChange}
    //           fullWidth
    //         />
    //       </Grid>

    //       <Grid size={{ xs: 6, md: 1.5 }}>
    //         <FormControlLabel
    //           control={
    //             <Checkbox
    //               name="criticalOnly"
    //               checked={filters.criticalOnly}
    //               onChange={handleChange}
    //             />
    //           }
    //           label="Критические"
    //         />
    //       </Grid>

    //       <Grid size={{ xs: 6, md: 1.5 }}>
    //         <FormControlLabel
    //           control={
    //             <Checkbox
    //               name="onlyReorderNeeded"
    //               checked={filters.onlyReorderNeeded}
    //               onChange={handleChange}
    //             />
    //           }
    //           label="Треб. заказ"
    //         />
    //       </Grid>

    //       <Grid size={{ md: true }} /> {/*Забирает свободное простр-во*/}

    //       <Grid size={{ xs: 12, md: 'auto' }} >
    //         <ExportFormat
    //           title="Отчет по прогнозу заказов"
    //           columns={columns}
    //           rows={rows}
    //         />
    //       </Grid>

    //     </Grid>
    //   </Paper>

    //   {/* ТАБЛИЦА */}
    //   <Paper sx={{ height: 500 }}>
    //     <DataGrid
    //       rows={rows}
    //       columns={columns}
    //       pageSize={10}
    //       rowsPerPageOptions={[10]}
    //       disableRowSelectionOnClick
    //       density="compact"
    //       initialState={{
    //       sorting: {
    //         sortModel: [{ field: 'recommendedOrder', sort: 'desc' }],
    //       },
    //     }}
    //     />
    //   </Paper>
    // </Box>
  );
};

export default ForecastReport;