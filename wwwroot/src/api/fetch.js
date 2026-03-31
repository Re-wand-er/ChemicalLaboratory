/**
 * Отправление Get запроса на сервер
 * @param {string} path - Путь запроса
 * @param {function} setData - Метод, обновляющий состояние объекта
 */
const fetchGetData = async (path, setData) => {
    try {
        const response = await fetch(path, {
            credentials: 'include'
        });

        if (!response.ok) {
            // Если пришла ошибка (например, 401 или 500)
            throw new Error(`Ошибка: ${response.status}`);
        }

        const result = await response.json();
        setData(result); 
    } catch (err) {
        console.error("Ошибка загрузки:", err.message);
        setData([]); 
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


export { fetchGetData, fetchPostData };
