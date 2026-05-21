import { useEffect, useState } from 'react';
import { 
  Dialog, DialogTitle, DialogContent, Box,
  TextField, Grid, FormControl, InputLabel, 
  Select, MenuItem, Typography
} from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';
   

import QrIncomeUploader from '../../Inventory/Reagents/QrIncomeUploader.jsx';

import { DataTableDialogActions, DataTableDialogLabel } from "../../../components/DataTable/DataTableDialogElements.jsx";
import { formatDate, dateConverter } from "../../../utils/formatDate.js";
import { getRecordsArray } from '../../../utils/getRecordsArray.js';

const deleteColumns = [
  //{ field: 'id',              headerName: 'ID',             width: 30 },
  { field: 'name',            headerName: 'Название',       width: 170, },
  { field: 'currentQuantity', headerName: 'Кол-во',         width: 80,  type: 'number', valueFormatter: (value) => `${value || 0}`, },
  { field: 'unit',            headerName: 'Ед. изм.',       width: 80, },
  { field: 'expirationDate',  headerName: 'Срок годности',  width: 140, type: 'date',   valueFormatter: (value) => formatDate(value, 'date'), },
];

const orderColumns = [
  //{ field: 'id',              headerName: 'ID',             width: 30 },
  { field: 'name',            headerName: 'Название',       width: 170, },
  { field: 'currentQuantity', headerName: 'Кол-во',         width: 80,  type: 'number', valueFormatter: (value) => `${value || 0}`, },
  { field: 'minQuantity',     headerName: 'Мин. кол-во',    width: 100, type: 'number', valueFormatter: (value) => `${value || 0}`, },
  { field: 'unit',            headerName: 'Ед. изм.',       width: 80, },
  //{ field: 'expirationDate',  headerName: 'Срок годности',  width: 120, type: 'date',   valueFormatter: (value) => formatDate(value, 'date'), },
];

const units = ['мл', 'л', 'г', 'кг', 'мг', 'шт']; 

const getFormData = (record = {}) => ({
  id: record.id || null,
  name: record.name || '',
  chemicalFormula: record.chemicalFormula || '',
  unit: record.unit || '',
  currentQuantity: Number(record.currentQuantity) || 0,
  minQuantity: record.minQuantity || 0,
  expirationDate: record.expirationDate || null,
  storageLocation: record.storageLocation || '',
  categoryId: record.categoryId || 0,
  isActive: record.isActive !== undefined ? record.isActive : true
});

const DialogReagents = ({ 
    modalMode, currentRecord, categories, 
    handleClose, handleSave, handleDelete, 
    handleRestore, handleWriteOff, handleOrder, 
    handleQrIncome, handleAdd }) => {    
	const [formData, setFormData] = useState(getFormData());
  const [writeOffRows, setWriteOffRows] = useState([]);
  const [uploadedFiles, setUploadedFiles] = useState([]);
	
  useEffect(() => {
    if (modalMode === 'add') {
        setFormData(getFormData());
    } else if (modalMode === 'edit' && currentRecord) {
        setFormData(getFormData(currentRecord));
    }
  }, [modalMode]);

  useEffect(() => {
    if ((modalMode === 'writeOff' || modalMode === 'income' || modalMode === 'order') && currentRecord) {
      const rows = getRecordsArray(currentRecord).map(item => ({
        id: item.id,
        name: item.name,
        currentQuantity: item.currentQuantity,
        minQuantity: item.minQuantity,
        unit: item.unit,
        quantity: '',
        operationType: 'writeOff' 
      }));
  
      setWriteOffRows(rows);
    }
  }, [modalMode, currentRecord]);

  const handleChange = (field) => (event) => {
    setFormData({
        ...formData,
        [field]: event.target.value
    });
  };

  const handleWriteOffChange = (field) => (event) => {
    setFormData({
        ...writeOffRows,
        [field]: event.target.value
    });
  };

  const handleQuantityChange = (id, value) => {
    setWriteOffRows(prev =>
      prev.map(row =>
        row.id === id
          ? { ...row, quantity: value }
          : row
      )
    );
  };

  const handleOperationChange = (id, value) => {
    setWriteOffRows(prev =>
      prev.map(row =>
        row.id === id
          ? { ...row, operationType: value }
          : row
      )
    );
  };

  // Адаптеры
  const onAdd = () =>{
    handleAdd(formData);
  }
 
  const onSave = () => {
    if (modalMode === 'add' || modalMode === 'edit') {
      handleSave(formData);
      return;
    }
  
    if (modalMode === 'writeOff' || modalMode === 'income') {
      
      const hasChanges = writeOffRows.some(row => Number(row.quantity) > 0);
      if (!hasChanges) {
        return;
      }
  
      const originalRecords = getRecordsArray(currentRecord);
  
      const payload = originalRecords.map(item => {
        const rowUpdate = writeOffRows.find(row => row.id === item.id);
        const userEnteredQuantity = rowUpdate ? Number(rowUpdate.quantity || 0) : 0;
  
        let newQuantity = Number(item.currentQuantity || 0);
        
        if (modalMode === 'writeOff') {
          newQuantity -= userEnteredQuantity;
        } else if (modalMode === 'income') {
          newQuantity += userEnteredQuantity;
        }
  
        return {
          ...item,
          currentQuantity: newQuantity,
          chemicalFormula: item.chemicalFormula || null,
          storageLocation: item.storageLocation || null,
          expirationDate: item.expirationDate || null,
          isActive: item.isActive ?? true 
        };
      });

      handleWriteOff(payload); 
    }
  };
  
  const onOrder = () => {
    const changedRows = writeOffRows.filter(row => Number(row.quantity) > 0);
    
    if (changedRows.length === 0) {
      return; 
    }
  
    const payload = changedRows.map(row => ({
      id: row.id,
      quantity: Number(row.quantity)
    }));
  
    handleOrder(payload); 
  };
  
  const handleConfirmQrIncome = async () => {
    if (uploadedFiles.length === 0) {
      alert("Пожалуйста, выберите хотя бы одну фотографию с QR-кодом.");
      return;
    }
  
    //setIsLoading(true); // Включаем крутилку MUI (CircularProgress)
    try {
      await handleQrIncome(uploadedFiles);
      
      setUploadedFiles([]); 
    } finally {
      //setIsLoading(false); // Выключаем крутилку
    }
  };

  return (        
    <Dialog open={modalMode !== null} onClose={handleClose} disableRestoreFocus>
      <DialogContent>
        {(modalMode === 'writeOff' || modalMode === 'income' || modalMode === 'order' && currentRecord) && (
          <Box sx={{ width: '100%' }}>
            <DialogTitle sx={{ textAlign: 'center', mb: 1, fontSize: 32, fontWeight: 'bold'}}>
              {modalMode === 'writeOff' ? 'Списание' : modalMode === 'income' ? 'Внесение' : 'Заказ реагента'}
            </DialogTitle>
        
            <Box sx={{ 
              display: 'flex', 
              alignItems: 'center', 
              pb: 1, 
              mb: 2, 
              borderBottom: '1px solid #e0e0e0' 
            }}>
              <Box sx={{ flex: 1, pr: 1 }}>
                <Typography variant="subtitle2" sx={{ fontWeight: 'bold' }}>Наименование</Typography>
              </Box>
          
              {modalMode === 'order' && (
                <>
                  <Box sx={{ width: 85, pr: 1, textAlign: 'center' }}>
                    <Typography variant="subtitle2" sx={{ fontWeight: 'bold', lineHeight: 1.2 }}>Текущий остаток</Typography>
                  </Box>
                  <Box sx={{ width: 85, pr: 1, textAlign: 'center' }}>
                    <Typography variant="subtitle2" sx={{ fontWeight: 'bold', lineHeight: 1.2 }}>Мин. остаток</Typography>
                  </Box>
                </>
              )}

              <Box sx={{ width: 100, pr: 2 }}>
                <Typography variant="subtitle2" sx={{ fontWeight: 'bold' }}>Количество</Typography>
              </Box>
            
              <Box sx={{ width: 50, pr: 1 }}>
                <Typography variant="subtitle2" sx={{ fontWeight: 'bold' }}>Ед. изм.</Typography>
              </Box>
            
              {/* Тип операции только для Списания */}
              {modalMode === 'writeOff' && (
                <Box sx={{ width: 140 }}>
                  <Typography variant="subtitle2" sx={{ fontWeight: 'bold' }}>Тип операции</Typography>
                </Box>
              )}
            </Box>
            
            {writeOffRows.map(row => (
              <Box key={row.id} sx={{ display: 'flex', alignItems: 'center', mb: 2 }}>

                <Box sx={{ flex: 1, pr: 1, overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap' }}>
                  <Typography title={row.name}>{row.name}</Typography>
                </Box>
            
                {modalMode === 'order' && (
                  <>
                    <Box sx={{ width: 85, pr: 1, textAlign: 'center' }}>
                      <Typography>{row.currentQuantity}</Typography>
                    </Box>
                    <Box sx={{ width: 85, pr: 1, textAlign: 'center' }}>
                      <Typography>{row.minQuantity}</Typography>
                    </Box>
                  </>
                )}

                <Box sx={{ width: 100, pr: 2 }}>
                  <TextField
                    type="number"
                    size="small"
                    fullWidth
                    value={row.quantity}
                    onChange={(e) => handleQuantityChange(row.id, e.target.value)}
                  />
                </Box>
              
                <Box sx={{ width: 50, pr: 1 }}>
                  <Typography>{row.unit}</Typography>
                </Box>
              
                {modalMode === 'writeOff' && (
                  <Box sx={{ width: 140 }}>
                    <FormControl fullWidth size="small">
                      <Select
                        value={row.operationType || 'writeOff'}
                        onChange={(e) => handleOperationChange(row.id, e.target.value)}
                      >
                        <MenuItem value="writeOff">Списание</MenuItem>
                        <MenuItem value="correction">Корректировка</MenuItem>
                      </Select>
                    </FormControl>
                  </Box>
                )}
              </Box>
            ))}
          </Box>
        )}


        {(modalMode === 'delete' || modalMode === 'restore' && currentRecord) 
          && 
        (
          <>
            <DataTableDialogLabel
              modalMode={modalMode}
              size={currentRecord.size}
              deleteOne={`Удалить реагент: ${currentRecord.name}`}
              deleteMany="Удалить группу реагентов?"
              restoreOne={`Восстановить категорию: ${currentRecord.name}`}
              restoreMany="Восстановить группу реагентов?"
            />

            <DataGrid 
                rows={getRecordsArray(currentRecord)}
                columns={deleteColumns}
                autoHeight
                hideFooter
                disableRowSelectionOnClick
                disableColumnMenu
                rowHeight={28}
                columnHeaderHeight={44}
            />
            
          </>
        )}


        {modalMode === 'qrIncome' && (
          <Box sx={{ p: 1 }}>
            <Typography variant="subtitle1" sx={{ fontWeight: 700, mb: 1, color: 'primary.main' }}>
              Пакетное зачисление реагентов по фото QR
            </Typography>
            <Typography variant="body2" color="text.secondary" sx={{ mb: 2 }}>
              Загрузите фотографии этикеток с QR-кодами. АИС автоматически распознает идентификаторы реактивов и увеличит их текущий остаток на складе.
            </Typography>
        
            <QrIncomeUploader 
              onFilesSelected={setUploadedFiles} 
            />
          </Box>
        )}

        {( modalMode === 'add' || modalMode === 'edit' && currentRecord)
          &&
        (
          <>
            <DialogTitle children={modalMode === 'add' ? 'Добавить реагент' : `Изменить реагент`} />

            <Grid container spacing={2} size={{ mt: 0 }}>
              <Grid size={12}>
                <TextField
                  autoFocus
                  label="Название"
                  fullWidth
                  value={formData.name}
                  onChange={handleChange('name')}
                />
              </Grid>

              <Grid size={10}>
                <TextField
                  label="Хим. формула"
                  fullWidth
                  value={formData.chemicalFormula}
                  onChange={handleChange('chemicalFormula')}
                />
              </Grid>

              <Grid size={2}>
                <FormControl fullWidth>
                  <InputLabel>Ед. измерения</InputLabel>
                  <Select
                    value={formData.unit}
                    label="Ед. измерения"
                    onChange={handleChange('unit')}
                    >
                      {units.map(unit => (
                        <MenuItem key={unit} value={unit}>{unit}</MenuItem>
                      ))}
                  </Select>
                </FormControl>
              </Grid>

              <Grid size={3}>
                <TextField
                  label="Текущее кол-во"
                  type="number"
                  fullWidth
                  value={formData.currentQuantity}
                  onChange={handleChange('currentQuantity')}
                />
              </Grid>

              <Grid size={3}>
                <TextField
                  label="Мин. кол-во"
                  type="number"
                  fullWidth
                  value={formData.minQuantity}
                  onChange={handleChange('minQuantity')}
                />
              </Grid>    

              <Grid size={6}>
                <TextField
                  label="Срок годности"
                  type="date"
                  fullWidth
                  value={dateConverter(formData.expirationDate)}
                  onChange={handleChange('expirationDate')}
                  InputLabelProps={{ shrink: true }}
                />
              </Grid>

              <Grid size={8}>
                <TextField
                  label="Место хранения"
                  fullWidth
                  value={formData.storageLocation}
                  onChange={handleChange('storageLocation')}
                />
              </Grid>

              <Grid size={4}>
                <FormControl fullWidth>
                  <InputLabel>Категория</InputLabel>
                  <Select
                    value={formData.categoryId}
                    label="Категория"
                    onChange={handleChange('categoryId')}
                    >
                    <MenuItem value={0}>Не выбрано</MenuItem>
                    {categories.map(cat => (
                      <MenuItem key={cat.id} value={cat.id}>{cat.name}</MenuItem>
                    ))}

                  </Select>
                </FormControl>
              </Grid>
            </Grid>

          </>
        )}

      </DialogContent>
              
      <DataTableDialogActions 
        modalMode={modalMode}
        handleAdd={onAdd}
        handleDelete={handleDelete}
        handleRestore={handleRestore}
        handleSave={onSave} 
        handleClose={handleClose}
        handleWriteOff={onSave} //handleWriteOff
        handleIncome={onSave} //handleIncome
        handleOrder={onOrder}
        handleQrIncome={handleConfirmQrIncome}
        />

    </Dialog>
    );
};

export default DialogReagents;