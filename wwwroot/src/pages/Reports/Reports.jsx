import { Fragment } from 'react';
import { 
  Box, 
  List, 
  ListItem, 
  ListItemButton, 
  ListItemText, 
  Typography, 
  Divider, 
  Paper 
} from "@mui/material";
import { Outlet, NavLink, useLocation } from "react-router-dom";

// Данные, адаптированные под вашу задачу
const reportSections = [
  {
    title: "Реагенты",
    links: [
      { to: "date-balance", label: "Остатки на дату" },
      { to: "below-minimum", label: "Ниже минимума" },
      { to: "expired", label: "Просроченный период" },
    ]
  },
  {
    title: "Движение",
    links: [
      { to: "income", label: "Поступления за период" },
      { to: "consumption", label: "Расход за период" },
      { to: "expense", label: "Списание" },
    ]
  },
  {
    title: "Аналитика",
    links: [
      { to: "top-usage", label: "Топ используемых реагентов" },
      { to: "forecast", label: "Прогноз заказа реагентов" },
    ]
  }
];

const Reports = () => {
  return (
    <Box sx={{ display: 'flex', height: '100vh', width: '100%' }}>
      {/* Левая панель: Список отчетов */}
      <Paper 
        elevation={0} 
        sx={{ 
          width: '17%', 
          borderRight: '1px solid', 
          borderColor: 'divider',
          display: 'flex',
          flexDirection: 'column'
        }}
      >
        <Typography variant="h6" sx={{ p: 2, fontWeight: 'bold' }}>
          Перечень отчетов
        </Typography>
        <Divider />
        
        <List sx={{ flexGrow: 1, py: 0, overflowY: 'auto' }}>
          {reportSections.map((section, index) => (
            <Fragment key={index}>
							
              {/* Заголовок секции (Реагенты, Движение и т.д.) */}
              <ListItem sx={{ bgcolor: 'action.hover', py: 0.5 }}>
                <Typography variant="overline" sx={{ fontWeight: 'bold', color: 'text.secondary' }}>
                  {section.title}
                </Typography>
              </ListItem>
              
              {/* Ссылки внутри секции */}
              {section.links.map((link) => (
                <ListItemButton 
                  key={link.to}
                  component={NavLink}
                  to={link.to}
                  // sx={{
                  //   '&.active': {
                  //     borderRight: '4px solid #1976d2',
                  //     bgcolor: 'primary.light',
                  //     color: 'primary.main',
                  //     '& .MuiListItemText-primary': { fontWeight: 'bold' }
                  //   }
                  // }}
                >

                  <ListItemText primary={link.label} sx={{ pl: 1 }} />
									
                </ListItemButton>
              ))}
              <Divider />
            </Fragment>
          ))}
        </List>
      </Paper>

      {/* Правая панель: Параметры и контент */}
      <Box sx={{ flexGrow: 1, p: 3, bgcolor: '#f5f5f5', overflowY: 'auto' }}>
        <Outlet />
      </Box>
    </Box>
  );
};

export default Reports;
