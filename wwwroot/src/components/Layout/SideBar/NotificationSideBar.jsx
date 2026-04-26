import { useState, useEffect } from 'react';
import { Drawer, Box, Typography, Button, List, ListItem, ListItemText, Divider, IconButton } from '@mui/material';
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import CloseIcon from '@mui/icons-material/Close';
import DoneIcon from '@mui/icons-material/Done';

import { useNotifications } from '../../../context/NotificationContext';

import { fetchGetData, fetchPostData } from '../../../api/fetch';

export const NotificationSideBar = ({ user, isOpen, onClose }) => {
  const [notifications, setNotifications] = useState([]);
  const { updateCount } = useNotifications();

  const loadNotifications = () => {
    if (isOpen) fetchGetData(`/api/notification/load`, setNotifications);
  }


  useEffect(() => {
    loadNotifications();
    const interval = setInterval(loadNotifications, 300000);
    return () => clearInterval(interval);
  }, [isOpen]);


  const markAllRead = async () => {
    await fetchPostData(`/api/notification/read-all`);
    updateCount(); 
    loadNotifications();
  };

  const handleRead = async (id) => {
    await fetchPostData(`/api/notification/${id}/read`);
    updateCount(); 
    loadNotifications();
  }

  const handleDelete = async (id) => {
    await fetchPostData(`/api/notification/${id}/delete`);
    updateCount(); 
    loadNotifications();
  }

  return (
    <Drawer
      anchor="right"
      open={isOpen}
      onClose={onClose}
      // Регулируем ширину здесь
      PaperProps={{ sx: { width: { xs: '100%', sm: 400 } } }}
    >
      {/* Шапка уведомлений */}
      <Box sx={{ p: 2, display: 'flex', alignItems: 'center', justifyContent: 'space-between', borderBottom: 1, borderColor: 'divider' }}>
        <Typography variant="h6">Уведомления</Typography>
        <Box>
          <Button size="small" onClick={markAllRead} sx={{ mr: 1 }}>
            Прочитать все
          </Button>
          <IconButton onClick={onClose}>
            <CloseIcon />
          </IconButton>
        </Box>
      </Box>

      {/* Список уведомлений */}
      <List sx={{ p: 0 }}>
        {notifications.map((item) => (
          <Box key={item.id}>
            <ListItem 
              alignItems="flex-start"
              sx={{ 
                bgcolor: item.isRead ? 'action.hover' : 'transparent',
                borderLeft: item.isRead ? 'none' : '4px solid #1976d2',
                transition: '0.3s',
                flexDirection: 'column',
                alignItems: 'stretch'
              }}
            >
             
              <Box sx={{ display: 'flex', justifyContent: 'space-between', width: '100%', mb: 1 }}>
                <Typography variant="subtitle2" sx={{ fontWeight: 'bold' }}>
                  {item.title}
                </Typography>
                <Box sx={{ display: 'flex', gap: 0.5 }}>
                  {!item.isRead && (
                    <IconButton size="small" onClick={() => handleRead(item.id)} sx={{ color: 'primary.main', p: 0.5 }}>
                      <DoneIcon fontSize="small" />
                    </IconButton>
                  )}
                  <IconButton size="small" onClick={() => handleDelete(item.id)} sx={{ color: 'text.secondary', p: 0.5 }}>
                    <DeleteOutlineIcon fontSize="small" />
                  </IconButton>
                </Box>
              </Box>

              {/* Сообщение */}
              <Typography variant="body2" color="text.primary" sx={{ mb: 1 }}>
                {item.message}
              </Typography>

              {/* Нижняя строка: реактив + дата */}
              <Box sx={{ display: 'flex', justifyContent: 'space-between' }}>
                <Typography variant="caption" sx={{ bgcolor: 'grey.200', px: 1, borderRadius: 1 }}>
                  ⚛️ {item.reagentName}
                </Typography>
                <Typography variant="caption" color="text.secondary">
                  {item.createdAt}
                </Typography>
              </Box>
            </ListItem>
            <Divider component="li" />
          </Box>
        ))}
        
        {notifications.length === 0 && (
          <Typography sx={{ p: 3, textAlign: 'center', color: 'text.secondary' }}>
            У вас пока нет уведомлений
          </Typography>
        )}
      </List>
    </Drawer>
  );
};
