import React, { useState } from 'react';
import { Box, TextField, Button, Typography } from '@mui/material';
import axios from 'axios';
import { UserModel } from '../Models/UserModel'; // Import the UserModel

interface LoginPageProps {
  onLoginSuccess: (user: UserModel) => void; // Pass full user model back to parent
}

const LoginPage: React.FC<LoginPageProps> = ({ onLoginSuccess }) => {
  const [username, setUsername] = useState<string>('');
  const [password, setPassword] = useState<string>('');

  const handleClear = () => {
    setUsername('');
    setPassword('');
  };

  const handleSubmit = async () => {
    try {
      const apiBaseUrl = process.env.REACT_APP_USER_API_BASE_URL;
      const response = await axios.post(`${apiBaseUrl}user`, {
        username,
        password,
      });

      // Assuming the backend returns the JWT token and user details on successful login
      if (response.status === 200) {
        const { token, userId, eMail, name } = response.data; // Example response data from backend
        const user: UserModel = {
          userId,
          username,
          name,
          eMail,
          token, // JWT token from backend
        };

        onLoginSuccess(user); // Pass full user object to parent
        localStorage.setItem('loggedInUser', JSON.stringify(user)); // Save user info to localStorage
      } else {
        alert('Login failed!');
      }
    } catch (error) {
      console.error('Error during login:', error);
      alert('Failed to log in.');
    }
  };

  return (
    <Box sx={{ maxWidth: 400, margin: '0 auto', padding: 2 }}>
      <Typography variant="h5" sx={{ marginBottom: 2 }}>
        Login
      </Typography>

      <TextField
        label="Username"
        fullWidth
        margin="normal"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
      />

      <TextField
        label="Password"
        fullWidth
        margin="normal"
        type="password"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />

      <Box sx={{ display: 'flex', justifyContent: 'space-between', marginTop: 2 }}>
        <Button variant="outlined" color="secondary" onClick={handleClear}>
          Clear
        </Button>
        <Button variant="contained" color="primary" onClick={handleSubmit}>
          Submit
        </Button>
      </Box>
    </Box>
  );
};

export default LoginPage;
