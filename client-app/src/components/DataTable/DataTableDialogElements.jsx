import { DialogActions, DialogTitle, Button, CircularProgress } from '@mui/material';

const DataTableDialogActions = (props) => {
  return (
    <DialogActions sx={{ px: 3, pb: 2, gap: 1 }}>
      {props.modalMode !== 'qrIncome' && (
        <Button onClick={props.handleClose}>Отмена</Button>
      )}
                
      {props.modalMode === 'add' && <Button onClick={props.handleAdd} variant="contained">Создать</Button>}
      {props.modalMode === 'delete' && <Button onClick={props.handleDelete} variant="contained" color="error">Удалить</Button>}
      {props.modalMode === 'restore' && <Button onClick={props.handleRestore} variant="contained" color="success">Восстановить</Button>}
      {props.modalMode === 'edit' && <Button onClick={props.handleSave} variant="contained">Изменить</Button>}
      {props.modalMode === 'writeOff' && <Button onClick={props.handleWriteOff} variant="contained" color="warning">Списать</Button>}
      {props.modalMode === 'income' && <Button onClick={props.handleIncome} variant="contained" color="success">Внести</Button>}
      {props.modalMode === 'order' && <Button onClick={props.handleOrder} variant="contained" color="secondary">Заказать</Button>}

      {props.modalMode === 'qrIncome' && (
        <>
          <Button 
            onClick={props.qrChanges?.length > 0 ? () => props.setQrChanges([]) : props.handleClose} 
            variant="outlined" 
            color="inherit"
            size="small"
            disabled={props.isScanning}
            sx={{ textTransform: 'none', fontWeight: 600 }}
          >
            {props.qrChanges?.length > 0 ? 'Назад' : 'Отмена'}
          </Button>

          {/* ШАГ 1: Кнопка распознавания */}
          {props.qrChanges?.length === 0 && (
            <Button
              onClick={props.onScanClick} 
              variant="contained"
              color="primary"
              size="small"
              disabled={props.isScanning || !props.hasFiles}
              startIcon={props.isScanning && <CircularProgress size={16} color="inherit" />}
              sx={{ textTransform: 'none', fontWeight: 600 }}
            >
              {props.isScanning ? 'Распознавание...' : 'Распознать фото'}
            </Button>
          )}
        
          {/* ШАГ 2: Кнопка финальной отправки пакета на склад */}
          {props.qrChanges?.length > 0 && (
            <Button
              onClick={props.onSaveClick} 
              variant="contained"
              color="success" 
              size="small"
              disabled={props.isScanning}
              sx={{ textTransform: 'none', fontWeight: 600 }}
            >
              Добавить на склад
            </Button>
          )}
        </>
      )}
    </DialogActions>
  );
};


const DataTableDialogLabel = ({modalMode, size, deleteOne, deleteMany, restoreOne, restoreMany}) =>{
	const getTitle = () => {
		if(modalMode === 'delete'){
			return size > 1 ? deleteMany : deleteOne;
		}
		if (modalMode === 'restore') {
      return size > 1 ? restoreMany : restoreOne;
    }
    if (modalMode === 'order') {
      return size > 1 ? deleteMany : deleteOne;
    }
    return '';
	}

  return (
    <DialogTitle>
      {getTitle()}
    </DialogTitle>
  );
};

export { DataTableDialogActions, DataTableDialogLabel };