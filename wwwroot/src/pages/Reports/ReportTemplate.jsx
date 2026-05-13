import {
  Box,
  Grid,
  Paper,
  Typography
} from "@mui/material";

import ReportDataTable from "../../components/DataTable/ReportDataTable";
import ExportFormat from "./ExportFormat.jsx";

const ReportTemplate = ({ title, exportTitle, children, rows, columns }) => {
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
            {columns.length > 0 && (
              <ExportFormat title={exportTitle || title || 'Отчет'} columns={columns} rows={rows} />
            )}
          </Grid>
        </Grid>
      </Paper>

      <Box sx={{ mt: 2 }}>
        {rows.length > 0 ? (
          <ReportDataTable rows={rows} columns={columns} />
        ) : (
          <Typography sx={{ p: 3, textAlign: 'center' }}>Нет данных для отображения</Typography>
        )}
      </Box>
    </Box>
  );
}

export default ReportTemplate;