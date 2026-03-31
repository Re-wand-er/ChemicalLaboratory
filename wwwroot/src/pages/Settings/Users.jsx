import { useState, useEffect } from "react";
import { fetchGetData } from '../../api/fetch.js';

import DataTable from "../../components/DataTable/DataTable.jsx";

const columns = [
    { 
        field: 'id', 
        headerName: 'ID', 
        width: 70,
        hide: true // Скрываем технический ID, если он не нужен пользователю
    },
    { 
        field: 'idWorkSchedule', 
        headerName: 'График работы', 
        width: 130 
    },
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
        field: 'systemRole', 
        headerName: 'Роль', 
        width: 120 
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
    useEffect(() =>{
        fetchGetData('/api/user/', setData);
    }, []);     

    return (
        <div>
            <h2>Пользователи</h2>
            <DataTable rows={data} columns={columns}/>
        </div>
    );
};
export default Users;