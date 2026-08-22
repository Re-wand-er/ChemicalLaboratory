const OPERATION_TYPES = Object.freeze({
  RECEIPT: 1,
  CONSUMPTION: 2,
  WRITEOFF: 3,
  ADJUSTMENT: 4,
  UPDATE: 5
});

const getOperationTypeColor = (typeId) => {
  switch(typeId){
    case OPERATION_TYPES.RECEIPT: return 'success';
    case OPERATION_TYPES.CONSUMPTION: return 'error';
    case OPERATION_TYPES.WRITEOFF: return 'warning'; 
    default: return 'default';
  }
}

export { OPERATION_TYPES, getOperationTypeColor }