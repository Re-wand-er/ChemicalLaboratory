import { useState, useEffect } from "react";

import DataTable from "../../../components/DataTable/DataTable.jsx";
import DialogNotifications from "./DialogNotifications.jsx";

import { fetchGetData, fetchPostData, fetchDeleteByIds, fetchPutData } from '../../../api/fetch.js';
import { getRecordsArray } from '../../../utils/getRecordsArray.js';
import { formatDate } from "../../../utils/formatDate.js";

const columns = [
  {
    field: 'id',
    headerName: 'ID',
    width: 50,
    type: 'number',
  },
  {
    field: 'reagentId',
    headerName: 'ID реагента',
    width: 50,
    type: 'number',
  },
  {
    field: 'notificationType',
    headerName: 'Тип уведомления',
    width: 140,
  },
  {
    field: 'message',
    headerName: 'Сообщение',
    width: 400,
  },
  {
    field: 'createdAt',
    headerName: 'Создано',
    width: 160,
    renderCell: (date) => formatDate(date.value, 'datetime')
  },
  {
    field: 'isRead',
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

const Notifications = () => {
  const [data, setData] = useState([]); 
  const [reagent, setReagent] = useState([]);
  const [user, setUser] = useState([]);

  useEffect(() =>{        
    Promise.all([
      fetchGetData('/api/notification'), 
      fetchGetData('/api/reagent/name'),
      fetchGetData('/api/user/name'),
    ])
      .then(([notification, reagents, users]) => {
        setData(notification);
        setReagent(reagents);
        setUser(users);
      }); 

    console.log(data);  
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
    //const response = await fetchPostData('api/notification', record, true);

    if(response.ok)
    {      
      //fetchGetData('/api/notification', setData)
      handleClose();
    }
  }

  const handleDelete = () => {
    const ids = getRecordsArray(currentRecord).map(item => item.id);
    //const success = fetchDeleteByIds('api/notification/bulk-delete', ids);

    if (success){
      setData(prev => prev.filter(r => !ids.includes(r.id)));
      handleClose();
    }
  };

  const handleSave = async (record) => {
    //const result = await fetchPutData(`api/notification/${record.id}`, record, true);
    
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
      <h2>Уведомления</h2>
      <DataTable 
        rows={data} 
        columns={columns} 
        onAdd={handleOpenAdd} 
        onEdit={handleOpenEdit} 
        onDelete={handleOpenDelete}/>

      <DialogNotifications
        modalMode={modalMode}
        currentRecord={currentRecord}
        reagents={reagent}
        users={user}
        handleAdd={handleAdd}
        handleDelete={handleDelete}
        handleSave={handleSave}
        handleClose={handleClose}/>
    </div>
  );
};

export default Notifications;