import { useState } from "react";
import {
  Box, Grid, TextField, MenuItem, Button, Typography,
  Paper, Input, FormControlLabel, Checkbox
} from "@mui/material";

import { exportCsvFile, exportJsonFile } from '../../utils/dataExportFotmat'; 
import { generateExcel } from "../../utils/generateExcel";
import { generatePDF } from "../../utils/generatePDF";

const ExportFormat = ({ title, columns, rows }) => {
  const [exportSelect, setExportSelect] = useState("pdf");

   const handleExportFile = () => {
      console.log(exportSelect);

      switch(exportSelect){
        case 'excel': generateExcel(title, columns, rows); break;
        case 'pdf': generatePDF(title, columns, rows); break;
        case 'csv': exportCsvFile(title, rows); break;
        case 'json': exportJsonFile(title, rows); break;
        default: console.error("Неизвестный формат");
      }
    }

  return (
    <Grid display='flex' alignItems='center' gap={2} > {/*xs={12} md={3} */}
      <Typography variant="body1">
        Сгенерировать в
      </Typography>
      <TextField
        select
        value={exportSelect}
        label="Формат"
        onChange={(e) => setExportSelect(e.target.value)}
      >
        <MenuItem value="pdf">PDF</MenuItem>
        <MenuItem value="excel">EXCEL</MenuItem>
        <MenuItem value="csv">CSV</MenuItem>
        <MenuItem value="json">JSON</MenuItem>
      </TextField>
  
      <Button 
        variant="outlined"
        onClick={() => handleExportFile()}
      >
        Сформировать 
      </Button>
    </Grid>
  );
}

export default ExportFormat;