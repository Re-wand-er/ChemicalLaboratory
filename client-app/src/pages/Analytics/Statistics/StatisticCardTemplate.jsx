import { Box, Stack, Typography } from '@mui/material';

import SimpleDataTable from '../../../components/DataTable/SimpleDataTable.jsx';

const StatisticCardTemplate = ({ title, children, rows, columns, typographyColor }) => {
  return (
    <Box>
      <Stack 
        direction="row" 
        justifyContent="space-between"
        alignItems='center'
        sx={{ mb:2 }}
      >
        <Typography variant="subtitle1" sx={{ fontWeight: 700 }} color={typographyColor}>
          {title}
        </Typography>          

        {children}
      </Stack>

      <SimpleDataTable 
        rows={rows}
        columns={columns}
      />
    </Box>
  );
};

export default StatisticCardTemplate;