import Avatar from '@mui/material/Avatar';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { Paper } from '@mui/material';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { FieldValues, useForm } from 'react-hook-form';
import { LoadingButton } from '@mui/lab';
import { useAppDispatch } from '../../app/store/configureStore';
import { signInUser } from './accountSlice';



const defaultTheme = createTheme();

export default function Login() {

    const navigate = useNavigate();
    const dispatch = useAppDispatch();
    const location = useLocation();
    const {register, handleSubmit, formState: {isSubmitting, errors, isValid}} = useForm({
        mode: 'all'
    });  
    
    async function submitForm(data: FieldValues) {
        
        try {
            await dispatch(signInUser(data));
            navigate(location.state?.from || '/catalog');
        } catch (error) {
            console.log(error);
        }
      
    }
    return (
        <ThemeProvider theme={defaultTheme}>
            <Container component={Paper} maxWidth='sm' sx={{ p: 4, display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                <CssBaseline />
                <Box
                    sx={{
                        marginTop: 8,
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                    }}
                >
                    <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                        <LockOutlinedIcon />
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        Prijava
                    </Typography>
                    <Box component="form" onSubmit={handleSubmit(submitForm)} noValidate sx={{ mt: 1 }}>
                        <TextField
                            margin="normal"
                            fullWidth
                            label="Email adresa"
                            autoComplete="email"
                            autoFocus
                            {...register('email', {required: 'Email is required'})}
                            error={!!errors.email}
                            helperText={errors?.email?.message as string}
                            />
                        <TextField
                            margin="normal"
                            fullWidth
                            label="Lozinka"
                            type="password"
                            autoComplete="current-password"
                            {...register('password', {required: 'Password is required'})}
                            error={!!errors.password}
                            helperText={errors?.password?.message as string}

                        />
                        <LoadingButton
                            disabled= {!isValid}
                            loading = {isSubmitting}
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            Prijava
                        </LoadingButton>
                        <Grid container>
                            <Grid item>
                                <Link to='/register'>
                                    {"Nemate nalog? Registrujte se"}
                                </Link>
                            </Grid>
                        </Grid>
                    </Box>
                </Box>
            </Container>
        </ThemeProvider>
    );
}