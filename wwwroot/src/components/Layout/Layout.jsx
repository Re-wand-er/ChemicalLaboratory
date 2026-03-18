import { useState } from "react";
import { Outlet } from "react-router-dom";
import { Header } from "./Header/Header";
import { SideBar } from "./SideBar/SideBar";
import { Footer } from "./Footer/Footer";

import styles from "./layout.module.css";

export const Layout = () => {
    const [isSideBarOpen, setIsSideBarOpen] = useState(true);

    const toogleSideBar = () => setIsSideBarOpen(!isSideBarOpen);

    return (
        <div className={styles.appWrapper}>
            <Header onToggle={toogleSideBar}/>

            <div className={styles.content}>
                <SideBar isOpen={isSideBarOpen} />
                <main className={styles.main}>
                    <Outlet />
                </main>
            </div>

            <Footer className={styles.footer}/>
        </div>
    );
};