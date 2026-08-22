import { Navigate, useLocation } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

export const ProtectedRoute = (props) => {
  const { isAuthenticated, loading } = useAuth();
  const location = useLocation();

  // Показываем загрузку, пока проверяем авторизацию
  if (loading) {
    return <div>Проверка авторизации...</div>;
  }

  // Если не авторизован — редирект на логин с сохранением пути
  if (!isAuthenticated) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  // Если авторизован — показываем контент
  return props.children;
};