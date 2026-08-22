import { useEffect, useState, useCallback } from "react";
import {
  TextField,
  MenuItem,
} from "@mui/material";

import ReportTemplate from "./ReportTemplate";

import { fetchGetData } from "../../api/fetch";

const columns = [
  //{ field: "id", headerName: "ID", width: 70 },
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
    categoryId: 0,
  });

  // загрузка данных
  const loadData = useCallback(async () => {
    const baseUrl = "/api/reagent/report";

    const params = new URLSearchParams();

    if (filters.categoryId !== 0) {
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
    <ReportTemplate
      rows={rows}
      columns={columns}
      title="Отчет по остаткам реагентов"
      exportTitle="Остатки реагентов"
    >
      <TextField
        select
        name="categoryId"
        label="Категория"
        value={filters.categoryId || 0}
        onChange={handleChange}
        size="small"
        fullWidth
      >
        
        <MenuItem value={0}>Все категории</MenuItem>
        {categories.map((c) => (
          <MenuItem key={c.id} value={c.id}>
            {c.name}
          </MenuItem>
        ))}
      </TextField>
    </ReportTemplate>
  );
};

export default ReagentReport;