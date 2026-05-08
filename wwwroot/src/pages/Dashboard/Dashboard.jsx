import { useState, useEffect } from 'react';
import { Card, CardContent, Typography, Grid, Container, Box } from '@mui/material';

import ReagentsBarChart from '../Analytics/Graphics/ReagentsBarChart';
import RecentActivityFeed from './RecentActivityFeed';
import ExpirationCalendar from './ExpirationCalendar';
import UserActivityTop from './UserActivityTop';

import { fetchGetData } from '../../api/fetch';

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
  ];  
  
  return (
    <Container maxWidth="xl" sx={{ py: 4 }}>
      {/* 1. Блок KPI: центрируются, на мобилках 1 в ряд, на планшетах 2, на десктопе 4 */}
      <Grid container spacing={3} justifyContent="center" sx={{ mb: 6 }}>
        {kpiItems.map((item, index) => (
          <Grid item xs={12} sm={6} md={3} lg={1.7} key={index}> 
            {/* lg={1.7} позволит уместить 7 карточек в ряд на широком экране */}
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

      {/* 2. Основная активность: на всю ширину */}
      <Grid container spacing={3} sx={{ mb: 4 }}>
        <Grid item xs={12}>
           <RecentActivityFeed />
        </Grid>
      </Grid>

      {/* 3. Нижний ярус: три колонки */}
      <Grid container spacing={3} alignItems="stretch">
        {/* Близкие к просрочке */}
        <Grid item xs={12} md={4} lg={3}>
          <ExpirationCalendar />
        </Grid>

        {/* Топ пользователей */}
        <Grid item xs={12} md={4} lg={3}>
          <UserActivityTop />
        </Grid>

        {/* График: самый широкий в этой строке */}
        <Grid item xs={12} md={4} lg={6}>
          <Box sx={{ 
            bgcolor: 'background.paper', 
            p: 2, 
            borderRadius: 2, 
            boxShadow: '0 2px 10px rgba(0,0,0,0.05)',
            height: '100%' 
          }}>
            <Typography variant="h6" sx={{ mb: 2, px: 2 }}>Аналитика потребления</Typography>
            <ReagentsBarChart startPeriod='Week' localeWidth='100%' />
          </Box>
        </Grid>adfdsf
      </Grid>
    </Container>
  );
};

export default Dashboard;