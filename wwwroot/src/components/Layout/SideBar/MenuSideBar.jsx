import SideBarSection from '../SideBarSection/SideBarSection';

import styles from './sidebar.module.css';

const sections = [
    {
        title: "Дашборд",
        links: [],
        to: "/dashboard"
    },
    {
        title: "Учет",
        links:[
            {to: "/reagents", label: "Реагенты"},
            {to: "/manufacturers", label: "Производители"},
            {to: "/suppliers", label: "Поставщики"}
        ],
        to: ""
    },
    {
        title: "Аналитика",
        links: [
            {to: "/statistics", label: "Статистика"},
            {to: "/graphics", label: "Графики"},
            {to: "/predicts", label: "Прогноз"}
        ],
        to: ""
    },
    {
        title: "Отчеты",
        links: [],
        to: "/reports"
    },
    {
        title: "Настройки",
        links: [
            {to: "/users", label: "Пользователи"},
            {to: "/categories", label: "Категории"},
            {to: "/notifications", label: "Уведомления"}
        ],
        to: ""
    }
];

export const MenuSideBar = (props) =>{
    return (
        <aside className={`${styles.sidebar} ${!props.isOpen ? styles.closed : ''}`}>
            <nav>
                <ul className={styles.menu}>
                {sections.map(item => (
                    <SideBarSection key={item.title} element={item} />
                ))}
                </ul>
            </nav>
        </aside>
  );
};