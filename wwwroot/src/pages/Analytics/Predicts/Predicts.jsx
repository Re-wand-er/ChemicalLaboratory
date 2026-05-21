import { Box, Typography, Stack, Button, Paper } from '@mui/material';

import ReagentForecastTable from "./ReagentForecastTable";

/**
 * Формат: таблица с расчётными полями, цветовая индикация срочности, возможно, шкала времени (Gantt) до наступления события.
 */
const Predicts = () => {
  return (
    <Box
      sx={{ 
        width: '100%', 
        maxWidth:'1400px', 
        display:'flex',
        flexDirection:'column',
        margin:'0 auto',
        justifyContent:'center',
      }}
    >
      <Box sx={{ display: 'flex', justifyContent: 'space-between', width: '100%', alignItems: 'center', mb:4 }}>
        <Typography variant="h4" sx={{ fontWeight: 'bold', color: 'text.primary' }}>
          Прогноз
        </Typography>
        
        <Paper
          elevation={0}
          sx={{
            border: 1,
            borderColor: 'divider',
            borderRadius: 1,
            bgcolor: 'background.paper',
            p: 0.5,
          }}
        >
          <Stack direction="row" spacing={1}>
            <Button
              variant="outlined"
              color="warning"
              //onClick={}
              size="small"
            >
              Экспорт в csv
            </Button>

            <Button
              variant="outlined"
              //onClick={}
              size="small"
            >
              Экспорт в json
            </Button>
          </Stack> 
        </Paper>
      </Box>

      <ReagentForecastTable />
    </Box>
  );
};
export default Predicts;