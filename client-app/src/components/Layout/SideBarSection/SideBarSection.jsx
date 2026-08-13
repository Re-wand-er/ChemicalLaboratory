import { useState, useEffect } from "react";
import { NavLink, useLocation } from "react-router-dom";
import { List, ListItem, ListItemButton, ListItemText, Collapse } from "@mui/material";
import ExpandMore from '@mui/icons-material/ExpandMore';
import ExpandLess from '@mui/icons-material/ExpandLess';

import styles from "./sideBarSection.module.css";

const SideBarSection = ({ element }) => {
  const { pathname } = useLocation();
  const hasSubLinks = element.links && element.links.length > 0;
  
  // Авто-открытие секции, если активная ссылка внутри неё
  const isAnySubLinkActive = hasSubLinks && element.links.some(link => pathname.includes(link.to));
  const [isOpen, setIsOpen] = useState(isAnySubLinkActive);

  const toggleOpen = () => setIsOpen(!isOpen);

  if (hasSubLinks) {
    return (
      <>
        <ListItem disablePadding>
          <ListItemButton 
            onClick={toggleOpen} 
            className={`${styles.toggleBtn}`}
          >
            <ListItemText primary={element.title} />
            {isOpen ? <ExpandLess fontSize="small" /> : <ExpandMore fontSize="small" />}
          </ListItemButton>
        </ListItem>
        
        <Collapse in={isOpen} timeout="auto" unmountOnExit>
          <List component="div" disablePadding>
            {element.links.map((link) => (
              <NavLink 
                key={link.to} 
                to={link.to} 
                className={({ isActive }) => 
                  `${styles.subLink} ${isActive ? styles.activeLink : ''}`
                }
              >
                {link.label}
              </NavLink>
            ))}
          </List>
        </Collapse>
      </>
    );
  }

  return (
    <ListItem disablePadding>
      <NavLink 
        to={element.to} 
        className={({ isActive }) => 
          `${styles.singleLink} ${isActive ? styles.activeLink : ''}`
        }
      >
        {element.title}
      </NavLink>
    </ListItem>
  );
};

export default SideBarSection;