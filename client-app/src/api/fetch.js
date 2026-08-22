/**
 * Отправление Get запроса на сервер
 * @param {string} path - Путь запроса
 * @param {function} setData - Метод, обновляющий состояние объекта (Не обязателен)
 * @returns {Array} возвращает массив записей
 */
const fetchGetData = async (path, setData ) => {
    try {
      const response = await fetch(path, {
        credentials: 'include'
      });

      if (!response.ok) {
        throw new Error(`Ошибка: ${response.status}`);
      }

      const result = await response.json();

      // Если передали функцию setData — вызываем её
      if (typeof setData === 'function') {
        setData(result);
      }
      
      // В любом случае возвращаем результат для использования в Promise.all
      return result;

    } catch (err) {
      console.error("Ошибка загрузки:", err.message);
      
      if (typeof setData === 'function') {
        setData([]);
      }
      return []; // Возвращаем пустой массив при ошибке
    }
};

/**
 * Отправление Get запроса на сервер для получения данных с ROLES.SUPER_ADMIN
 * @param {string} path - Путь запроса
 * @param {boolean} accessRole - Роль, получается из кастомного хука useAuth();
 * @param {function} setData - Метод, обновляющий состояние объекта (Не обязателен)
 * @returns {Array} возвращает массив записей
 */
const fetchGetSuperAdminData = async (baseUrl, accessRole = false, setData) =>{
  const urlParams = new URLSearchParams();
  
  if (accessRole) {
    urlParams.append("includeInactive", "true");
  }

  return await fetchGetData(`${baseUrl}?${urlParams.toString()}`, setData)
}

/**
 * Отправление Post запроса на сервер
 * @param {string} path - Путь запроса
 * @param {object} body - Отправляемый json-объект
 * @param {boolean} credentialsInclude - Подтверждать ли пользователя
 */
const fetchPostData = async (path, body, credentialsInclude = true) => {
  const options = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(body)
  };

  if (credentialsInclude) {
    options.credentials = 'include';
  }

  const response = await fetch(path, options);

  return response;
};

/**
 * Отправление Post запроса на массовое удаление записей по ID 
 * @param {string} path - Путь запроса
 * @param {Array} ids - Массив id для удаления, например [1,5,12]
 * @returns {boolean} - возвращает успешность запроса на удаление
 */
const fetchDeleteByIds = async (path, ids) => postBulk(path, ids);
/**
 * Отправление Post запроса на массовое восстановление записей по ID 
 * @param {string} path - Путь запроса
 * @param {Array} ids - Массив id для удаления, например [1,5,12]
 * @returns {boolean} - возвращает успешность запроса на удаление
 */
const fetchRestoreByIds = async (path, ids) => await postBulk(path, ids);

/**
 * Отправление Put запроса на сервер (обновление данных)
 * @param {string} path - Путь запроса
 * @param {object} body - Отправляемый json-объект
 * @param {boolean} credentialsInclude - Подтверждать ли пользователя
 * @returns {boolean} - возвращает успешность запроса на удаление
 */
const fetchPutData = async (path, body, credentialsInclude = false) => {
  const options = {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(body)
  };

  if (credentialsInclude) {
    options.credentials = 'include';
  }

  try {
    const response = await fetch(path, options);

    if (!response.ok) {
      throw new Error(`Ошибка: ${response.status}`);
    }

    return await response.json();

  } catch (err) {
    console.error("Ошибка PUT-запроса:", err.message);        
    throw err;
  }
};

async function postBulk(path, ids) {
  const response = await fetchPostData(path, { ids }, true);
  if (response.ok) return true;
  
  const error = await response.text();
  console.error('Ошибка:', error);
  return false;
}

export { fetchGetData, fetchGetSuperAdminData, fetchPostData, fetchDeleteByIds, fetchRestoreByIds, fetchPutData };
