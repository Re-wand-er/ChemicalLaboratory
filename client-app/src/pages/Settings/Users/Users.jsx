import { useState, useEffect } from "react";

import { useAuth } from '../../../context/AuthContext.jsx';
import PageContainer from "../../../components/PageContainer.jsx";
import DataTable from "../../../components/DataTable/DataTable.jsx";
import DialogUsers from "./DialogUsers.jsx";

import { fetchGetData, fetchGetSuperAdminData, fetchDeleteByIds, fetchPostData, fetchPutData } from '../../../api/fetch.js';
import { getRecordsArray } from '../../../utils/getRecordsArray.js';
import { systemRoles } from '../../../constants/roles.js';

const columns =(schedules) => [
  // { 
  //     field: 'id', 
  //     headerName: 'ID', 
  //     width: 70,
  //     hide: true // Скрываем технический ID, если он не нужен пользователю
  // },
    { 
    field: 'firstName', 
    headerName: 'Имя', 
    width: 120 
  },
  { 
    field: 'middleName', 
    headerName: 'Отчество', 
    width: 130 
  },
  { 
    field: 'lastName', 
    headerName: 'Фамилия', 
    width: 130 
  },
  { 
    field: 'idworkSchedule', //idworkSchedule
    headerName: 'График работы', 
    width: 130,
    valueGetter: (value, row) => {
      if (!schedules || schedules.length === 0) return 'Загрузка...';
  
      const currentId = value || row?.idWorkSchedule;
      const foundSchedule = schedules.find(s => s.id === currentId);
  
      if (foundSchedule) {
        // if (foundSchedule.workShift) {
        //   return foundSchedule.workShift;
        // }

        return `${foundSchedule.startTime.slice(0, 5)} - ${foundSchedule.endTime.slice(0, 5)}`
      }
  
      return schedules[0]?.workShift || '';
    }
  },
  { 
    field: 'email', 
    headerName: 'Email', 
    width: 200 
  },
  { 
    field: 'sex', 
    headerName: 'Пол', 
    width: 80,
    valueFormatter: (params) => params.value === 'MALE' ? 'М' : 'Ж' // Пример форматирования
  },
  { 
    field: 'systemRoleId', 
    headerName: 'Роль', 
    width: 120,
    valueGetter: (value, row)=>{
      const role = systemRoles.find(r => r.id == row?.systemRoleId);
      return role.name;
    }
  },
  { 
    field: 'jobPosition', 
    headerName: 'Должность', 
    width: 150 
  },
  {
    field: 'login', 
    headerName: 'Логин', 
    width: 120 
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

const Users = () => {
    const [data, setData] = useState([]); 
    const [workSchedule, setWorkSchedule] = useState([]);
    const { isSuperAdmin } = useAuth();   

    useEffect(() =>{
      Promise.all([
        fetchGetSuperAdminData('/api/user', isSuperAdmin), 
        fetchGetData('/api/work-schedule')
      ])
        .then(([users, schedules, roles]) => {
          setData(users);
          setWorkSchedule(schedules);
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

  const handleOpenRestore = (record) => {
    setCurrentRecord(record);
    setModalMode('restore');
  };
  ////////////////////////////////////////////////////////////////
  const handleAdd = async (record) => {
    const response = await fetchPostData('api/user', record, true);

    console.log(record);

    if(response.ok)
    {      
      fetchGetData('api/user', setData)
      handleClose();
    }
  }

  const handleDelete = () => {
    const ids = getRecordsArray(currentRecord).map(item => item.id);
    const success = fetchDeleteByIds('api/user/bulk-delete', ids);

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
    const success = await fetchDeleteByIds('api/user/bulk-restore', ids);

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
    const result = await fetchPutData(`api/user`, record, true);
    
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
    <PageContainer title="Пользователи">
      <DataTable 
        rows={data} 
        columns={columns(workSchedule)} 
        fileName="users"
        onAdd={handleOpenAdd} 
        onEdit={handleOpenEdit} 
        onDelete={handleOpenDelete}
        onRestore={handleOpenRestore}
        isSuperAdmin={isSuperAdmin}
      />

      <DialogUsers 
        modalMode={modalMode} 
        currentRecord={currentRecord} 
        schedules={workSchedule}
        handleAdd={handleAdd}
        handleDelete={handleDelete}
        handleRestore={handleRestore}
        handleSave={handleSave} 
        handleClose={handleClose} 
      />    

    </PageContainer>
  );
};
export default Users;