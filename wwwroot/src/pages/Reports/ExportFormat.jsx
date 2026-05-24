import { useState } from "react";
import { Box, MenuItem, Button, ButtonGroup, Select, Divider } from "@mui/material";
import FileDownloadIcon from '@mui/icons-material/FileDownload';

import { exportCsvFile, exportJsonFile } from '../../utils/dataExportFotmat';
import { generateExcel } from "../../utils/generateExcel";
import { generatePDF } from "../../utils/generatePDF";

const ExportFormat = ({ title, columns, rows, generatePdfReport, divider = true }) => {
  const [exportSelect, setExportSelect] = useState("pdf");

  const handleExportFile = () => {
    switch (exportSelect) {
      case 'excel':
        generateExcel(title, columns, rows);
        break;
      case 'pdf':
        if(typeof generatePdfReport === 'function')
          generatePdfReport(title, columns, rows);
        else
          generatePDF(title, columns, rows);
        break;
      case 'csv':
        exportCsvFile(title, rows);
        break;
      case 'json':
        exportJsonFile(title, rows);
        break;
      default:
        console.error("Неизвестный формат");
    }
  };

  return (
    <Box sx={{ display: 'flex', alignItems: 'center' }}>
      {divider &&
        <Divider 
          orientation="vertical" 
          flexItem 
          sx={{ marginRight: 1.5, height: 32, alignSelf: 'center', borderColor: 'var(--mui-palette-background-border)' }} 
        />
      } 

      <ButtonGroup variant="outlined" size="small">

        <Select
          value={exportSelect}
          onChange={(e) => setExportSelect(e.target.value)}
          sx={{
            width: 100,
            height: 34, 
            borderTopRightRadius: 0,
            borderBottomRightRadius: 0,
            backgroundColor: 'background.paper',
            '& .MuiOutlinedInput-notchedOutline': {
              borderRight: 'none', 
            },
          }}
        >
          <MenuItem value="pdf">PDF</MenuItem>
          <MenuItem value="excel">EXCEL</MenuItem>
          <MenuItem value="csv">CSV</MenuItem>
          <MenuItem value="json">JSON</MenuItem>
        </Select>

        <Button
          onClick={handleExportFile}
          variant="contained"
          startIcon={<FileDownloadIcon />}
          sx={{
            height: 34,
            px: 2,
            textTransform: 'none', 
            boxShadow: 'none',   
          }}
        >
          Скачать
        </Button>
      </ButtonGroup>
    </Box>
  );
};

export default ExportFormat;
