import styles from './footer.module.css';

export const Footer = ({ user, className }) =>{
    return (
        <footer className={`${styles.footer} ${className}`}>
            <h3>Разработчик... искренне пытался</h3>{/*{user.lastName} {user.firstName} {user.middleName} */}
        </footer>
    );
};