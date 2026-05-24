import React, { useState } from 'react';
import { 
  Box, 
  Card, 
  CardContent, 
  Typography, 
  Stack, 
  CircularProgress
} from '@mui/material';

import ExportFormat from "./ExportFormat.jsx";

const ReportPage = ({ rows, columns, title, pdfBlob, handleDownload }) => {
  const [loading, setLoading] = useState(false);
  const [pdfBlobUrl, setPdfBlobUrl] = useState(pdfBlob);

  return (
    <Box >
      <Typography variant="h4" gutterBottom fontWeight="bold">
        Генерация отчетов
      </Typography>

      <Card sx={{ p: 2, border: '1px solid #e0e0e0' }}>
        <CardContent>
          <Stack direction="row" justifyContent="space-between" alignItems="center" sx={{ mb: 2 }}>
            <Typography variant="h6">Предпросмотр отчета</Typography>
            <ExportFormat title={'Отчет'} columns={columns} rows={rows} generatePdfReport={handleDownload}/>
          </Stack>
          

          {pdfBlob ? (
          <Box 
            component="iframe" 
            src={pdfBlob} 
            title="PDF Preview"
            sx={{
              width: '100%',
              height: '600px',
              border: 'none',
              borderRadius: 1,
              backgroundColor: '#f5f5f5'
            }}
          />
          ) : (
            <Box sx={{ display: 'flex', justifyContent: 'center', p: 5 }}>
              <CircularProgress />
              <Typography sx={{ ml: 2, alignSelf: 'center' }}>Создание превью...</Typography>
            </Box>
          )}
        </CardContent>
      </Card>
    </Box>
  );
}

export default ReportPage;