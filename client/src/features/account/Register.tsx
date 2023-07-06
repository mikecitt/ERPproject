import Avatar from '@mui/material/Avatar';
import TextField from '@mui/material/TextField';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { Alert, AlertTitle, List, ListItem, ListItemText, Paper } from '@mui/material';
import { Link, useNavigate } from 'react-router-dom';
import { useForm } from 'react-hook-form';
import { LoadingButton } from '@mui/lab';
import agent from '../../app/api/agent';
import { useState } from 'react';
import { toast } from 'react-toastify';

export default function Register() {
    const navigate = useNavigate();
    const [validationErrors, setValidationErrors ] = useState([]);
    const { register, handleSubmit, formState: { isSubmitting, errors, isValid } } = useForm({
        mode: 'all'
    });

    return (
        <Container component={Paper} maxWidth="sm" sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center', p: 4 }}>
            <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
                Registracija
            </Typography>
            <Box component="form"
                onSubmit={handleSubmit((data) =>
                    agent.Account.register(data)
                    .then(() => {
                        toast.success('Registracija uspesna- mozete se prijaviti');
                        navigate('/login');
                    })
                        .catch(error => setValidationErrors(error)))
                }
                noValidate sx={{ mt: 1 }}>
                <TextField
                    margin="normal"
                    fullWidth
                    label="Korisnicko ime"
                    autoFocus
                    {...register("username", {
                        required: 'Korisnicko ime je obavezno'
                    })}
                    error={!!errors.username} 
                />
                <TextField
                    margin="normal"
                    fullWidth
                    label="Email addresa"
                    {...register("email", {
                        required: 'Email addresa je obavezna',
                        pattern: {
                            value: /^([\w\-.]+)@((\[([0-9]{1,3}\.){3}[0-9]{1,3}\])|(([\w-]+\.)+)([a-zA-Z]{2,4}))$/,
                            message: 'Not valid email address'
                        }
                    })}
                    error={!!errors.email} //!! - make casting a variable to boolean
                />
                <TextField
                    margin="normal"
                    fullWidth
                    label="Lozinka"
                    type="password"
                    {...register('password', {
                        required: 'Lozinka je obavezna'
                    })}
                    error={!!errors.password}
                />
                {validationErrors.length > 0 &&
                    <Alert severity='error'>
                        <AlertTitle>Validation Errors</AlertTitle>
                        <List>
                            {validationErrors.map(error =>
                                <ListItem key={error}>
                                    <ListItemText>{error}</ListItemText>
                                </ListItem>
                            )}
                        </List>
                    </Alert>
                }
                <TextField
                    margin="normal"
                    fullWidth
                    label="Ime"
                    {...register('firstname', {
                        required: 'Ime  je  obavezno'
                    })}
                    error={!!errors.password}
                />
                 <TextField
                    margin="normal"
                    fullWidth
                    label="Prezime"
                    {...register('lastname', {
                        required: 'Prezime  je  obavezno'
                    })}
                    error={!!errors.password}
                />
                 <TextField
                    margin="normal"
                    fullWidth
                    label="Telefon"
                    {...register('phone', {
                        required: 'Telefon je obavezan'
                    })}
                    error={!!errors.password}
                />
                {validationErrors.length > 0 &&
                    <Alert severity='error'>
                        <AlertTitle>Validation Errors</AlertTitle>
                        <List>
                            {validationErrors.map(error =>
                                <ListItem key={error}>
                                    <ListItemText>{error}</ListItemText>
                                </ListItem>
                            )}
                        </List>
                    </Alert>
                }
                <LoadingButton
                    disabled={!isValid}
                    loading={isSubmitting}
                    type="submit"
                    fullWidth
                    variant="contained"
                    sx={{ mt: 3, mb: 2 }}
                >
                    Registracija
                </LoadingButton>
                <Grid container>
                    <Grid item>
                        <Link to='/login'>
                            {"Imate nalog? Prijavite se"}
                        </Link>
                    </Grid>
                </Grid>
            </Box>
        </Container>
    );
}