/**
 * Преобразует ISO строку в читаемый формат
 * @param {string} isoString - Дата в формате "YYYY-MM-DDTHH:MM:SS" (T - разделитель)
 * @param {string} format - Формат вывода: 'date' | 'time' | 'datetime' | 'datetime-full'
 * @returns {string} - Отформатированная дата
 */
const formatDate = (isoString, format = 'datetime') => {
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

/**
 * Преобразует дату в формат YYYY.MM.DD
 * @param {Date} date - Дата в формате "YYYY-MM-DDTHH:MM:SS" (T - разделитель)
 * @returns {Date} - Отформатированная дата
 */
const dateConverter = (date) => {
  if (!date) return '';
  const d = new Date(date);
  if (isNaN(d.getTime())) return '';

  // Получаем компоненты даты именно местного времени
  const year = d.getFullYear();
  const month = String(d.getMonth() + 1).padStart(2, '0');
  const day = String(d.getDate()).padStart(2, '0');

  return `${year}-${month}-${day}`;
}

export { formatDate, dateConverter };