import { useState, useEffect } from "react";

import { useAuth } from '../../../context/AuthContext.jsx';
import PageContainer from "../../../components/PageContainer.jsx";
import DataTable from "../../../components/DataTable/DataTable.jsx";
import DialogReagents from "./DialogReagents.jsx";

import { useNotifications } from "../../../context/NotificationContext.jsx";
import { fetchGetData, fetchGetSuperAdminData, fetchPostData, fetchDeleteByIds, fetchPutData } from '../../../api/fetch.js';
import { getRecordsArray } from '../../../utils/getRecordsArray.js';
import { formatDate } from "../../../utils/formatDate.js";

const columns = [
  // {
  //   field: 'id',
  //   headerName: 'ID',
  //   width: 40,
  //   type: 'number',
  // },
  {
    field: 'name',
    headerName: 'Название',
    minWidth: 200,
    flex: 2,
    // editable: true,
  },
  {
    field: 'chemicalFormula',
    headerName: 'Хим. формула',
    minWidth: 150,
    flex: 1,
  },
  {
    field: 'unit',
    headerName: 'Ед. изм.',
    width: 60,
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
    minWidth: 180,
    flex: 2,
  },
  {
    field: 'categoryName',
    headerName: 'Категория',
    width: 150,
    //type: 'number',
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
  const { updateCount } = useNotifications();
  const { isSuperAdmin } = useAuth(); 

  useEffect(() =>{
    Promise.all([
      fetchGetSuperAdminData('/api/reagent', isSuperAdmin), 
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

    console.log(record);
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
    const response = await fetchPostData('api/reagent', record, true);

    if(response.ok)
    {      
      fetchGetData('/api/reagent', setData)
      handleClose();
    }
  }

  const handleDelete = async () => {
    console.log(currentRecord);
    const ids = getRecordsArray(currentRecord).map(item => item.id);
    const success = await fetchDeleteByIds('api/reagent/bulk-delete', ids);
    console.log(ids);

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
    const success = await fetchDeleteByIds('api/reagent/bulk-restore', ids);

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
    const result = await fetchPutData(`api/reagent/${record.id}`, record, true);

    if(result){      
      setData(prevItems => prevItems.map(data => data.id === result.id ? result : data));
      updateCount();
      handleClose();
    }
  };

  // Закрытие окна
  const handleClose = () => {
    setModalMode(null);
    setCurrentRecord(null);
  };

  return (
    <PageContainer title="Реагенты">

      <DataTable 
        rows={data} 
        columns={columns} 
        fileName="reagents"
        onAdd={handleOpenAdd} 
        onEdit={handleOpenEdit} 
        onDelete={handleOpenDelete}
        onRestore={handleOpenRestore}
        isSuperAdmin={isSuperAdmin}
      />

      <DialogReagents 
        modalMode={modalMode} 
        currentRecord={currentRecord} 
        categories={categories}
        handleAdd={handleAdd}
        handleDelete={handleDelete}
        handleRestore={handleRestore}
        handleSave={handleSave} 
        handleClose={handleClose} 
      />

    </PageContainer>
  );
};
export default Reagents;