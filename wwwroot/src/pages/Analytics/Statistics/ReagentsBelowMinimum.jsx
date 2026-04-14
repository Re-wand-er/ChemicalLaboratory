import TableWithLabel from '../../../components/DataTable/TableWithLabel.jsx'; 

import './statisticStyle.css';

const columns = [
  { field: 'id', headerName: 'ID', width: 50 },
  { field: 'name', headerName: 'Реагент', flex: 1 },
  { 
    field: 'currentQuantity', 
    headerName: 'Остаток', 
    width: 90,
    renderCell: (params) => (
      <span style={{ color: 'var(--main-error)', fontWeight: 'var(--main-font-bold)',  }}>
        {params.value}
      </span>
    )
  },
  { 
    field: 'minQuantity', 
    headerName: 'Мин.', 
    width: 70
  },
  { 
    field: 'unit', 
    headerName: 'Ед. изм.', 
    width: 80
  }
];

/**
 * Список реактивов с количеством ниже минимума
 * Цвет текста поля с кол-вом меньше минимума - красный
 */
const ReagentsBelowMinimum = () => {
  return (
    <div>
      <TableWithLabel title={"Данные2"} path="/api/reagent/low-stock" columns={columns} />
    </div>
  );
};

export default ReagentsBelowMinimum;