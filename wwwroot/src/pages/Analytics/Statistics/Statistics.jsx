import { Grid, Container, Typography, Box, Paper } from "@mui/material";

import CardContainer from "../CardContainer.jsx";
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
          <CardContainer>
            <AverageOperationSize title="Среднее количество операций"/>
          </CardContainer>
        </Grid>

        <Grid size={{ xs:12, sm:6, md:6}}>
          <CardContainer>
            <CountOfOperations title="Интенсивность работы"/>
          </CardContainer>
        </Grid>

        <Grid size={{ xs:12, sm:6, md:6}}>
          <CardContainer>
            <ReagentsExpire title="Контроль сроков годности"/>
          </CardContainer>
        </Grid>

        <Grid size={{ xs:12, sm:6, md:6}}>
          <CardContainer>
            <ReagentsTurnOverRatio title="Оборачиваемость реагентов"/>
          </CardContainer>
        </Grid>

        <Grid size={{ xs:12, sm:6, md:6}}>
          <CardContainer>
            <ReagentsBelowMinimum title="Дефицит (Ниже минимума)"/>
          </CardContainer>
        </Grid>

      </Grid>
    </Container>
  );
};

export default Statistics;
