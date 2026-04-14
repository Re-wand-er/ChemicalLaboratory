import { useState, useEffect } from "react";
import { DataGrid } from '@mui/x-data-grid';

import { fetchGetData } from '../../api/fetch.js';
import SimpleDataTable from "./SimpleDataTable.jsx";

const TableWithLabel = ({ title, path, columns }) => {
  const [rows, setRows] = useState([]);

  useEffect(() => {
    fetchGetData(path, setRows);
  }, []);

  return (
    <div style={{ width: '100%', maxWidth: '600px' }}>
      <div>
        <label>
          {title}
        </label>
      </div>

      <SimpleDataTable 
        rows={rows} 
        columns={columns} 
      />
      
    </div>
  );
};

export default TableWithLabel;