import styles from './footer.module.css';

export const Footer = ({ user, className }) => {
  const currentYear = new Date().getFullYear();

  return (
    <footer className={`${styles.footer} ${className}`}>
      <p className={styles.footerText}>
        © {currentYear} Система учета реагентов. 
        <span className={styles.footerAuthor}>Разработано с заботой о пользователях</span>
      </p>
    </footer>
  );
};
