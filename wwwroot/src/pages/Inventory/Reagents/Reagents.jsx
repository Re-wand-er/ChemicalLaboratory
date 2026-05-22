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
  const [modalMode, setModalMode] = useState(null); // 'add' | 'edit' | 'delete' | 'writeOff' | 'income' | 'qrIncome'
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

  const handleOpenWriteOff = (record) => {
    setCurrentRecord(record);
    setModalMode('writeOff');
  };

  const handleOpenIncome = (record) => {
    setCurrentRecord(record);
    setModalMode('income');
  };

  const handleOpenOrder = (record) => {
    setCurrentRecord(record);
    setModalMode('order');
  };

  const handleOpenQrIncome = () => {
    setCurrentRecord();
    setModalMode('qrIncome');
  };

  const handleClose = () => {
    setModalMode(null);
    setCurrentRecord(null);
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


  const handleWriteOff = async (record) => {
    const records = getRecordsArray(record);
  
    console.log(records);
    if (!records.length) return;
  
    const payload = records.map(item => ({
      id: item.id,
      name: item.name,
      chemicalFormula: item.chemicalFormula || null,
      unit: item.unit,
      currentQuantity: Number(item.currentQuantity), 
      minQuantity: Number(item.minQuantity),
      expirationDate: item.expirationDate || null,
      storageLocation: item.storageLocation || null,
      categoryId: Number(item.categoryId),
      isActive: item.isActive ?? true 
    }));

    const result = await fetchPutData(
      '/api/reagent/batch',
      payload,
      true
    );
  
    if (!result) return;
  
    setData(prev =>
      prev.map(item => {
        const updated = result.find(r => r.id === item.id);
        return updated ? { ...item, ...updated } : item;
      })
    );
  
    alert("Данные обновлены");
    updateCount();
    handleClose();
  };

  const handleSave = async (record) => {
    const result = await fetchPutData(`api/reagent/${record.id}`, record, true);

    if(result){      
      setData(prevItems => prevItems.map(data => data.id === result.id ? result : data));
      updateCount();
      handleClose();
    }
  };


  const handleOrder = async (orderPayload) => {
    if (!orderPayload || !orderPayload.length) {
      alert("Выберите реагенты и укажите количество для формирования заказа");
      return;
    }
  
    try {
      const response = await fetchPostData('/api/reagent/export-order-pdf', orderPayload, true);
  
      if (!response.ok) {
        throw new Error(`Ошибка сервера: ${response.status}`);
      }
  
      const blob = await response.blob();
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      
      const today = new Date();
      const formattedDate = `${String(today.getDate()).padStart(2, '0')}_${String(today.getMonth() + 1).padStart(2, '0')}_${today.getFullYear()}`;
      link.setAttribute('download', `Заявка_Закупка_${formattedDate}.pdf`); 
      
      document.body.appendChild(link);
      link.click();
      
      link.parentNode.removeChild(link);
      window.URL.revokeObjectURL(url);
  
      updateCount();
      handleClose(); 
  
    } catch (error) {
      console.error("Не удалось сгенерировать PDF заказа:", error.message);
      alert(`Ошибка при генерации отчета заказа: ${error.message}`);
    }
  };
  
  

///// QR ///////////////////////////////////////
  const handleQrUpdateSuccess = (updatedBatchFromApi) => {
    setData(prevItems =>
      prevItems.map(oldItem => {
        const updatedItem = updatedBatchFromApi.find(newItem => newItem.id === oldItem.id);
        return updatedItem ? { ...oldItem, ...updatedItem } : oldItem;
      })
    );
    
    updateCount(); 
    handleClose();
  };

  const handleQrIncome = async (filesArray) => {
    if (!filesArray || filesArray.length === 0) return;
  
    const formData = new FormData();
    
    filesArray.forEach((file) => {
      formData.append('files', file);
    });
  
    try {
      const response = await fetch('/api/reagent/income-by-qr', {
        method: 'POST',
        body: formData, 
        credentials: 'include' 
      });
  
      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(errorText || `Ошибка сервера: ${response.status}`);
      }
  
      const updatedReagents = await response.json();
      if (typeof handleQrUpdateSuccess === 'function') {
        handleQrUpdateSuccess(updatedReagents);
      }
  
      alert(`Успешно распознано и внесено реактивов: ${updatedReagents.length}`);
    } catch (error) {
      console.error("Ошибка при обработке QR-фотографий:", error.message);
      alert(`Не удалось обработать изображения. Qr-код не распознан!`);
      throw error;
    }
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
        onWriteOff={handleOpenWriteOff}
        onIncome={handleOpenIncome}
        onOrder={handleOpenOrder}
        onQrIncome={handleOpenQrIncome}
        isSuperAdmin={isSuperAdmin}
        isReagent={true}
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
        handleWriteOff={handleWriteOff}
        handleOrder={handleOrder}
        handleQrIncome={handleQrIncome}
        //handleIncome={handleIncome}
      />

    </PageContainer>
  );
};
export default Reagents;