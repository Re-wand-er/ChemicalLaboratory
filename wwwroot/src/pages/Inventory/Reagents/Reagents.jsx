import { useState, useEffect } from "react";

import DataTable from "../../../components/DataTable/DataTable.jsx";
import DialogReagents from "./DialogReagents.jsx";

import { fetchGetData, fetchPostData, fetchDeleteByIds, fetchPutData } from '../../../api/fetch.js';
import { getRecordsArray } from '../../../utils/getRecordsArray.js';
import { formatDate } from "../../../utils/formatDate.js";

const columns = [
  {
    field: 'id',
    headerName: 'ID',
    width: 40,
    type: 'number',
  },
  {
    field: 'name',
    headerName: 'Название',
    width: 200,
    // editable: true,
  },
  {
    field: 'chemicalFormula',
    headerName: 'Хим. формула',
    width: 150,
  },
  {
    field: 'unit',
    headerName: 'Ед. измерения',
    width: 50,
  },
  {
    field: 'currentQuantity',
    headerName: 'Текущее кол-во',
    width: 90,
    type: 'number',
    valueFormatter: (value) => `${value || 0}`,
  },
  {
    field: 'minQuantity',
    headerName: 'Мин. кол-во',
    width: 90,
    type: 'number',
    valueFormatter: (value) => `${value || 0}`,
  },
  {
    field: 'expirationDate',
    headerName: 'Срок годности',
    width: 140,
    type: 'date',
    valueFormatter: (value) => formatDate(value, 'date'),
  },
  {
    field: 'storageLocation',
    headerName: 'Место хранения',
    width: 180,
  },
  {
    field: 'categoryId',
    headerName: 'Категория',
    width: 150,
    type: 'number',
  },
  {
    field: 'isActive',
    headerName: 'Активен',
    width: 100,
    type: 'boolean',
    renderCell: (params) => (
      <span
        style={{
          color: params.value ? 'green' : 'red',
          fontWeight: 'bold',
        }}
      >
        {params.value ? '✓ Да' : '✗ Нет'}
      </span>
    ),
  },
];


const Reagents = () => {
  const [data, setData] = useState([]);
  const [categories, setCategories] = useState([]); 

    useEffect(() =>{
      Promise.all([
        fetchGetData('/api/reagent'), 
        fetchGetData('/api/category/name')
      ])
        .then(([reagents, categories]) => {
          setData(reagents);
          setCategories(categories);
        }); 
    }, []);    

  //// Методы для открытия соотв. окон //////////////////////////  
  // Состояние модального окна
  const [modalMode, setModalMode] = useState(null); // 'add' | 'edit' | 'delete'
  const [currentRecord, setCurrentRecord] = useState(null); // Данные для редактирования

  const handleOpenAdd = () => {
    setCurrentRecord(null);
    setModalMode('add');
  };

  const handleOpenEdit = (record) => {
    setCurrentRecord(record);
    setModalMode('edit');
  };

  const handleOpenDelete = (record) => {
    setCurrentRecord(record);
    setModalMode('delete');
  };
  ////////////////////////////////////////////////////////////////

  const handleAdd = async (record) => {
    const response = await fetchPostData('api/reagent', record, true);

    if(response.ok)
    {      
      fetchGetData('/api/reagent', setData)
      handleClose();
    }
  }

  const handleDelete = () => {
    const ids = getRecordsArray(currentRecord).map(item => item.id);
    const success = fetchDeleteByIds('api/reagent/bulk-delete', ids);

    if (success){
      setData(prev => prev.filter(r => !ids.includes(r.id)));
      handleClose();
    }
  };

  const handleSave = async (record) => {
    const result = await fetchPutData(`api/reagent/${record.id}`, record, true);
      
    if(result){      
      setData(prevItems => prevItems.map(data => data.id === result.id ? result : data));
      handleClose();
    }
  };

  // Закрытие окна
  const handleClose = () => {
    setModalMode(null);
    setCurrentRecord(null);
  };

  return (
    <div>
      <h2>Реагенты</h2>
      <DataTable 
        rows={data} 
        columns={columns} 
        onAdd={handleOpenAdd} 
        onEdit={handleOpenEdit} 
        onDelete={handleOpenDelete}/>

      <DialogReagents 
        modalMode={modalMode} 
        currentRecord={currentRecord} 
        categories={categories}
        handleAdd={handleAdd}
        handleDelete={handleDelete}
        handleSave={handleSave} 
        handleClose={handleClose} />

    </div>
  );
};
export default Reagents;