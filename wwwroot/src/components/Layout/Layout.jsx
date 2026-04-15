import { useState } from "react";
import { Outlet } from "react-router-dom";
import { Header } from "./Header/Header";
import { MenuSideBar } from "./SideBar/MenuSideBar";
import { NotificationSideBar } from "./SideBar/NotificationSideBar";
import { Footer } from "./Footer/Footer";

import { useAuth } from '../../context/AuthContext';

import styles from "./layout.module.css";

export const Layout = () => {
  const { user, logout } = useAuth();  

    const [isMenuSideBarOpen, setIsMenuSideBarOpen] = useState(true);
    const toogleMenuSideBar = () => setIsMenuSideBarOpen(!isMenuSideBarOpen);

    const [isNotificationSideBarOpen, setIsNotificationSideBarOpen] = useState(false);
    const toogleNotificationSideBar = () => setIsNotificationSideBarOpen(!isNotificationSideBarOpen);

    return (
        <div className={styles.appWrapper}>
            <Header 
							onMenuToggle={toogleMenuSideBar} 
							onNotificationToggle={toogleNotificationSideBar}
							logout={logout}
							user={user}
						/>

            <div className={styles.content}>
                <MenuSideBar isOpen={isMenuSideBarOpen} />

                <main className={styles.main}>
                    <Outlet />
                </main>

                <NotificationSideBar 
									isOpen={isNotificationSideBarOpen} 
									onClose={toogleNotificationSideBar}
									user={user}
								/>
            </div>

            <Footer user={user} className={styles.footer}/>
        </div>
    );
};