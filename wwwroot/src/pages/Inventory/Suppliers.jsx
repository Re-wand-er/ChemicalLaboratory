import { useState, useEffect } from "react";
import { fetchGetData } from '../../api/fetch.js';

import DataTable from "../../components/DataTable/DataTable.jsx";

const columns = [
    {
        field: 'id',
        headerName: 'ID',
        width: 50,
        type: 'number',
    },
    {
        field: 'name',
        headerName: 'Наименование',
        width: 200,
    },
    {
        field: 'contactInfo',
        headerName: 'Контактная информация',
        width: 150,
    },
    {
        field: 'address',
        headerName: 'Адрес',
        width: 200,
    }
];

const Suppliers = () => {
    const [data, setData] = useState([]); 

    useEffect(() =>{
        fetchGetData('/api/supplier', setData);
    }, []);      

    return (
        <div>
            <h2>Поставщики</h2>
            <DataTable rows={data} columns={columns} />
        </div>
    );
};
export default Suppliers;