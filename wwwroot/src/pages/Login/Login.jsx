import { useState } from 'react';
import { useNavigate, useLocation, NavLink, Navigate } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import { fetchPostData } from '../../api/fetch';

import './login.css';


export const Login = () => {
  const [credentials, setCredentials] = useState({ login: '', password: '' });
  const [error, setError] = useState('');
  
  const { login, loading, isAuthenticated } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();

  // Куда вернуться после входа (по умолчанию главная)
  const from = location.state?.from?.pathname || '/';

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError('');

    try {
      const response = await fetchPostData('api/auth/login', credentials, true);

      if (!response.ok) 
        throw new Error('Неверный логин или пароль');

      const data = await response.json();
      
      // Сохраняем пользователя и токен
      login(data.user);
      
      // Редирект на страницу, куда пытались зайти
      navigate(from, { replace: true });
      
    } catch (err) {
      setError(err.message || 'Ошибка авторизации');
    }
  };

  ///
  // Служит, чтобы экран не мигал на страницу логина
  if (loading) 
    return <div>Проверка авторизации...</div>; 

  // Редиректит если пользователь уже существует
  if (isAuthenticated){
    return <Navigate to={from} replace />;
  }
  //
  ///

  return (
    <div className="login-page">
      <form onSubmit={handleSubmit} className="login-form">
        <h2>Вход в систему</h2>
        
        {error && <div className="error">{error}</div>}
        
        <input
          type="text"
          placeholder="Логин"
          value={credentials.login}
          onChange={(e) => setCredentials({...credentials, login: e.target.value})}
          required
        />
        
        <input
          type="password"
          placeholder="Пароль"
          value={credentials.password}
          onChange={(e) => setCredentials({...credentials, password: e.target.value})}
          required
        />
        
        <button type="submit">Войти</button>
        
        <p>
          <NavLink to="/reset-password">Забыли пароль?</NavLink>
        </p>
      </form>
    </div>
  );
};