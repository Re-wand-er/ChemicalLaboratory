import { useEffect, useState } from 'react';
import { 
  Dialog, DialogTitle, DialogContent, Box,
  TextField, Grid, FormControl, InputLabel, 
  Select, MenuItem, Typography, TableContainer,
  Table, Paper, TableHead, TableRow, TableCell, 
  TableBody, IconButton, FormHelperText
} from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';
   
import DeleteIcon from '@mui/icons-material/Delete';

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
    handleAdd, 
  
    onScanQr, onSaveQrIncome,

    rows
  }) => {   

	const [formData, setFormData] = useState(getFormData());
  const [errors, setErrors] = useState({});
  const [writeOffRows, setWriteOffRows] = useState([]);

  // Qr-действия
  const [uploadedFiles, setUploadedFiles] = useState([]);
	
  useEffect(() => {
    setErrors({});
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

  const handleOperationChange = (id, value) => {
    setWriteOffRows(prev =>
      prev.map(row =>
        row.id === id
          ? { ...row, operationType: value }
          : row
      )
    );
  };

  const validateForm = () => {
    const newErrors = {};
  
    // Валидация Названия (обязательно)
    if (!formData.name || !formData.name.trim()) {
      newErrors.name = "Название реагента обязательно для заполнения";
    }
  
    if (formData.name.length > 50) {
      newErrors.name = "Слишком длинное название для реагента";
    }

    if (formData.chemicalFormula === "") {
      newErrors.chemicalFormula = "Химическая формула не может быть пустой";
    }

    // Валидация Единиц измерения (обязательно)
    if (!formData.unit) {
      newErrors.unit = "Выберите единицу измерения";
    }
  
    // Валидация Текущего количества (не может быть отрицательным)
    if (formData.currentQuantity === "" || Number(formData.currentQuantity) < 0) {
      newErrors.currentQuantity = "Количество не может быть отрицательным";
    }
  
    // Валидация Минимального количества (не может быть отрицательным)
    if (formData.minQuantity === 0 || Number(formData.minQuantity) < 0) {
      newErrors.minQuantity = "Минимум не может быть 0 или меньше 0";
    }
  
    // Валидация Категории (обязательно, значение 0 считается не выбранным)
    if (!formData.categoryId || formData.categoryId === 0) {
      newErrors.categoryId = "Необходимо выбрать категорию";
    }
  
    setErrors(newErrors);
    
    // Возвращает true, если объект с ошибками пуст (форма валидна)
    return Object.keys(newErrors).length === 0;
  };

  // Адаптеры
  const onAdd = () =>{
    if (validateForm()) {      
      handleAdd(formData);
      return;
    }
  }
 
  const onSave = () => {
    if (modalMode === 'add' || modalMode === 'edit') {
      if (validateForm()) {      
        handleSave(formData);
        return;
      }
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
  
  // Qr
  const [qrChanges, setQrChanges] = useState([]);
  const [isScanning, setIsScanning] = useState(false);

  const resetQrForm = () => {
    setQrChanges([]);
    setUploadedFiles([]);
  };

  const triggerScan = () => {
    onScanQr(uploadedFiles, setQrChanges, setIsScanning);
  };

  const handleQuantityWriteOffChange = (id, newValue) => {
    setWriteOffRows(prev => 
      prev.map(item => item.id === id ? { ...item, quantity: Number(newValue) } : item)
    );
  };

  const handleQuantityQrChange = (id, newValue) => {
    setQrChanges(prev => 
      prev.map(item => item.id === id ? { ...item, quantity: Number(newValue) } : item)
    );
  };

  const triggerSave = () => {
    onSaveQrIncome(qrChanges, rows, resetQrForm, setIsScanning, handleClose);
  };
  // Qr

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
                    onChange={(e) => handleQuantityWriteOffChange(row.id, e.target.value)}
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
          <Box sx={{ p: 1}}>
            <Typography variant="h6" sx={{ fontWeight: 700, mb: 1, color: 'primary.main' }}>
              Пакетное зачисление реагентов по фото QR
            </Typography>
            
            {/* ШАГ 1: ЗОНА ЗАГРУЗКИ ФАЙЛОВ */}
            {qrChanges.length === 0 ? (
              <Box>
                <Typography variant="body2" color="text.secondary" sx={{ mb: 2 }}>
                  Загрузите фотографии этикеток с QR-кодами. АИС автоматически распознает идентификаторы реактивов и увеличит их текущий остаток на складе.
                </Typography>
                
                {/* Передаем функцию сохранения файлов в локальный стейт диалога */}
                <QrIncomeUploader 
                  onFilesSelected={setUploadedFiles} 
                  loading={isScanning} 
                />
              </Box>
            ) : (
              /* ШАГ 2: ИНТЕРАКТИВНАЯ ТАБЛИЦА СВЕРКИ */
              <Box>
                <Typography variant="body2" color="text.secondary" sx={{ mb: 2 }}>
                  Проверьте распознанные данные. Вы можете скорректировать добавляемое количество или удалить ошибочные позиции.
                </Typography>
            
                <TableContainer component={Paper} variant="outlined" sx={{ borderRadius: 2 }}>
                  <Table size="small" stickyHeader>
                    <TableHead>
                      <TableRow sx={{ '& th': { backgroundColor: 'background.default', fontWeight: 700 } }}>
                        <TableCell>Наименование</TableCell>
                        <TableCell align="right">Тек. остаток</TableCell>
                        <TableCell align="right">Мин. остаток</TableCell>
                        <TableCell sx={{ width: 140 }}>Добавить количество</TableCell>
                        <TableCell align="center">Ед. изм.</TableCell>
                        <TableCell align="center">Действие</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {qrChanges.map((changeItem) => {
                        // Находим справочные данные реагента в твоем известном глобальном массиве rows страницы Reagents.jsx
                        const reagentInfo = rows.find(r => r.id === changeItem.id);
                      
                        // Защита: если по QR пришел неизвестный ID, строку пропускаем
                        if (!reagentInfo) return null; 
                      
                        return (
                          <TableRow key={changeItem.id} hover>
                            <TableCell sx={{ fontWeight: 600 }}>{reagentInfo.name}</TableCell>
                            <TableCell align="right">{Number(reagentInfo.currentQuantity).toFixed(2)}</TableCell>
                            <TableCell align="right">{Number(reagentInfo.minQuantity).toFixed(2)}</TableCell>
                            <TableCell>
                              {/* Инпут с возможностью изменения количества из QR */}
                              <TextField
                                type="number"
                                size="small"
                                value={changeItem.quantity}
                                onChange={(e) => handleQuantityQrChange(changeItem.id, e.target.value)}
                                inputProps={{ min: 0, step: 0.1 }}
                                //disabled={isScanning}
                                fullWidth
                              />
                            </TableCell>
                            <TableCell align="center" sx={{ color: 'text.secondary' }}>{reagentInfo.unit}</TableCell>
                            <TableCell align="center">
                              {/* Кнопка удаления, если лаборант не хочет вносить этот реагент */}
                              <IconButton color="error" size="small" onClick={() => handleRemoveRow(changeItem.id)} disabled={isScanning}>
                                <DeleteIcon fontSize="small" />
                              </IconButton>
                            </TableCell>
                          </TableRow>
                        );
                      })}
                    </TableBody>
                  </Table>
                </TableContainer>
              </Box>
            )}
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
                  error={!!errors.name} 
                  helperText={errors.name}
                />
              </Grid>

              <Grid size={10}>
                <TextField
                  label="Хим. формула"
                  fullWidth
                  value={formData.chemicalFormula}
                  onChange={handleChange('chemicalFormula')}
                  error={!!errors.chemicalFormula} 
                  helperText={errors.chemicalFormula}
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
                  {errors.unit && <FormHelperText style={{color:'red'}}>{errors.unit}</FormHelperText>}
                </FormControl>
              </Grid>

              <Grid size={3}>
                <TextField
                  label="Текущее кол-во"
                  type="number"
                  fullWidth
                  value={formData.currentQuantity}
                  onChange={handleChange('currentQuantity')}
                  error={!!errors.currentQuantity}
                  helperText={errors.currentQuantity}
                />
              </Grid>

              <Grid size={3}>
                <TextField
                  label="Мин. кол-во"
                  type="number"
                  fullWidth
                  value={formData.minQuantity}
                  onChange={handleChange('minQuantity')}
                  error={!!errors.minQuantity}
                  helperText={errors.minQuantity}
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
                  {errors.categoryId && <FormHelperText style={{color:'red'}}>{errors.categoryId}</FormHelperText>}
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
        //handleQrIncome={handleConfirmQrIncome}

        qrChanges={qrChanges}
        setQrChanges={setQrChanges}
        isScanning={isScanning}
        hasFiles={uploadedFiles.length > 0}
        onScanClick={triggerScan}
        onSaveClick={triggerSave}
        />

    </Dialog>
    );
};

export default DialogReagents;