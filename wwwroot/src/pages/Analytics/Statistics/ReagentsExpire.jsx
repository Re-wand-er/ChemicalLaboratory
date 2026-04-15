import TableWithLabel from '../../../components/DataTable/TableWithLabel.jsx';

import { formatDate } from "../../../utils/formatDate.js";  

import './statisticStyle.css';

const columns = [
  { field: 'id', headerName: 'ID', width: 50 },
  { field: 'name', headerName: 'Реагент', flex: 1 },
  { 
    field: 'expirationDate', 
    headerName: 'Срок до', 
    width: 110,
    valueFormatter: (params) => formatDate(params, 'date') 
  },
  { 
    field: 'daysRemaining', 
    headerName: 'Дней осталось', 
    width: 150,
    renderCell: (params) => {
      const days = params.value;
      let color = 'inherit'; 
      
      if (days <= 30) color = 'var(--main-error)'; // Red
      else if (days <= 60) color = 'var(--main-warning)'; // Yellow 
      return (
        <span style={{ color, fontWeight: Number(days) <= 60 ? 'var(--main-font-bold)' : 'var(--main-font-normal)' }}>
          {days < 0 ? `Просрочен (${Math.abs(days)})` : days}
        </span>
      );
    }
  }
];

/**
 * Список реактивов с истекающим сроком (ближайшие 30/60/90 дней)
 * Цвет текста поля с ближ. сроком минимума - красный?
 */
const ReagentsExpire = () => {
  return (
    <TableWithLabel title="Данные" path="/api/reagent/expiring" columns={columns} />
  );
};

export default ReagentsExpire;