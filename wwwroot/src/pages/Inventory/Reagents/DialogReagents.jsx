import { useEffect, useState } from 'react';
import { 
  Dialog, DialogTitle, DialogContent,
  TextField, Grid, FormControl, InputLabel, 
  Select, MenuItem, FormControlLabel, Switch
} from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';
   
import { DataTableDialogActions, DataTableDialogLabel } from "../../../components/DataTable/DataTableDialogElements.jsx";
import { formatDate, dateConverter } from "../../../utils/formatDate.js";
import { getRecordsArray } from '../../../utils/getRecordsArray.js';

const deleteColumns = [
  { field: 'id',              headerName: 'ID',             width: 30 },
  { field: 'name',            headerName: 'Название',       width: 170, },
  { field: 'unit',            headerName: 'Ед. изм.',       width: 80, },
  { field: 'currentQuantity', headerName: 'Кол-во',         width: 80,  type: 'number', valueFormatter: (value) => `${value || 0}`, },
  { field: 'expirationDate',  headerName: 'Срок годности',  width: 140, type: 'date',   valueFormatter: (value) => formatDate(value, 'date'), },
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

const DialogReagents = ( { modalMode, currentRecord, categories, handleClose, handleSave, handleDelete, handleRestore, handleAdd } ) => {    
	const [formData, setFormData] = useState(getFormData());
	
  useEffect(() => {
    if (modalMode === 'add') {
        setFormData(getFormData());
    } else if (modalMode === 'edit' && currentRecord) {
        setFormData(getFormData(currentRecord));
    }
  }, [modalMode]);

  const handleChange = (field) => (event) => {
    setFormData({
        ...formData,
        [field]: event.target.value
    });
  };

  // Адаптеры
  const onAdd = () =>{
    handleAdd(formData);
  }

  const onSave = () =>{
    handleSave(formData);
  }

  return (        
    <Dialog open={modalMode !== null} onClose={handleClose} disableRestoreFocus>
      <DialogContent>
        {(modalMode === 'delete' || modalMode === 'restore' && currentRecord) 
          ? 
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
        )
          : 
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

              <Grid size={12}>
                <TextField
                  label="Место хранения"
                  fullWidth
                  value={formData.storageLocation}
                  onChange={handleChange('storageLocation')}
                />
              </Grid>

              <Grid>
                <FormControl>
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

              <Grid>

                <FormControlLabel
                  control={
                    <Switch
                      checked={formData.isActive}
                      onChange={(e) => setFormData({...formData, isActive: e.target.checked})}
                    />
                  }
                  label="Активен"
                />
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
        />

    </Dialog>
    );
};

export default DialogReagents;