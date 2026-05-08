import { Box } from '@mui/material';

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
      <h2>Прогноз</h2>
      {/* <label>Реагент необходимо заказать если остаток меньше мин. колич * 3</label> */}
      <ReagentForecastTable />
    </Box>
  );
};
export default Predicts;