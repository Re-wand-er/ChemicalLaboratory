/**
 * Преобразует запись в массив
 * @param {object} currentRecord - Данные в формате объекта, массива или Map
 * @returns {Array} - Отформатированная данные в виде массива
 */
export const getRecordsArray = (currentRecord) => {
  if (!currentRecord) 					    return [];
  if (Array.isArray(currentRecord)) 		return currentRecord;
  if (currentRecord instanceof Map) 		return Array.from(currentRecord.values());
  if (typeof currentRecord === 'object')    return [currentRecord];
  return [];
};  