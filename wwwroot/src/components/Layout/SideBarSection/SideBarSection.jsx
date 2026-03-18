import { useState } from "react";
import { NavLink } from "react-router-dom";


import styles from "./sideBarSection.module.css";

const SideBarSection = (props) =>{
    const [isOpen, setIsOpen] = useState(false);
    const hasSubLinks = props.element.links && props.element.links.length > 0; 

    // Когда в меню есть выпадающий список
    if (hasSubLinks) {
        return (
            <li className={styles.menuItem}>
                <button 
                    className={styles.toggleBtn} 
                    onClick={() => setIsOpen(!isOpen)}
                >
                    {props.element.title}
                    <span className={isOpen ? styles.rotate : ''}>▼</span>
                </button>
                
                <ul className={`${styles.subMenu} ${isOpen ? styles.show : ''}`}>
                    {props.element.links.map(link => (
                        <li key={link.to}>
                            <NavLink className={styles.singleLink} to={link.to}>{link.label}</NavLink>
                        </li>
                    ))}
                </ul>
            </li>
        );
    }

    // Когда в меню нет выпадающего списка
    return (
        <li className={styles.menuItem}>
            <NavLink 
                to={props.element.to}
                className={styles.singleLink}
            >
                {props.element.title}
            </NavLink>
        </li>
    );
}
export default SideBarSection;