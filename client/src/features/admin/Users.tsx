import React, { useState, useEffect } from 'react';
import { Typography, TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody } from '@mui/material';
import { Delete } from '@mui/icons-material';
import { LoadingButton } from '@mui/lab';
import agent from '../../app/api/agent';

interface User {
  id: number;
  username: string;
  email: string;
  firstName: string;
  lastName: string;
}

const Users: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(false);
  const [target, setTarget] = useState<number | null>(null);

  useEffect(() => {
    fetchUsers();
  }, []);

  const fetchUsers = async () => {
    try {
      const response = await agent.Account.getAll();
      setUsers(response);
    } catch (error) {
      console.log(error);
    }
  };

  const handleDeleteUser = async (id: number) => {
    setLoading(true);
    setTarget(id);
    try {
      await agent.Account.delete(id);
      setUsers(users.filter((user) => user.id !== id));
    } catch (error) {
      console.log(error);
    } finally {
      setLoading(false);
      setTarget(null);
    }
  };

  return (
    <>
      <Typography variant="h4">Svi nalozi</Typography>
      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>#</TableCell>
              <TableCell>Korisnicko ime</TableCell>
              <TableCell>Email</TableCell>
              <TableCell>Ime</TableCell>
              <TableCell>Prezime</TableCell>
              <TableCell></TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {users.map((user) => (
              <TableRow key={user.id}>
                <TableCell>{user.id}</TableCell>
                <TableCell>{user.username}</TableCell>
                <TableCell>{user.email}</TableCell>
                <TableCell>{user.firstName}</TableCell>
                <TableCell>{user.lastName}</TableCell>
                <TableCell>
                  <LoadingButton
                    loading={loading && target === user.id}
                    onClick={() => handleDeleteUser(user.id)}
                    startIcon={<Delete />}
                    color="error"
                    variant="contained"
                    size="small"
                  >
                    Delete
                  </LoadingButton>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </>
  );
};

export default Users;
