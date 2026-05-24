import { useEffect, useState, useCallback } from "react";
import {
  Grid, TextField, MenuItem, 
  FormControlLabel, Checkbox
} from "@mui/material";
import { fetchGetData } from "../../api/fetch";
import ReportTemplate from "./ReportTemplate";

const columns = [
  //{ field: 'id', headerName: 'ID', width: 50 },
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
      <span style={{ color: 'var(--mui-palette-error-main)', fontWeight: 'bold'  }}>
        {params.value}
      </span>
    )
  },
];

const BelowMinReport = () => {
  const [rows, setRows] = useState([]);
  const [categories, setCategories] = useState([]);
  const [filters, setFilters] = useState({
    categoryId: 0,
    criticalPercent: 100,
    excludeExpired: false
  });

  const loadData = useCallback(async () => {
    const baseUrl = "/api/reagent/low-stock";
    const urlParams = new URLSearchParams();

    if (filters.categoryId !== 0) 
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
    <ReportTemplate
      rows={rows}
      columns={columns}
      title='Отчет по реагентам ниже минимального уровня'
      exportTitle='Отчет реагентов ниже нуля'
    >
      <Grid size={4}>
        <TextField
          select
          name="categoryId"
          label="Категория"
          value={filters.categoryId}
          onChange={handleFilterChange}
          size="small"
          fullWidth
        >
          <MenuItem value={0}>Все категории</MenuItem>
          {categories.map((cat) => (
            <MenuItem key={cat.id} value={cat.id}>
              {cat.name}
            </MenuItem>
          ))}
        </TextField>
      </Grid>

      <Grid size={3}>
        <TextField
          type="number"
          name="criticalPercent"
          label="Критичность (%)"
          fullWidth
          value={filters.criticalPercent}
          onChange={handleFilterChange}
          size="small"
        />
      </Grid>

      <Grid size={4}>   
        <FormControlLabel
          control={
            <Checkbox
              name="excludeExpired"
              checked={filters.excludeExpired}
              onChange={handleFilterChange}
            />
          }
          label="Искл. просрок"
        />
      </Grid> 
    </ReportTemplate>
  );
};

export default BelowMinReport;