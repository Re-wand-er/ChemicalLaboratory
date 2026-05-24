// Вспомогательный метод для подготовки данных (только первые 10 строк)
const prepareTableBody = (columns, data) => {
  const body = [];
  
  // 1. Создаем шапку таблицы
  const headerRow = columns.map(col => ({
    text: col.headerName || col.field,
    style: 'tableHeader'
  }));
  body.push(headerRow);

  // 2. Берем только первые 10 строк
  const limitedData = data.slice(0, 10);

  // 3. Заполняем строки данными
  limitedData.forEach(row => {
    const dataRow = columns.map(col => {
      const val = row[col.field];
      return {
        text: val !== undefined && val !== null ? String(val) : '',
        style: typeof val === 'number' ? 'tableCellNumber' : 'tableCell'
      };
    });
    body.push(dataRow);
  });

  return body;
};

// ОСНОВНОЙ МЕТОД ГЕНЕРАЦИИ СТРУКТУРЫ
export const buildPdfContent = (title, columns, data, filters) => {
  const tableBody = prepareTableBody(columns, data);

  // Определение документа (docDefinition) в стиле ERP
  return {
    pageSize: 'A4',
    pageOrientation: 'portrait',
    pageMargins: [40, 40, 40, 50], // Оставляем место под футер
    
    content: [
      // КОРПОРАТИВНЫЙ ЗАГОЛОВОК
      {
        columns: [
          { text: title || 'АНАЛИТИЧЕСКИЙ ОТЧЕТ', style: 'reportTitle' },
          { text: 'ERP-СИСТЕМА', style: 'systemLabel', alignment: 'right' }
        ]
      },
      { 
        canvas: [{ type: 'line', x1: 0, y1: 5, x2: 515, y2: 5, lineWidth: 1.5, lineColor: '#1976d2' }],
        margin: [0, 0, 0, 15] 
      },

      // ИНФОРМАЦИОННЫЙ БЛОК И ФИЛЬТРЫ
      {
        columns: [
          {
            width: '50%',
            text: [
              { text: 'Дата формирования: ', bold: true },
              `${new Date().toLocaleDateString()} ${new Date().toLocaleTimeString().slice(0, 5)}\n`,
              { text: 'Статус данных: ', bold: true },
              'Актуален (Выборка ограничена: 10 стр.)'
            ],
            style: 'metaText'
          },
          {
            width: '50%',
            stack: [
              { text: 'ПРИМЕНЕННЫЕ ФИЛЬТРЫ:', bold: true, margin: [0, 0, 0, 5] },
              { text: `Период: с ${filters?.dateFrom || '—'} по ${filters?.dateTo || '—'}` },
              { text: `Категория ID: ${filters?.categoryId || 'Все'}` },
              { text: `Реагент ID: ${filters?.reagentId || 'Все'}` },
              { text: `Мин. кол-во: ${filters?.minQuantity || 'Не указано'}` }
            ],
            style: 'filterText',
            alignment: 'right'
          }
        ],
        margin: [0, 0, 0, 25]
      },

      // ТАБЛИЦА С ДАННЫМИ
      {
        table: {
          headerRows: 1,
          widths: columns.map(col => col.flex ? '*' : 'auto'),
          body: tableBody
        },
        layout: {
          fillColor: function (rowIndex) {
            if (rowIndex === 0) return '#1976d2'; // MUI Primary Blue для шапки
            return (rowIndex % 2 === 0) ? '#f9f9f9' : null; // Зебра для строк
          },
          hLineColor: function () { return '#e0e0e0'; },
          vLineColor: function () { return '#e0e0e0'; },
          paddingLeft: function() { return 8; },
          paddingRight: function() { return 8; },
          paddingTop: function() { return 6; },
          paddingBottom: function() { return 6; }
        }
      }
    ],

    // ПРОФЕССИОНАЛЬНЫЙ НУМЕРАТОР СТРАНИЦ
    footer: function(currentPage, pageCount) {
      return {
        text: `Страница ${currentPage} из ${pageCount}`,
        alignment: 'center',
        style: 'footerText',
        margin: [0, 20, 0, 0]
      };
    },

    // СТИЛИ КОМПОНЕНТОВ
    styles: {
      reportTitle: { fontSize: 20, bold: true, color: '#1a237e' }, // Темно-синий ERP цвет
      systemLabel: { fontSize: 10, bold: true, color: '#9e9e9e', tracking: 1 },
      metaText: { fontSize: 9, color: '#424242', lineHeight: 1.4 },
      filterText: { fontSize: 9, color: '#616161', lineHeight: 1.3 },
      tableHeader: { bold: true, fontSize: 10, color: '#ffffff', alignment: 'left' },
      tableCell: { fontSize: 9, color: '#212121' },
      tableCellNumber: { fontSize: 9, color: '#212121', alignment: 'right' },
      footerText: { fontSize: 9, color: '#9e9e9e' }
    },
    defaultStyle: { font: 'Roboto' }
  };
};
