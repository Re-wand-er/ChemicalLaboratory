import { useState, useEffect } from 'react';
import { Drawer, Box, Typography, Button, List, ListItem, Divider, IconButton } from '@mui/material';
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';
import CloseIcon from '@mui/icons-material/Close';
import DoneIcon from '@mui/icons-material/Done';
import CloudDownloadIcon from '@mui/icons-material/CloudDownload';

import { useNotifications } from '../../../context/NotificationContext';

import { fetchGetData, fetchPostData } from '../../../api/fetch';

import styles from './notificationSideBar.module.css';

export const NotificationSideBar = ({ user, isOpen, onClose }) => {
  const [notifications, setNotifications] = useState([]);
  const { updateCount } = useNotifications();

  const loadNotifications = () => {
    if (isOpen) fetchGetData(`/api/notification/load`, setNotifications);
  }

  useEffect(() => {
    loadNotifications();
    const interval = setInterval(loadNotifications, 300000);

    console.log(notifications);
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

  const downloadNotificationInvoice = async (id) => {
    try {
      const response = await fetch(`/api/notification/${id}/download`, {
        method: 'GET',
        credentials: 'include' // Передаем куки сессии авторизации
      });
  
      if (!response.ok) {
        throw new Error(`Ошибка скачивания: ${response.status}`);
      }
  
      // Читаем бинарный ответ сервера
      const blob = await response.blob();
      
      // Создаем временную ссылку в памяти браузера
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      
      // Имя файла браузер возьмет из заголовка Content-Disposition, 
      // но мы даем дефолтное на случай сбоя
      link.setAttribute('download', `Накладная_Дефицит_${id}.pdf`);
      
      document.body.appendChild(link);
      link.click();
      
      link.parentNode.removeChild(link);
      window.URL.revokeObjectURL(url);
      
    } catch (error) {
      console.error("Ошибка при получении PDF:", error.message);
    }
  };
  

  // return (
  //   <Drawer 
  //     anchor="right" 
  //     open={isOpen} 
  //     onClose={onClose}
  //     classes={{ paper: styles.drawerPaper }}
  //   >
  //     {/* Шапка уведомлений */}
  //     <Box className={styles.header}>
  //       <Typography variant="h6" sx={{ fontWeight: 600 }}>Уведомления</Typography>
  //       <Box>
  //         <Button size="small" onClick={markAllRead} sx={{ mr: 1, textTransform: 'none', fontSize: 16 }}>
  //           Прочитать все
  //         </Button>
  //         <IconButton onClick={onClose} size="small">
  //           <CloseIcon />
  //         </IconButton>
  //       </Box>
  //     </Box>

  //     {/* Список уведомлений */}
  //     <List sx={{ p: 0 }}>
  //       {notifications.map((item) => (
  //         <Box key={item.id}>
  //           <ListItem 
  //             className={styles.notificationItem}
  //             alignItems="flex-start"
  //             sx={{ 
  //               bgcolor: item.isRead ? 'action.hover' : 'transparent',
  //               borderLeft: item.isRead ? 'none' : '4px solid var(--mui-palette-primary-main)',
  //             }}
  //           >
  //             {/* Верх: Заголовок и действия */}
  //             <Box className={styles.itemHeader}>
  //               <Typography variant="subtitle2" sx={{ fontWeight: 700, lineHeight: 1.2 }}>
  //                 {item.title}
  //               </Typography>
  //               <Box sx={{ display: 'flex', gap: 0.5 }}>
  //                 {!item.isRead && (
  //                   <IconButton 
  //                     size="small" 
  //                     onClick={() => handleRead(item.id)} 
  //                     sx={{ color: 'primary.main' }}
  //                     title="Отметить как прочитанное"
  //                   >
  //                     <DoneIcon fontSize="small" />
  //                   </IconButton>
  //                 )}
  //                 <IconButton 
  //                   size="small" 
  //                   onClick={() => handleDelete(item.id)} 
  //                   sx={{ color: 'text.secondary' }}
  //                   title="Удалить"
  //                 >
  //                   <DeleteOutlineIcon fontSize="small" />
  //                 </IconButton>
  //               </Box>
  //             </Box>

  //             {/* Сообщение */}
  //             <Typography variant="body2" className={styles.message}>
  //               {item.message}
  //             </Typography>

  //             {/* Низ: тег и дата */}
  //             <Box className={styles.itemFooter}>
  //               <Box className={styles.reagentTag}>
  //                 ⚛️ {item.reagentName}
  //               </Box>
  //               <Typography variant="caption" color="text.secondary">
  //                 {item.createdAt}
  //               </Typography>
  //             </Box>
  //           </ListItem>
  //           <Divider />
  //         </Box>
  //       ))}

  //       {notifications.length === 0 && (
  //         <Typography sx={{ p: 4, textAlign: 'center', color: 'text.secondary', fontStyle: 'italic' }}>
  //           У вас пока нет уведомлений
  //         </Typography>
  //       )}
  //     </List>
  //   </Drawer>
  // );

  return (
    <Drawer 
      anchor="right" 
      open={isOpen} 
      onClose={onClose}
      classes={{ paper: styles.drawerPaper }}
    >
      {/* Шапка уведомлений */}
      <Box className={styles.header}>
        <Typography variant="h6" sx={{ fontWeight: 600 }}>Уведомления</Typography>
        <Box>
          <Button size="small" onClick={markAllRead} sx={{ mr: 1, textTransform: 'none', fontSize: 16 }}>
            Прочитать все
          </Button>
          <IconButton onClick={onClose} size="small">
            <CloseIcon />
          </IconButton>
        </Box>
      </Box>
  
      {/* Список уведомлений */}
      <List sx={{ p: 0 }}>
        {notifications.map((item) => (
          <Box key={item.id}>
            <ListItem 
              className={styles.notificationItem}
              alignItems="flex-start"
              sx={{ 
                display: 'flex',
                flexDirection: 'column', // Гарантирует правильный перенос блоков сверху вниз
                bgcolor: item.isRead ? 'action.hover' : 'transparent',
                borderLeft: item.isRead ? 'none' : '4px solid var(--mui-palette-primary-main)',
                gap: 1 // Добавляем аккуратный отступ между строками
              }}
            >
              {/* Верх: Заголовок и действия */}
              <Box className={styles.itemHeader} sx={{ width: '100%' }}>
                <Typography variant="subtitle2" sx={{ fontWeight: 700, lineHeight: 1.2 }}>
                  {item.title}
                </Typography>
                <Box sx={{ display: 'flex', gap: 0.5 }}>
                  {!item.isRead && (
                    <IconButton 
                      size="small" 
                      onClick={() => handleRead(item.id)} 
                      sx={{ color: 'primary.main' }}
                      title="Отметить как прочитанное"
                    >
                      <DoneIcon fontSize="small" />
                    </IconButton>
                  )}
                  <IconButton 
                    size="small" 
                    onClick={() => handleDelete(item.id)} 
                    sx={{ color: 'text.secondary' }}
                    title="Удалить"
                  >
                    <DeleteOutlineIcon fontSize="small" />
                  </IconButton>
                </Box>
              </Box>
  
              {/* Сообщение */}
              <Typography variant="body2" className={styles.message} sx={{ width: '100%' }}>
                {item.message}
              </Typography>
  
              {/* ДОБАВЛЕНО: Кнопка скачивания отчета, если FilePath присутствует */}
              {item.filePath && (
                <Box sx={{ width: '100%', mt: 0.5 }}>
                  <Button
                    variant="outlined"
                    size="small"
                    color="primary"
                    startIcon={<CloudDownloadIcon />}
                    onClick={() => downloadNotificationInvoice(item.id)}
                    sx={{ 
                      textTransform: 'none', 
                      borderRadius: '6px',
                      fontWeight: 600,
                      fontSize: '13px'
                    }}
                  >
                    Скачать накладную (.pdf)
                  </Button>
                </Box>
              )}
  
              {/* Низ: тег и дата */}
              <Box className={styles.itemFooter} sx={{ width: '100%', mt: 0.5 }}>
                {/* Рендерим тег только если уведомление привязано к конкретному реагенту */}
                {item.reagentName ? (
                  <Box className={styles.reagentTag}>
                    ⚛️ {item.reagentName}
                  </Box>
                ) : (
                  <Box className={styles.reagentTag} sx={{ fontStyle: 'italic', color: 'text.secondary' }}>
                    📋 Системный отчет
                  </Box>
                )}
                
                <Typography variant="caption" color="text.secondary">
                  {item.createdAt}
                </Typography>
              </Box>
            </ListItem>
            <Divider />
          </Box>
        ))}
  
        {notifications.length === 0 && (
          <Typography sx={{ p: 4, textAlign: 'center', color: 'text.secondary', fontStyle: 'italic' }}>
            У вас пока нет уведомлений
          </Typography>
        )}
      </List>
    </Drawer>
  );
};