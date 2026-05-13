import { useEffect } from 'react';
import { Badge, IconButton } from '@mui/material';
import NotificationsIcon from '@mui/icons-material/Notifications';

import { useNotifications } from '../../../context/NotificationContext';

const NotificationBell = ({ onOpenDrawer }) => {
  const { unreadCount, updateCount } = useNotifications();

  useEffect(() => {
    updateCount(); 
    const interval = setInterval(updateCount, 300000);
    return () => clearInterval(interval);
  }, [updateCount]);

  return (
    <IconButton onClick={onOpenDrawer} sx={{ color: '#000000' }}>
      <Badge badgeContent={unreadCount} color="error">
        <NotificationsIcon />
      </Badge>
    </IconButton>
  );
};


export default NotificationBell;