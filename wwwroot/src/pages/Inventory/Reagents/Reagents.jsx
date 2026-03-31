import { useState, useEffect } from "react";
import { fetchGetData } from '../../../api/fetch.js';
import { Button } from '@mui/material';

import DataTable from "../../../components/DataTable/DataTable.jsx";
import DialogReagents from "./DialogReagents.jsx";

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
    field: 'createdAt',
    headerName: 'Дата создания',
    width: 140,
    type: 'date',
    valueFormatter: (value) => formatDate(value, 'date'),
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

    useEffect(() =>{
        fetchGetData('/api/reagent', setData);
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

    // Обработка данных //
    const handleSave = () => {

      console.log('Режим:', modalMode, 'Данные:', currentRecord);

      handleClose();
    };

    const handleDelete = () => {

      console.log('Режим:', modalMode, 'Данные:', currentRecord);

      handleClose();
    };

    // Закрытие окна
    const handleClose = () => {
      setModalMode(null);
      setCurrentRecord(null);
    };
    // Обработка данных //

    return (
        <div>
            <h2>Реагенты</h2>
            <DataTable rows={data} columns={columns} onAdd={handleOpenAdd} onEdit={handleOpenEdit} onDelete={handleOpenDelete}/>

            <DialogReagents modalMode={modalMode} currentRecord={currentRecord} handleClose={handleClose} handleSave={handleSave} handleDelete={handleDelete}/>
        </div>
    );
};
export default Reagents;