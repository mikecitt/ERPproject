import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import Paper from '@mui/material/Paper';
export default function HomePage() {
    return (
        <Box
      display="flex"
      flexDirection="column"
      alignItems="center"
      justifyContent="center"
      height="100vh"
      textAlign="center"
      padding={2}
    >
      <Typography variant="h3" gutterBottom>
      Dobrodosli u online trgovinsku radnju 
      </Typography>
      <Paper elevation={3} sx={{ width: 400, height: 'auto', borderRadius: 8, boxShadow: '0 2px 6px rgba(0, 0, 0, 0.2)' }}>
        <img src="home.jpg" alt="Welcome" style={{ width: '100%', height: 'auto', borderRadius: 8 }} />
      </Paper>
    </Box>
    )

}