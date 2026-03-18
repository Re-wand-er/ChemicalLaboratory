import { StrictMode, useEffect, useState } from 'react';
import { lazy, Suspense } from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { createRoot } from 'react-dom/client';
import { Layout } from './components/Layout/Layout.jsx';

/*Дашборд*/
const Dashboard = lazy(()=>import('./components/Dashboard/Dashboard.jsx'));

/*Учет*/
const Inventory = lazy(()=>import('./components/Inventory/Inventory.jsx'));
const Reagents = lazy(()=>import('./components/Inventory/Reagents.jsx'));
const Manufacturers = lazy(()=>import('./components/Inventory/Manufacturers.jsx'));
const Suppliers = lazy(()=>import('./components/Inventory/Suppliers.jsx'));

/*Аналитика*/
const Analytics = lazy(()=>import('./components/Analytics/Analytics.jsx'));
const Statistics = lazy(()=>import('./components/Analytics/Statistics.jsx'));
const Graphics = lazy(()=>import('./components/Analytics/Graphics.jsx'));
const Predicts = lazy(()=>import('./components/Analytics/Predicts.jsx'));

/*Отчеты*/
const Reports = lazy(()=>import('./components/Reports/Reports.jsx'));

/*Настройки*/
const Settings = lazy(()=>import('./components/Settings/Settings.jsx'));
const Users = lazy(()=>import('./components/Settings/Users.jsx'));
const Categories = lazy(()=>import('./components/Settings/Categories.jsx'));
const Notifications = lazy(()=>import('./components/Settings/Notifications.jsx'));

import './assets/main.css';

createRoot(document.getElementById('root')).render(
  <BrowserRouter>
      <Suspense fallback={<div>Подождите, идет загрузка...</div>}>
      <Routes>
        
        <Route path="/" element={<Layout />}>
          <Route index element={<h1>Home</h1>}/>
          <Route path="leave" element={<h2>Выйти</h2>}/>
          <Route path="secret" element={<h2>Поздравляю вы вышли на секретную страницу</h2>}/>

          {/*Дашборд*/}
          <Route path="dashboard" element={<Dashboard />}/>

          {/*Учет*/}
          <Route path="inventory" element={<Inventory />} />
          <Route path="reagents" element={<Reagents />}/>
          <Route path="manufacturers" element={<Manufacturers />}/>
          <Route path="suppliers" element={<Suppliers />}/>

          {/*Аналитика*/}
          <Route path="analytics" element={<Analytics />}/>
          <Route path="statistics" element={<Statistics />}/>
          <Route path="graphics" element={<Graphics />}/>
          <Route path="predicts" element={<Predicts />}/>

          {/*Отчеты*/}
          <Route path="reports" element={<Reports />}/>

          {/*Настройки*/}
          <Route path="settings" element={<Settings />}/>
          <Route path="users" element={<Users />}/>
          <Route path="categories" element={<Categories />}/>
          <Route path="notifications" element={<Notifications />}/>

          <Route path="*" element={<h2>Такой страницы не существует</h2>}/>
        </Route>
      </Routes>
      </Suspense>
  </BrowserRouter>
);
