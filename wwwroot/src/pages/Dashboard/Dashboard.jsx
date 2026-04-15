import { useState, useEffect } from 'react';
import { Card, CardContent, Typography, Grid } from '@mui/material';

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
    { label: 'Активные реактивы', value: kpi.activeReagentsCount, unit: '', color: '#2196f3' },
    { label: 'Дефицит запасов', value: kpi.lowStockPercentage, unit: '%', color: '#f44336' },
    { label: 'Просрочено', value: kpi.expiredPercentage, unit: '%', color: '#ff9800' },
    { label: 'Истекают (30 дн.)', value: kpi.expiringSoonPercentage, unit: '%', color: '#ff9800' },
    { label: 'Операций сегодня', value: kpi.operationsTodayCount, unit: '', color: '#4caf50' },
    { label: 'Неликвид (180 дн)', value: kpi.illiquidPercentage, unit: '%', color: '#9c27b0' },
    { label: 'Срок хранения (DSI)', value: kpi.dsiDays, unit: ' дн.', color: '#00bcd4' },
    //{ label: 'Операций сегодня', value: kpi.operationsTodayCount, unit: '', color: '#4caf50' }, ?
  ];

  return (
    <div style={{display: 'flex', justifyContent: 'center'}}>
      <Grid container spacing={2} sx={{ mb: 4 }}>
        {kpiItems.map((item, index) => (
          <Grid item xs={12} sm={6} md={3} key={index}>
            <Card sx={{ borderLeft: `5px solid ${item.color}` }}>
              <CardContent>
                <Typography color="textSecondary" variant="subtitle2" gutterBottom>
                  {item.label}
                </Typography>
                <Typography variant="h4" component="div" sx={{ fontWeight: 'bold' }}>
                  {item.value}{item.unit}
                </Typography>
              </CardContent>
            </Card>
          </Grid>
        ))}

        <RecentActivityFeed />
        <ExpirationCalendar />
        <UserActivityTop />
        <ReagentsBarChart  startPeriod='Day' localeWidth='400px'/>
      </Grid>

    </div>
  );
};

export default Dashboard;