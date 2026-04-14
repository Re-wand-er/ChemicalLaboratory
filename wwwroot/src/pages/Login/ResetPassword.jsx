import { useState } from 'react';
import { NavLink, Navigate, replace, useNavigate } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import { fetchPostData } from '../../api/fetch';

import './login.css';

export const ResetPassword = ({ login }) => {
  const [step, setStep] = useState(1); // 1: Email, 2: Code & New Password
  const [email, setEmail] = useState('');
  const [error, setError] = useState('');
  const [formData, setFormData] = useState({ code: '', password: '', confirm: '' });

  const navigate = useNavigate();

  const { loading, logout } = useAuth();

  const handleSendCode = async (e) => {
    e.preventDefault();
    await fetchPostData('/api/auth/email-access', { email }, false );
    setStep(2);
  };

  const handleReset = async (e) => {
    e.preventDefault();
    if (formData.password !== formData.confirm) { 
      setError("Пароли не совпадают"); 
      return;
    }

    const response =  await fetchPostData('/api/auth/reset-password', { email, ...formData }, false );

    if (!response.ok) {
      const errorData = await response.json(); 
      const errorMessage = errorData.message || 'Произошла ошибка';
    
      setError(errorMessage); 
      return; 
    }

    // Если всё Ok
    //navigate('/login');
    logout(); 
    // Перенаправляем на вход
    navigate('/login', { replace: true });
    alert("Пароль изменен. Пожалуйста, войдите снова.");
    navigate(-1, { replace: true });
  };

  const handleBack = (e) => {
    e.preventDefault(); // Останавливаем переход по "#"
    navigate(-1, { replace: true });
  }
  ///
  // Служит, чтобы экран не мигал на страницу логина
  if (loading) 
    return <div>Проверка авторизации...</div>; 

  // Редиректит если пользователь уже существует
  // if (isAuthenticated){
  //   return <Navigate to="/" replace />;
  // }
  //
  ///

  return (
    <div className="login-page">
      <div className='login-form'>

      {step === 1 ? (
        <form onSubmit={handleSendCode}>
          <h2>Подтверждение на почту</h2>
          <input type="email" required placeholder="Email" onChange={e => setEmail(e.target.value)}/>
          <button type='submit'>Отправить код</button>
        </form>
      ) : (
        <form onSubmit={handleReset} autoComplete="off">
          <h2>Сброс пароля</h2>
          {error && <div className="error">{error}</div>}
          {/* Поля обманки. Служать для автозаполнения браузера, которое нам не нужно */}
          {/* Вот до чего дошли, обманываем. Эти поля не отправляются на сервер */}
          <input type="text" name="username" style={{ display: 'none' }} autoComplete="username" />
          <input type="password" name="password" style={{ display: 'none' }} autoComplete="new-password" />

          {/* Реальные отображаемые поля, которые отправляются на сервер */}
          <input type="number" min={100000} max={999999} autoComplete="one-time-code" placeholder="6-значный код"    onChange={e => setFormData({...formData, code: e.target.value.toString()})} />
          <input type="password" autoComplete="new-password"  placeholder="Новый пароль"     onChange={e => setFormData({...formData, password: e.target.value})} />
          <input type="password" autoComplete="new-password"  placeholder="Повторите пароль" onChange={e => setFormData({...formData, confirm: e.target.value})} />
          <button type="submit">Сбросить пароль</button>
        </form>
      )}
      
      <p>
        <NavLink to="#" onClick={handleBack}>Вернуться обратно</NavLink>
      </p>

      </div>
    </div>
  );
};
