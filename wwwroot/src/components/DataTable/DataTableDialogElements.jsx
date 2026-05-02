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

    </DialogActions>
  );
};

const DataTableDialogLabel = ({modalMode, size, deleteOne, deleteMany, restoreOne, restoreMany}) =>{
	const getTitle = () => {
		if(modalMode === 'delet'){
			return size > 1 ? deleteMany : deleteOne;
		}
		if (modalMode === 'restore') {
       return size > 1 ? restoreMany : restoreOne;
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