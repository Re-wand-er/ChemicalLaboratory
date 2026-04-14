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
            
            { props.modalMode === 'edit' &&
                <Button onClick={props.handleSave} variant="contained">
                  Изменить
                </Button>
            }

        </DialogActions>
    );
};

export { DataTableDialogActions };