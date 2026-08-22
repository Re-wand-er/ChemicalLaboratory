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
    minWidth: 150,
    flex: 1.5,
    // editable: true,
  },
  {
    field: 'chemicalFormula',
    headerName: 'Хим. формула',
    minWidth: 100,
    flex: 1.5,
  },
  {
    field: 'unit',
    headerName: 'Ед. изм.',
    width: 60,
  },
  {
    field: 'currentQuantity',
    headerName: 'Тек. кол-во',
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

  const handleQrIncome = async (filesArray, onScanSuccess, setScanning) => {
    // 1. Защита: если лаборант нажал кнопку, но файлы не выбраны
    if (!filesArray || filesArray.length === 0) return;

    // 2. Включаем лоадер (блокируем кнопки интерфейса)
    setScanning(true);

    // 3. Создаем контейнер FormData для передачи файлов как multipart/form-data
    const formData = new FormData();

    // Имя ключа 'files' должно строго совпадать с аргументом в C# (List<IFormFile> files)
    filesArray.forEach((file) => {
      formData.append('files', file);
    });

    try {
      // 4. Отправляем POST-запрос на C# бэкенд для ПЕРВИЧНОГО распознавания (без сохранения в БД)
      const response = await fetch('/api/reagent/scan-qr-preview', {
        method: 'POST',
        body: formData, 
        credentials: 'include' // Передаем куки сессии авторизации
      });

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(errorText || `Ошибка сервера: ${response.status}`);
      }

      // 5. Получаем от C# легкий JSON-массив пар [ { "id": 11, "quantity": 5.0 }, ... ]
      const data = await response.json(); 

      // 6. ВЫЗОВ КОЛБЭКА: Записываем этот массив в стейт qrChanges внутри ReagentDialog.jsx
      // Это мгновенно переключит интерфейс диалога с загрузчика на таблицу сверки
      if (typeof onScanSuccess === 'function') {
        onScanSuccess(data); 
      }

    } catch (error) {
      console.error("Ошибка при обработке QR-фотографий:", error.message);
      alert(`Не удалось распознать изображения: ${error.message}`);
    } finally {
      // 7. Всегда выключаем лоадер в конце (при успехе или ошибке)
      setScanning(false);
    }
  };

    // 1. Метод Шага 1: Отправка фото на C# для распознавания кодов
  const handleUploadAndScan = async (filesArray, onScanSuccess, setScanning) => {
    if (!filesArray || filesArray.length === 0) return;

    setScanning(true);
    const formData = new FormData();
    filesArray.forEach(file => formData.append('files', file));

    try {
      const response = await fetch('/api/reagent/scan-qr-preview', {
        method: 'POST',
        body: formData,
        credentials: 'include'
      });

      if (!response.ok) {
        const errText = await response.text();
        throw new Error(errText || 'Не удалось распознать коды');
      }

      const data = await response.json(); // [ { id, quantity } ]
      onScanSuccess(data); // Передаем результат обратно в стейт Диалога
    } catch (err) {
      alert(`Ошибка сканирования: ${err.message}`);
    } finally {
      setScanning(false);
    }
  };

  // 2. Метод Шага 2: Финальный приход через UpdateBatchAsync
  const handleFinalSubmitIncome = async (qrChangesArray, rowsMaster, onSaveSuccess, setScanning, closeDialog) => {
    if (!qrChangesArray || qrChangesArray.length === 0) return;

    setScanning(true);

    // Собираем Payload, скрещивая черновик qrChanges с глобальным rowsMaster
    const payload = qrChangesArray.map(change => {
      const original = rowsMaster.find(r => r.id === change.id);
      return {
        ...original,
        currentQuantity: Number(original.currentQuantity) + Number(change.quantity),
        chemicalFormula: original.chemicalFormula || null,
        storageLocation: original.storageLocation || null,
        expirationDate: original.expirationDate || null,
        isActive: original.isActive ?? true
      };
    });

    try {
      const result = await fetchPutData('/api/reagent/batch', payload, true);
      if (result) {
        setData(prevItems =>
          prevItems.map(oldItem => {
            const updatedItem = result.find(newItem => newItem.id === oldItem.id);
            return updatedItem ? { ...oldItem, ...updatedItem } : oldItem;
          })
        );
        updateCount(); 
        handleClose();
        alert("Реактивы успешно зачислены.");
      }
    } catch (err) {
      alert("Ошибка сохранения: " + err.message);
    } finally {
      setScanning(false);
    }
  };

  ///// QR ///////////////////////////////////////


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

        // Qr
        rows={data} // Передаем все данные для маппинга названий
        onScanQr={handleUploadAndScan} // Метод Шага 1
        onSaveQrIncome={handleFinalSubmitIncome} // Метод Шага 2
        // Qr

      />

    </PageContainer>
  );
};
export default Reagents;