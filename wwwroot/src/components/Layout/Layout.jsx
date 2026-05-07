import { useState } from "react";
import { Outlet } from "react-router-dom";
import { Box } from "@mui/material";
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
    <Box className={styles.appWrapper}>
      <Header 
        onMenuToggle={toogleMenuSideBar} 
        onNotificationToggle={toogleNotificationSideBar} 
        logout={logout} 
        user={user} 
      />

      <Box className={styles.content}>
        <MenuSideBar isOpen={isMenuSideBarOpen} />

        <Box 
          component="main" 
          className={styles.main}
        >
          <Outlet />
        </Box>

        <NotificationSideBar 
          isOpen={isNotificationSideBarOpen} 
          onClose={toogleNotificationSideBar} 
          user={user} 
        />
      </Box>

      <Footer user={user} />
    </Box>
  );
};