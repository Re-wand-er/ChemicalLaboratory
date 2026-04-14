import { NavLink } from 'react-router-dom';
import { useAuth } from '../../../context/AuthContext';

import styles from'./header.module.css';

export const Header = (props) => {
  const { user, logout } = useAuth();

  return (
   <header id={styles.header}>
     <div className={styles.logo}>
       <button onClick={props.onToggle}>☰</button>
       <NavLink className={styles.link} to="/dashboard">Учет химических реагентов</NavLink>
     </div>
     <nav className={styles.nav}>
       <NavLink className={styles.link} to="/">Уведомление</NavLink>
       <NavLink className={styles.link} to="/user">{user.lastName} {user.firstName} {user.middleName}</NavLink>
       <NavLink className={styles.link} onClick={logout} to="/login">Выйти</NavLink>
     </nav>
   </header>
  );
};
