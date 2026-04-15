import { Badge, IconButton } from '@mui/material';
import NotificationsIcon from '@mui/icons-material/Notifications';
import { useState, useEffect } from 'react';

import { useNotifications } from '../../../context/NotificationContext';

import { fetchGetData } from '../../../api/fetch';

const NotificationBell = ({ onOpenDrawer }) => {
  const { unreadCount, updateCount } = useNotifications();

  useEffect(() => {
    updateCount(); 
    const interval = setInterval(updateCount, 300000);
    return () => clearInterval(interval);
  }, [updateCount]);

  return (
    <IconButton onClick={onOpenDrawer} sx={{ color: '#ffffff' }}>
      <Badge badgeContent={unreadCount} color="error">
        <NotificationsIcon />
      </Badge>
    </IconButton>
  );
};


export default NotificationBell;