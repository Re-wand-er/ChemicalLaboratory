/**
 * Преобразует ISO строку в читаемый формат
 * @param {string} isoString - Дата в формате "2026-01-17T18:11:44.51"
 * @param {string} format - Формат вывода: 'date' | 'time' | 'datetime' | 'datetime-full'
 * @returns {string} - Отформатированная дата
 */
export const formatDate = (isoString, format = 'datetime') => {
  if (!isoString) return '—';
  
  const date = new Date(isoString);

  if (isNaN(date.getTime())) return 'Некорректная дата';

  const options = {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
  };

  switch (format) {
    case 'date':
      // Только день.месяц.год
      return date.toLocaleString('ru-RU', options);
    
    case 'time':
      // Только часы:минуты:секунды
      return date.toLocaleString('ru-RU', {
        second: '2-digit',
        minute: '2-digit',
        hour: '2-digit',
      });
    
    case 'datetime':
      // День.месяц.год часы:минуты
      return date.toLocaleString('ru-RU', {
        ...options,
        minute: '2-digit',
        hour: '2-digit',
      });
    
    case 'datetime-full':
      // День.месяц.год часы:минуты:секунды
      return date.toLocaleString('ru-RU', {
        ...options,
        second: '2-digit',
        minute: '2-digit',
        hour: '2-digit',
      });
    
    default:
      return date.toLocaleString('ru-RU', {
        ...options,
        minute: '2-digit',
        hour: '2-digit',
      });
  }
};