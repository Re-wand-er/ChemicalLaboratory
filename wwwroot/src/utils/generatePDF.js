/**
 * Создание PDF отчета
 * @param {*} title - Название
 * @param {*} muiColumns - Колонки в формате mui
 * @param {*} data - Данные
 */
export const generatePDF = async (title, muiColumns, data) => {
    const pdfMakeModule = await import("pdfmake/build/pdfmake");
    const pdfFontsModule = await import("pdfmake/build/vfs_fonts");

    const pdfMake = pdfMakeModule.default;
    pdfMake.vfs = pdfFontsModule.default.pdfMake ? pdfFontsModule.default.pdfMake.vfs : pdfFontsModule.default.vfs;

    pdfMake.fonts = {
        Roboto: {
            normal: 'Roboto-Regular.ttf',
            bold: 'Roboto-Medium.ttf',
            italics: 'Roboto-Italic.ttf',
            bolditalics: 'Roboto-MediumItalic.ttf'
        }
    };

    // ПРЕОБРАЗОВАНИЕ ДАННЫХ
    const tableBody = preparePdfData(muiColumns, data);

    const docDefinition = {
        content: [
            { text: title, style: 'header' },
            {
                table: {
                    headerRows: 1,
                    widths: muiColumns.map(col => col.flex ? '*' : 'auto'),
                    body: tableBody
                },
                layout: 'lightHorizontalLines'
            }
        ],
        styles: {
            header: { fontSize: 18, bold: true, margin: [0, 0, 0, 10] },
            tableHeader: { bold: true, fontSize: 12, fillColor: '#f5f5f5' }
        },
        defaultStyle: { font: 'Roboto' }
    };

    pdfMake.createPdf(docDefinition).download(`${title}_${new Date().getTime()}.pdf`);
};


const preparePdfData = (muiColumns, data) => {
    const headers = muiColumns
        .filter(col => col.field)
        .map(col => ({ text: col.headerName, style: 'tableHeader' }));

    // Формируем строки данных
    const rows = data.map(item => {
        return muiColumns
            .filter(col => col.field)
            .map(col => {
                const value = item[col.field];
                return value ?? '';
            });
    });

    return [headers, ...rows];
};

