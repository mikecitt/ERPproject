import React, { useEffect, useState } from 'react';
import { Typography, Paper, Grid, CircularProgress, Button } from '@mui/material';
import agent from '../../app/api/agent';
import { useAppDispatch } from '../../app/store/configureStore';
import { signOut } from './accountSlice';
import { clearBasket } from '../basket/basketSlice';

interface ProfileData {
  email: string;
  username: string;
  firstName: string;
  lastName: string;
  phone: string;
}

const ProfilePage: React.FC = () => {
  const [profile, setProfile] = useState<ProfileData | null>(null);
  const [loading, setLoading] = useState(true);

  const dispatch = useAppDispatch();

  useEffect(() => {
    const fetchProfile = async () => {
      try {
        const response = await agent.Account.profile();
        console.log(response)
        setProfile(response);
        setLoading(false);
      } catch (error) {
        console.log(error);
      }
    };

    fetchProfile();
  }, []);

  const handleDeleteAccount = async () => {
    try {
      await agent.Account.deletePersonal();
      console.log('Account deleted successfully');
      dispatch(signOut());
      dispatch(clearBasket());
    } catch (error) {
      console.log(error);
    }
  };

  if (loading) {
    return (
      <div
        style={{
          display: 'flex',
          justifyContent: 'center',
          alignItems: 'center',
          height: '100vh',
        }}
      >
        <CircularProgress />
      </div>
    );
  }

  return (
    <div
      style={{
        display: 'flex',
        justifyContent: 'center',
        marginTop: '2rem',
      }}
    >
      <Paper
        sx={{
          padding: '2rem',
          margin: 'auto',
          maxWidth: 400,
          textAlign: 'center',
          backgroundColor: '#fafafa',
        }}
        elevation={3}
      >
        <Grid container spacing={2}>
          <Grid item xs={12}>
            <Typography variant="h4" align="center" gutterBottom>
              Profil
            </Typography>
          </Grid>
          <Grid item xs={12}>
            <Typography variant="body1">
              Email: {profile?.email}
            </Typography>
          </Grid>
          <Grid item xs={12}>
            <Typography variant="body1">
              Korisnicko ime: {profile?.username}
            </Typography>
          </Grid>
          <Grid item xs={12}>
            <Typography variant="body1">
               Ime: {profile?.firstName}
            </Typography>
          </Grid>
          <Grid item xs={12}>
            <Typography variant="body1">
              Prezime: {profile?.lastName}
            </Typography>
          </Grid>
          <Grid item xs={12}>
            <Typography variant="body1">
              Telefon: {profile?.phone}
            </Typography>
          </Grid>
          <Grid item xs={12}>
            <Button
              variant="contained"
              color="error"
              onClick={handleDeleteAccount}
            >
              Obriši nalog
            </Button>
          </Grid>
        </Grid>
      </Paper>
    </div>
  );
};

export default ProfilePage;
