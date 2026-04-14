/**
 * Отправление Get запроса на сервер
 * @param {string} path - Путь запроса
 * @param {function} setData - Метод, обновляющий состояние объекта (Не обязателен)
 * @returns {Array} возвращает массив записей
 */
const fetchGetData = async (path, setData) => {
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
 * Отправление Post запроса на сервер
 * @param {string} path - Путь запроса
 * @param {object} body - Отправляемый json-объект
 * @param {boolean} credentialsInclude - Подтверждать ли пользователя
 */
const fetchPostData = async (path, body, credentialsInclude = false) => {
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
async function fetchDeleteByIds(path, ids) {
  try {
    const response = await fetch(path, {
      method: 'POST',
      headers: {
          'Content-Type': 'application/json'
        },
      body: JSON.stringify({ ids: ids }),
      credentials: 'include'
    });

    if (response.ok) {
      //console.log('Удаление выполнено успешно');
      return true;
    } else {
      const error = await response.text();
      console.error('Ошибка сервера:', error);
      return false;
    }
  } catch (error) {
    console.error('Ошибка сети:', error);
    return false;
  }
}

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

export { fetchGetData, fetchPostData, fetchDeleteByIds, fetchPutData };
