import styles from './footer.module.css';

export const Footer = ({ user, className }) =>{
    return (
        <footer className={`${styles.footer} ${className}`}>
            <h2>Разработчик {user.lastName} {user.firstName} {user.middleName}</h2>
        </footer>
    );
};