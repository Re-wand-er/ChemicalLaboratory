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

const reportSections = [
  {
    title: "Реагенты",
    links: [
      { to: "reagent", label: "Остатки реагентов" },
      { to: "below-minimum", label: "Ниже минимума" },
      { to: "expired", label: "Просроченный период" },
    ]
  },
  {
    title: "Движение",
    links: [
      { to: "income", label: "Поступления за период" },
      { to: "consumption", label: "Расход за период" },
      { to: "writeOff", label: "Списание за период" },
      { to: "adjustment", label: "Корректировки за период" },
      { to: "update", label: "Обновления за период" },
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
    <Box sx={{ display: 'flex', height:'100vh', width: '100%' }}>
      <Paper 
        elevation={0} 
        sx={{ 
          width: 280, 
          flexShrink: 0, 
          border: '1px solid', 
          borderColor: 'divider', 
          display: 'flex', 
          flexDirection: "column",
          bgcolor: 'background.paper',
          height: 'fit-content',
          maxHeight: '100%',
          alignSelf: 'flex-start', 
        }}
      >
        <Typography 
          variant="subtitle1" 
          sx={{ 
            p:1.5, 
            pl: 2.5, 
            fontWeight: 700, 
            color: 'var(--mui-palette-primary-main)' //color: 'primary.main'
          }}>
          Типы отчетов
        </Typography>
        <Divider />
        <List sx={{ py: 0, overflowY: 'auto' }}>
          {reportSections.map((section, index) => (
            <Box key={index}>
              <Typography 
                variant="caption" 
                sx={{ 
                  px: 3, 
                  pt: 2, 
                  pb: 0.5, 
                  display: 'block', 
                  fontWeight: 700, 
                  letterSpacing: 1,
                  color: 'text.disabled',
                  textTransform: 'uppercase'
                }}
              >
                {section.title}
              </Typography>
              {section.links.map((link) => (
                <ListItemButton 
                  key={link.to} 
                  component={NavLink} 
                  to={link.to}
                  sx={{
                    mx: 1, 
                    borderRadius: 2,
                    '&.active': {
                      bgcolor: 'var(--mui-palette-primary-light)',
                      color: 'var(--mui-palette-background-paper)',
                      '& .MuiTypography-root': { fontWeight: 600 }
                    }
                  }}
                >
                  <ListItemText 
                    primary={link.label} 
                    primaryTypographyProps={{ variant: 'body2' }} 
                  />
                </ListItemButton>
              ))}
              {index !== reportSections.length - 1 && <Box sx={{ height: 4 }} />}
            </Box>
          ))}
        </List>
      </Paper>

      <Box sx={{ flexGrow: 1, paddingLeft:3, minWidth:0}}>
        <Outlet />
      </Box>
    </Box>
  );
};

export default Reports;
