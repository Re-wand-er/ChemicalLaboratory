// import AverageOperationSize from "./AverageOperationSize";
// import CountOfOperations from "./CountOfOperations";
// import ReagentsBelowMinimum from "./ReagentsBelowMinimum";
// import ReagentsExpire from "./ReagentsExpire";
// import ReagentsTurnOverRatio from "./ReagentsTurnOverRatio.jsx"

// /**
//  * Формат: таблицы, числа, условное форматирование (красный – критично, жёлтый – предупреждение).
//  */
// const Statistics = () => {
//   return (
//     <>
//       <h2>Статистика</h2>
//       <AverageOperationSize />
//       <CountOfOperations />
//       <ReagentsBelowMinimum />
//       <ReagentsExpire />
//       <ReagentsTurnOverRatio />
//     </>
//   );
// };
// export default Statistics;

import { Grid, Container, Typography, Box, Paper } from "@mui/material";
import AverageOperationSize from "./AverageOperationSize";
import CountOfOperations from "./CountOfOperations";
import ReagentsBelowMinimum from "./ReagentsBelowMinimum";
import ReagentsExpire from "./ReagentsExpire";
import ReagentsTurnOverRatio from "./ReagentsTurnOverRatio.jsx";

const Statistics = () => {
  return (
    <Container maxWidth="xl">
      <Typography variant="h4" sx={{ mb: 4, fontWeight: 'bold', color: 'text.primary' }}>
        Статистика
      </Typography>

      <Grid container spacing={3}>
        

        <Grid size={{ xs:12, sm:6, md:6}}>
          <Paper elevation={0} sx={{ p: 2, border: '1px solid', borderColor: 'divider', height: '100%', borderRadius:2 }}>
            <Typography variant="subtitle1" sx={{ mb: 2, fontWeight: 700, color: 'primary.main' }}>
              Размеры и объемы операций
            </Typography>
            <AverageOperationSize />
          </Paper>
        </Grid>

        <Grid size={{ xs:12, sm:6, md:6}}>
          <Paper elevation={0} sx={{ p: 2, border: '1px solid', borderColor: 'divider', height: '100%' }}>
            <Typography variant="subtitle1" sx={{ mb: 2, fontWeight: 700, color: 'primary.main' }}>
              Интенсивность работы (кол-во записей)
            </Typography>
            <CountOfOperations />
          </Paper>
        </Grid>

        <Grid size={{ xs:12, sm:6, md:6}}>
          <Paper elevation={0} sx={{ p: 2, border: '1px solid', borderColor: 'divider', height: '100%' }}>
            <Typography variant="subtitle1" sx={{ mb: 2, fontWeight: 700, color: 'error.main' }}>
              Дефицит (Ниже минимума)
            </Typography>
            <ReagentsBelowMinimum />
          </Paper>
        </Grid>

        <Grid size={{ xs:12, sm:6, md:6}}>
          <Paper elevation={0} sx={{ p: 2, border: '1px solid', borderColor: 'divider', height: '100%' }}>
            <Typography variant="subtitle1" sx={{ mb: 2, fontWeight: 700, color: 'warning.main' }}>
              Контроль сроков годности
            </Typography>
            <ReagentsExpire />
          </Paper>
        </Grid>

        <Grid size={{ xs:12, sm:6, md:6}}>
          <Paper elevation={0} sx={{ p: 2, border: '1px solid', borderColor: 'divider' }}>
            <Typography variant="subtitle1" sx={{ mb: 2, fontWeight: 700, color: 'text.secondary' }}>
              Оборачиваемость реагентов
            </Typography>
            <ReagentsTurnOverRatio />
          </Paper>
        </Grid>

      </Grid>
    </Container>
  );
};

export default Statistics;
