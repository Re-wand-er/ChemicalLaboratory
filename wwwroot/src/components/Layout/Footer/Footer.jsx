import styles from './footer.module.css';

export const Footer = (props) =>{
    return (
        <footer className={`${styles.footer} ${props.className}`}>
            <h2>Footer текст</h2>
        </footer>
    );
};