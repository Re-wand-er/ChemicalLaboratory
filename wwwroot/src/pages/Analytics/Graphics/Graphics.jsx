import { Grid, Container } from '@mui/system';
import { Typography } from '@mui/material';

import CardContainer from '../CardContainer.jsx';
import CategoryPie from './CategoryPie.jsx'
import ReagentsBarChart from './ReagentsBarChart.jsx';
import ReagentsLineChart from './ReagentsLineChart.jsx';
import ReagentsPie from './ReagentsPie.jsx';

const Graphics = () => {
  return (
    <Container maxWidth="xl">
      <Typography variant="h4" sx={{ mb: 4, fontWeight: 'bold', color: 'text.primary' }}>
        Графики
      </Typography>
      
      <Grid container spacing={3}>
        <Grid size={{ xs: 12, md: 6, lg: 4 }}>
          <CardContainer
            title="Потребление реагентов по категориям"
          >
            <CategoryPie/>
          </CardContainer>
        </Grid>

        <Grid size={{ xs: 12, md: 6, lg: 4 }}>
          <CardContainer>
            <ReagentsPie title="Использование реагентов"/>
          </CardContainer>
        </Grid>

        <Grid size={{ xs: 12, md: 6, lg: 4 }}>
          <CardContainer>
            <ReagentsBarChart filterBar={true} />{/* title="Топ используемых реагентов"*/}
          </CardContainer>
        </Grid>

        <Grid size={12}>
          <CardContainer>
            <ReagentsLineChart title="Аналитика по периодам"/>
          </CardContainer>
        </Grid>
      </Grid>
    </Container>
  );
};
export default Graphics;