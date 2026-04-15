import { NavLink } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';
import NotificationBell from './NotificationBell';

import styles from'./header.module.css';

export const Header = ({ user, logout, onMenuToggle, onNotificationToggle }) => {
  return (
    <header id={styles.header}>
      <div className={styles.logo}>
        <button onClick={onMenuToggle}>☰</button>
        <NavLink className={styles.link} to="/dashboard">Учет химических реагентов</NavLink>
      </div>

      <nav className={styles.nav}>
        <NotificationBell onOpenDrawer={onNotificationToggle} />
        <NavLink className={styles.link} to="/user">{user.lastName} {user.firstName} {user.middleName}</NavLink>
        <NavLink className={styles.link} onClick={logout} to="/login">Выйти</NavLink>
      </nav>
    </header>
  );
};
