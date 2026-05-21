import { DialogActions, DialogTitle, Button } from '@mui/material';

const DataTableDialogActions = (props) =>{
  return (
    <DialogActions>
      <Button onClick={props.handleClose}>Отмена</Button>
                
      { props.modalMode === 'add' &&
        <Button onClick={props.handleAdd} variant="contained">
          Создать
        </Button>
      }

      { props.modalMode === 'delete' &&
          <Button onClick={props.handleDelete} variant="contained" color="error" >
            Удалить
          </Button>
      }

      { props.modalMode === 'restore' &&
        <Button onClick={props.handleRestore} variant="contained" color="success" >
          Восстановить
        </Button>
      }
      
      { props.modalMode === 'edit' &&
        <Button onClick={props.handleSave} variant="contained">
          Изменить
        </Button>
      }

      { props.modalMode === 'writeOff' &&
        <Button onClick={props.handleWriteOff} variant="contained" color="warning">
          Списать
        </Button>
      }

      { props.modalMode === 'income' &&
        <Button onClick={props.handleIncome} variant="contained" color="success">
          Внести
        </Button>
      }

      { props.modalMode === 'order' &&
        <Button onClick={props.handleOrder} variant="contained" color="secondary">
          Заказать
        </Button>
      }

      { props.modalMode === 'qrIncome' &&
        <Button onClick={props.handleQrIncome} variant="contained" color="success">
          Добавить
        </Button>
      }
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