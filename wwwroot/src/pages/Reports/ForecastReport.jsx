import { useEffect, useState, useCallback } from "react";
import {
  Grid,
  TextField,
  FormControlLabel,
  Checkbox
} from "@mui/material";

import ReportTemplate from "./ReportTemplate";

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
      title="Отчет по прогнозу расхода реагентов"
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
  );
};

export default ForecastReport;