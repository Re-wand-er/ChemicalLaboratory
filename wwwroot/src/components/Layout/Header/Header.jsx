import { NavLink } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import NotificationBell from './NotificationBell';

import styles from'./header.module.css';

export const Header = ({ user, logout, onMenuToggle, onNotificationToggle }) => {
  return (
    <header id={styles.header}>
      <div className={styles.logo}>
        <button onClick={onMenuToggle} title="Меню">☰</button>
        <NavLink className={styles.linkLogo} to="/dashboard">
          Учет химических реагентов
        </NavLink>
      </div>

      <nav className={styles.nav}>
        <NotificationBell onOpenDrawer={onNotificationToggle} />
        
        {/* Добавил отдельный класс для имени, чтобы отделить его от кнопки выхода */}
        <NavLink className={`${styles.link} ${styles.userName}`} to="/user">
          {user.lastName} {user.firstName[0]}.{user.middleName[0]}.
        </NavLink>
        
        <NavLink className={styles.link} onClick={logout} to="/login" data-variant="danger">
          Выйти
        </NavLink>
      </nav>
    </header>
  );
};

