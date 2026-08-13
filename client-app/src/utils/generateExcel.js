/**
 * Строит таблицу в Excel
 * @param {*} fileName - Имя файла
 * @param {*} columns - Формат колонок в mui (преобразовывается в подх. формат)
 * @param {*} data - Данные
 * @param {*} worksheetName - Название листа 
 */
export const generateExcel = async (fileName, columns, data, worksheetName = 'Лист1',) => {
  // Загружаем библиотеки только внутри функции
  const ExcelJS = await import('exceljs');
  const { saveAs } = await import('file-saver');

  const workbook = new ExcelJS.Workbook();
  const worksheet = workbook.addWorksheet(worksheetName);

  worksheet.columns = columns
    .filter(col => col.field)
    .map(col => ({
      header: col.headerName,
      key: col.field,
      width: col.width ? Math.round(col.width / 7) : 20
    }));

  worksheet.getRow(1).font = { bold: true };
  worksheet.addRows(data);
  
  const buffer = await workbook.xlsx.writeBuffer();
  saveAs(new Blob([buffer]), `${fileName}_${new Date().getTime()}.xlsx`);
};
