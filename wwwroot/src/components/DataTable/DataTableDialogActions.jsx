import { DialogActions, Button } from '@mui/material';

const DataTableDialogActions = (props) =>{
    return (
        <DialogActions>
            <Button onClick={props.handleClose}>Отмена</Button>
                      
            { props.modalMode === 'delete' 
              ? 
                <Button onClick={props.handleDelete} variant="contained" color="error" >
                  {props.modalMode === 'delete' && 'Удалить'}
                </Button>
              :
                <Button onClick={props.handleSave} variant="contained">
                  {props.modalMode === 'add' && 'Создать'}
                  {props.modalMode === 'edit' && 'Изменить'}
                </Button>
            }
        </DialogActions>
    );
};

export default DataTableDialogActions;