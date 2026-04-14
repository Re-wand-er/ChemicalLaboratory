import { StrictMode, useEffect, useState } from 'react';
import { lazy, Suspense } from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { createRoot } from 'react-dom/client';
import { Layout } from './components/Layout/Layout.jsx';
import { ProtectedRoute } from './components/ProtectedRoute.jsx';
import { AuthProvider } from './context/AuthContext'; 
import { Login } from './pages/Login/Login.jsx';
import { ResetPassword } from './pages/Login/ResetPassword.jsx';

import './assets/main.css';

/*Дашборд*/
const Dashboard = lazy(()=>import('./pages/Dashboard/Dashboard.jsx'));

/*Учет*/
const Inventory = lazy(()=>import('./pages/Inventory/Inventory.jsx'));
const Reagents = lazy(()=>import('./pages/Inventory/Reagents/Reagents.jsx'));
const Manufacturers = lazy(()=>import('./pages/Inventory/Manufacturers.jsx'));
const Suppliers = lazy(()=>import('./pages/Inventory/Suppliers/Suppliers.jsx'));

/*Аналитика*/
const Analytics = lazy(()=>import('./pages/Analytics/Analytics.jsx'));
const Statistics = lazy(()=>import('./pages/Analytics/Statistics/Statistics.jsx'));
const Graphics = lazy(()=>import('./pages/Analytics/Graphics/Graphics.jsx'));
const Predicts = lazy(()=>import('./pages/Analytics/Predicts/Predicts.jsx'));

/*Отчеты*/
const Reports = lazy(()=>import('./pages/Reports/Reports.jsx'));

/*Настройки*/
const Settings = lazy(()=>import('./pages/Settings/Settings.jsx'));
const Users = lazy(()=>import('./pages/Settings/Users/Users.jsx'));
const Categories = lazy(()=>import('./pages/Settings/Categories/Categories.jsx'));
const Notifications = lazy(()=>import('./pages/Settings/Notifications/Notifications.jsx'));

const ProfileForm = lazy(()=>import('./pages/ProfileForm.jsx')); // Страница пользователя

createRoot(document.getElementById('root')).render(
  <BrowserRouter>
    <AuthProvider>
      <Suspense fallback={<div>Подождите, идет загрузка...</div>}>
      <Routes>
        
        <Route path="/login" element={<Login />}></Route>
        <Route path="/reset-password" element={<ResetPassword />}></Route>

        <Route 
          path="/" 
          element={
            <ProtectedRoute>
              <Layout />
            </ProtectedRoute>
          }>

          <Route index element={<h2>Home</h2>}/>
          <Route path="leave" element={<h2>Выйти</h2>}/>
          <Route path="secret" element={<h2>Поздравляю вы вышли на секретную страницу</h2>}/>

          {/*Дашборд*/}
          <Route path="dashboard" element={<Dashboard />}/>
          <Route path="user" element={<ProfileForm />}/>

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
    </AuthProvider>
  </BrowserRouter>
);
