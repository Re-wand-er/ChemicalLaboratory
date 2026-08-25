import { useEffect, useState } from "react";
import {
  Box,
  Grid,
  Paper,
  Typography,
  Button,
  Divider,
  CircularProgress
} from "@mui/material";
import { Download as DownloadIcon, PlayArrow as GenerateIcon } from '@mui/icons-material';

import ReportDataTable from "../../components/DataTable/ReportDataTable";
import robotoFontUrl from "../../assets/Roboto.ttf";

import ReportPage from "./ReportPage.jsx";
import jsPDF from "jspdf";

const ReportTemplate = ({ title, exportTitle, children, rows, columns, pdfGenerator, filters }) => {
  const [reportCreate, setReportCreate] = useState(false);
  const [loading, setLoading] = useState(false);
  const [pdfBlobUrl, setPdfBlobUrl] = useState(null);
  const [pdfInstance, setPdfInstance] = useState(null);
  
  const generatePdfReport = async (data) => {
    setLoading(true);
    try {
      const doc = new jsPDF({ orientation: "portrait", unit: "mm", format: "a4" });
  
      // Указываем относительный путь к вашей папке assets от корня wwwroot
      //const fontUrl = "../../assets/Roboto.ttf"; 
      const response = await fetch(robotoFontUrl);
      
      if (!response.ok) {
        throw new Error(`Файл шрифта не найден по пути: ${robotoFontUrl}. Проверьте вкладку Network в F12.`);
      }
      
      const blobFont = await response.blob();
      
      // Переводим файл в base64 для jsPDF
      const buffer = await blobFont.arrayBuffer();
      const arr = new Uint8Array(buffer);
      const binary = Array.from(arr).map(b => String.fromCharCode(b)).join('');
      const base64Font = btoa(binary);
      
      // Регистрируем шрифт под точным именем "Robot"
      doc.addFileToVFS("Roboto.ttf", base64Font);
      doc.addFont("Roboto.ttf", "Roboto", "normal");
      doc.setFont("Roboto"); // Активируем его
  
      const activeFilters = filters || {
        dateFrom: "",
        dateTo: "",
        categoryId: 0,
        reagentId: 0,
        minQuantity: ""
      };
  
      // Вызываем наполнение отчета
      pdfGenerator(doc, title, columns, data, activeFilters);
  
      // Создаем ссылку для превью
      const blob = doc.output('blob');
      const blobUrl = URL.createObjectURL(blob);
  
      setPdfBlobUrl(blobUrl);
      setPdfInstance(doc);
  
    } catch (error) {
      console.error("Ошибка генерации PDF:", error);
      alert(`Ошибка: ${error.message}`);
    } finally {
      setLoading(false);
    }
  };
  
  

  const handleGeneratePdfReport = async (e) => {
    e.preventDefault();
    setReportCreate(true);
    setLoading(true);
    setPdfBlobUrl(null); 
  
    try {
      if (rows && rows.length > 0) {
        await generatePdfReport(rows);
      } else {
        throw "Ошибка генерации";
      }
    } catch (error) {
      console.error("Ошибка при получении данных:", error);
    } finally {
      setLoading(false);
    }
  };

  const handleDownload = () => {
    if (pdfInstance) {
      pdfInstance.save(`${title || 'отчет'}.pdf`);
    }
  };

  if (!rows || !columns) return <Typography>Загрузка данных...</Typography>;

  return (
    <Box>
      <Typography variant="h5" mb={2}>{title}</Typography>
      
      <Paper sx={{ p: 1, mb: 2}}>
        <Grid container spacing={1} alignItems="center" justifyContent='space-between'>
          <Grid size={{ md: 'grow' }}> 
            <Box 
              sx={{ 
                display: 'flex', 
                gap: 1, 
                flexWrap: 'wrap', 
                alignItems: 'center' 
              }}
            >
              {children}
            </Box>
          </Grid>
          
          <Grid size={{ md:'auto' }}>
            <Box sx={{ display: 'flex', alignItems: 'center' }}>
              <Divider 
                orientation="vertical" 
                flexItem 
                sx={{ marginRight: 1.5, height: 32, alignSelf: 'center', borderColor: 'var(--mui-palette-background-border)' }} 
              />

              <Button 
                size="small"
                variant="contained" 
                onClick={handleGeneratePdfReport}
                startIcon={<GenerateIcon />}
                disabled={loading}
              >
                {loading ? <CircularProgress size={24} color="inherit" /> : 'Сформировать'}
              </Button>
            </Box>
          </Grid>
        </Grid>
      </Paper>

      <Box sx={{ mt: 2 }}>
        {rows.length > 0 ? (
          <ReportDataTable rows={rows} columns={columns} />
        ) : (
          <Typography sx={{ p: 3, textAlign: 'center' }}>Нет данных для отображения</Typography>
        )}

        {reportCreate && (
          <ReportPage 
            title={title}
            pdfBlob={pdfBlobUrl}
            docInstance={pdfInstance}
            handleDownload={handleDownload}
          />
        )}
      </Box>
    </Box>
  );
}

export default ReportTemplate;