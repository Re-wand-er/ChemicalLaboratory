const exportJsonFile = (fileName, rows) =>{
  const jsonString = JSON.stringify(rows, null, 2);
		
	const blob = new Blob([jsonString], { type: "application/json" });

	const url = URL.createObjectURL(blob);
	
	const link = document.createElement("a");
	link.href = url;
	link.download = `${fileName || "data"}.json`;
	
	document.body.appendChild(link);
	link.click();
	document.body.removeChild(link);
	URL.revokeObjectURL(url);
}

const exportCsvFile = (fileName, rows) => {
  if (!rows || !rows.length) return;

  const separator = ';'; // стандартный разделитель "," 
  const keys = Object.keys(rows[0]);

  const csvContent = [
    keys.join(separator),
    ...rows.map(row => 
      keys.map(fieldName => JSON.stringify(row[fieldName] ?? '')).join(separator)
    )
  ].join('\r\n');

  
  const bom = '\uFEFF'; 
  const blob = new Blob([bom + csvContent], { type: 'text/csv;charset=utf-8;' });// Создаем Blob и ссылку для скачивания
  const link = document.createElement("a");
  
  if (link.download !== undefined) {
    const url = URL.createObjectURL(blob);
    link.setAttribute("href", url);
    link.setAttribute("download", fileName);
    link.style.visibility = 'hidden';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
    URL.revokeObjectURL(url); 
  }
}

export {exportCsvFile, exportJsonFile};