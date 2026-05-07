import { Grid, Container } from '@mui/system';

import CategoryPie from './CategoryPie.jsx'
import ReagentsBarChart from './ReagentsBarChart.jsx';
import ReagentsLineChart from './ReagentsLineChart.jsx';
import ReagentsPie from './ReagentsPie.jsx';
import { Typography } from '@mui/material';

const Graphics = () => {
  return (
    <Container maxWidth="xl">
      <Typography variant="h4" sx={{ mb: 4, fontWeight: 'bold', color: 'text.primary' }}>
        Графики
      </Typography>
      
      <Grid container spacing={3}>
        <Grid size={{ xs: 12, md: 6, lg: 4 }}>
          <CategoryPie title="Потребление реагентов по категориям"/>
        </Grid>

        <Grid size={{ xs: 12, md: 6, lg: 4 }}>
          <ReagentsPie title="Использование реагентов"/>
        </Grid>

        <Grid size={{ xs: 12, md: 6, lg: 4 }}>
          <ReagentsBarChart filterBar={true} />{/* title="Топ используемых реагентов"*/}
        </Grid>

        <Grid size={12}>
          <ReagentsLineChart title="Аналитика по периодам"/>
        </Grid>
      </Grid>
    </Container>
  );
};
export default Graphics;