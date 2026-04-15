import { createContext, useContext, useState, useCallback } from 'react';

import { fetchGetData } from '../api/fetch';

const NotificationContext = createContext();

export const NotificationProvider = ({ children }) => {
  const [unreadCount, setUnreadCount] = useState(0);

  // Функция загрузки, которую можно вызвать извне
  const updateCount = useCallback(async () => {
    fetchGetData('/api/notification/unread-count', (data) => {
      const count = typeof data === 'object' ? data.value : data;
      setUnreadCount(count);
    });
  }, []);

  return (
    <NotificationContext.Provider value={{ unreadCount, updateCount }}>
      {children}
    </NotificationContext.Provider>
  );
};

// Хук для удобного использования
export const useNotifications = () => useContext(NotificationContext);
