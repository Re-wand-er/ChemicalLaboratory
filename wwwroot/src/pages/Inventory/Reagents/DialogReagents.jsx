import { useEffect, useState } from 'react';
import { 
  Dialog, DialogTitle, DialogContent, DialogActions, Button, 
  TextField, Typography, Grid, FormControl, InputLabel, 
  Select, MenuItem, FormControlLabel, Switch
} from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';
   
import DataTableDialogActions from "../../../components/DataTable/DataTableDialogActions.jsx";
import { formatDate } from "../../../utils/formatDate.js";
import { getRecordsArray } from '../../../utils/getRecordsArray.js';

const deleteColumns = [
  { field: 'id',              headerName: 'ID',             width: 30,  type: 'number', },
  { field: 'name',            headerName: 'Название',       width: 170, },
  { field: 'unit',            headerName: 'Ед. изм.',       width: 80, },
  { field: 'currentQuantity', headerName: 'Кол-во',         width: 80,  type: 'number', valueFormatter: (value) => `${value || 0}`, },
  { field: 'expirationDate',  headerName: 'Срок годности',  width: 140, type: 'date',   valueFormatter: (value) => formatDate(value, 'date'), },
];
const defaultFormData = {
  name: '',
  chemicalFormula: '',
  unit: '',
  currentQuantity: 0,
  minQuantity: 0,
  expirationDate: '',
  storageLocation: '',
  categoryId: '',
  isActive: true
};
const getFormDataFromRecord = (record) => ({
  name: record.name || '',
  chemicalFormula: record.chemicalFormula || '',
  unit: record.unit || '',
  currentQuantity: record.currentQuantity || 0,
  minQuantity: record.minQuantity || 0,
  expirationDate: record.expirationDate ? formatDate(record.expirationDate) : '',
  storageLocation: record.storageLocation || '',
  categoryId: record.categoryId || '',
  isActive: record.isActive !== undefined ? record.isActive : true
});

const units = ['мл', 'л', 'г', 'кг', 'мг', 'шт']; // Не должно быть так
const categories = [
        { id: 1, name: 'Кислоты' },
        { id: 2, name: 'Щелочи' },
        { id: 3, name: 'Металлы и неметаллы' }
    ];

const DialogReagents = ( { modalMode, currentRecord, handleClose, handleSave, handleDelete } ) => {    
	const [formData, setFormData] = useState({defaultFormData});
	
  useEffect(() => {
    if (modalMode === 'add') {
        setFormData({defaultFormData});
    } else if (modalMode === 'edit' && currentRecord) {
        setFormData(getFormDataFromRecord(currentRecord));
    }
  }, [modalMode, open]);

    const handleChange = (field) => (event) => {
        setFormData({
            ...formData,
            [field]: event.target.value
        });
    };

  return (        
    <Dialog open={modalMode !== null} onClose={handleClose} >
      <DialogContent>
        {(modalMode === 'delete' && currentRecord) 
          ? 
        (
          <>
            <DialogTitle>
              {currentRecord.size > 1 ? 'Удалить группу реагентов?' : `Удалить реагент: ${currentRecord.name}?`}
            </DialogTitle>

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
            <DialogTitle>
              Добавить реагент
            </DialogTitle>

            <Grid container spacing={2} sx={{ mt: 0 }}>
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
                  value={formData.expirationDate}
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
                    <MenuItem value="">Не выбрано</MenuItem>
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
        handleSave={handleSave} 
        handleDelete={handleDelete}
        handleClose={handleClose}
        />

    </Dialog>
    );
};

export default DialogReagents;