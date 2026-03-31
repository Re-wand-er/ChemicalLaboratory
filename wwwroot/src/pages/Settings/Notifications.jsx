import { useState, useEffect } from "react";
import { fetchGetData } from '../../api/fetch.js';
import { formatDate } from "../../utils/formatDate.js";

import DataTable from "../../components/DataTable/DataTable.jsx";

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

    useEffect(() =>{        
            fetchGetData('/api/notification/', setData);
        }, []);      

    return (
        <div>
            <h2>Уведомления</h2>
            <DataTable rows={data} columns={columns} />
        </div>
    );
};
export default Notifications;