import { NavLink } from 'react-router-dom';

import styles from'./header.module.css';

export const Header = (props) => {
   return (
    <header id={styles.header}>
      <div className={styles.logo}>
        <button onClick={props.onToggle}>☰</button>
        <NavLink className={styles.link} to="/dashboard">Химическая лоборатория</NavLink>
      </div>
      <nav className={styles.nav}>
        <NavLink className={styles.link} to="/">Уведомление</NavLink>
        <NavLink className={styles.link} to="/second">Выйти</NavLink>
      </nav>
    </header>
  );
};
