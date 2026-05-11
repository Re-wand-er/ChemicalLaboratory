import { useState, useEffect } from 'react';
import { Card, CardContent, Typography, Grid, Container, Box } from '@mui/material';

import ReagentsBarChart from '../Analytics/Graphics/ReagentsBarChart';
import RecentActivityFeed from './RecentActivityFeed';
import ExpirationCalendar from './ExpirationCalendar';
import UserActivityTop from './UserActivityTop';

import { fetchGetData } from '../../api/fetch';
import CardContainer from '../Analytics/CardContainer';

const Dashboard = () => {
  const [kpi, setKpi] = useState(null);

  useEffect(() => {
    fetchGetData('/api/dashboard/dashboard-kpi', setKpi);
  }, []);

  if (!kpi) return null;

  const kpiItems = [
    { label: 'Активные реактивы', value: kpi.activeReagentsCount, unit: '', color: 'var(--mui-palette-status-ownBlue)' },
    { label: 'Дефицит запасов', value: kpi.lowStockPercentage, unit: '%', color: 'var(--mui-palette-status-ownRed)' },
    { label: 'Просрочено', value: kpi.expiredPercentage, unit: '%', color: 'var(--mui-palette-status-ownOrange)' },
    { label: 'Истекают (30 дн.)', value: kpi.expiringSoonPercentage, unit: '%', color: 'var(--mui-palette-status-ownOrange)' },
    { label: 'Операций сегодня', value: kpi.operationsTodayCount, unit: '', color: 'var(--mui-palette-status-ownGreen)' },
    { label: 'Неликвид (180 дн)', value: kpi.illiquidPercentage, unit: '%', color: 'var(--mui-palette-status-ownPurple)' },
    { label: 'Срок хранения (DSI)', value: kpi.dsiDays, unit: ' дн.', color: 'var(--mui-palette-status-ownLightBlue)' },
    // { label: 'ДАЛЖНА БЫТЬ КРАСИВА', value: kpi.dsiDays, unit: ' дн.', color: 'var(--mui-palette-status-ownLightBlue)' },
  ];  
  
  return (
    <Container maxWidth="xl" sx={{ py: 4 }}>
      <Grid container spacing={3} justifyContent="center" sx={{ mb: 6 }}>
        {kpiItems.map((item, index) => (
          <Grid item xs={12} sm={6} md={3} lg={1.7} key={index}> 
            <Card sx={{ 
              borderTop: `4px solid ${item.color}`, // Перенес акцент наверх для чистоты
              height: '100%',
              boxShadow: '0 2px 10px rgba(0,0,0,0.05)' 
            }}>
              <CardContent>
                <Typography color="textSecondary" variant="caption" sx={{ fontWeight: 600, textTransform: 'uppercase' }}>
                  {item.label}
                </Typography>
                <Typography variant="h4" sx={{ mt: 1, fontWeight: 600 }}>
                  {item.value}<small style={{ fontSize: '1rem' }}>{item.unit}</small>
                </Typography>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>

      <Grid container spacing={3}>
        <Grid size={{ xs:12, sm:12, md:8}}>
          <Grid container spacing={3}>
            <Grid size={{ xs:12, sm:12, md:12}}>
              <CardContainer>
                <RecentActivityFeed title="Последние пользовательские операции"/>
              </CardContainer>
            </Grid>
            <Grid size={{ xs:12, sm:12, md:6}}>
              <CardContainer>
                <ExpirationCalendar title="Близкие к просрочке"/>
              </CardContainer>
            </Grid>
            <Grid size={{ xs:12, sm:12, md:6}}>
              <CardContainer>
                <UserActivityTop title="Операции за период"/>
              </CardContainer>
            </Grid>
          </Grid>
        </Grid>

        <Grid size={{ xs:12, sm:12, md:4}}>
          <CardContainer>
            <ReagentsBarChart startPeriod='Week' title="Аналитика по периодам"/>
          </CardContainer>
        </Grid>
      </Grid>

    </Container>
  );
};

export default Dashboard;