import { Container, Typography } from "@mui/material";

const PageContainer = ({ title, children }) => {
  return (
    <Container maxWidth="xl">
      <Typography variant="h4" sx={{ mb: 4, fontWeight: 'bold', color: 'text.primary' }}>
        {title}
      </Typography>

      {children}
    </Container>
  );
};
export default PageContainer;