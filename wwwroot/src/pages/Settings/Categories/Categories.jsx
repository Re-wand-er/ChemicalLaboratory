import { useState, useEffect } from "react";

import DataTable from "../../../components/DataTable/DataTable.jsx";
import { useAuth } from "../../../context/AuthContext.jsx";
import DialogCategories from "./DialogCategories.jsx";

import { fetchGetData, fetchGetSuperAdminData, fetchPostData, fetchDeleteByIds, fetchPutData } from '../../../api/fetch.js';
import { getRecordsArray } from '../../../utils/getRecordsArray.js';

const columns = [
    // {
    //     field: 'id',
    //     headerName: 'ID',
    //     width: 60,
    //     type: 'number',
    // },
    {
        field: 'name',
        headerName: 'Наименование',
        width: 200,
    },
    {
        field: 'description',
        headerName: 'Описание',
        width: 600,
    }
];

const Categories = () => {
  const [data, setData] = useState([]);
  const { isSuperAdmin } = useAuth();

  useEffect(() =>{
      fetchGetSuperAdminData('/api/category', isSuperAdmin, setData);
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

  const handleOpenRestore = (record) => {
    setCurrentRecord(record);
    setModalMode('restore');
  };
  ////////////////////////////////////////////////////////////////

  const handleAdd = async (record) => {
    const response = await fetchPostData('api/category', record, true);
  
    console.log(record);

    if(response.ok)
    {      
      fetchGetData('/api/category', setData)
      handleClose();
    }
  }
  
  const handleDelete = () => {
    const ids = getRecordsArray(currentRecord).map(item => item.id);
    const success = fetchDeleteByIds('api/category/bulk-delete', ids);
  
    if (success){

      if(isSuperAdmin){
        setData(prevItems => prevItems.map(item => {

          if (ids.includes(item.id)) {
            return { ...item, isActive: false };
          }
        
          return item;
        }));
      }
      else{
        setData(prev => prev.filter(r => !ids.includes(r.id)));
      }
      
      handleClose();
    }
  };
  
  const handleRestore = async () => {
    const ids = getRecordsArray(currentRecord).map(item => item.id);
    const success = await fetchDeleteByIds('api/category/bulk-restore', ids);

    if (success){

      setData(prevItems => prevItems.map(item => {

        if (ids.includes(item.id)) {
          return { ...item, isActive: true };
        }
      
        return item;
      }));

      handleClose();
    }
  }

  const handleSave = async (record) => {
    const {id, ...saveRecord} = record; 
    console.log(saveRecord);

    const result = await fetchPutData(`api/category/${record.id}`, saveRecord, true);
        
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
    <>
      <h2>Категории</h2>
      <DataTable 
        rows={data} 
        columns={columns} 
        fileName="categories" 
        onAdd={handleOpenAdd} 
        onEdit={handleOpenEdit} 
        onDelete={handleOpenDelete}
        onRestore={handleOpenRestore}
        isSuperAdmin={isSuperAdmin}
      />

      <DialogCategories
        modalMode={modalMode} 
        currentRecord={currentRecord}
        handleAdd={handleAdd}
        handleDelete={handleDelete}
        handleRestore={handleRestore}
        handleSave={handleSave} 
        handleClose={handleClose} 
      />

    </>
  );
};
export default Categories;