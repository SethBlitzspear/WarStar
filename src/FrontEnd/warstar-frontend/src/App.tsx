import React, { useState, useEffect } from 'react';
import { AppBar, Toolbar, Typography, Button, Box, Divider } from '@mui/material';
import CreateAccount from './Components/CreateAccount';
import LoginPage from './Components//LoginPage';
import pages from './Pages/PagesIndex';
import { UserModel } from './Models/UserModel'; // Import the UserModel
import { BasePage } from './Pages/BasePage';

const App: React.FC = () => {
  const [loggedInUser, setLoggedInUser] = useState<UserModel | null>(null); // Store full user object
  const [activePage, setActivePage] = useState<string>(''); // Track active page

  // On component mount, check if user is already logged in
  useEffect(() => {
    const storedUser = localStorage.getItem('loggedInUser');
    if (storedUser) {
      setLoggedInUser(JSON.parse(storedUser));
    }
  }, []);

  const handleLoginSuccess = (user: UserModel) => {
    setLoggedInUser(user); // Store full user object
    setActivePage(''); // Go back to the main page
  };

  const handleLogout = () => {
    setLoggedInUser(null);
    localStorage.removeItem('loggedInUser'); // Clear user from localStorage
  };

 const renderActivePage = () => {
  switch (activePage) {
    case 'createAccount':
      return <CreateAccount />;
    case 'login':
      return <LoginPage onLoginSuccess={handleLoginSuccess} />;
    default:
      const page = pages.find((p) => p.name === activePage);
      return page ? <page.page loggedInUser={loggedInUser}/> : <Typography variant="h4">Welcome to WarStar</Typography>;
  }
};

  return (
    <div>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" sx={{ flexGrow: 1 }}>
            WarStar
          </Typography>

          {loggedInUser ? (
            <>
              <Typography variant="body1" sx={{ marginRight: '1rem' }}>
                {loggedInUser.username} {/* Display logged-in username */}
              </Typography>
              <Button color="inherit" onClick={handleLogout}>
                Logout
              </Button>
            </>
          ) : (
            <>
              <Button color="inherit" onClick={() => setActivePage('login')}>
                Login
              </Button>
              <Button color="inherit" onClick={() => setActivePage('createAccount')}>
                Create Account
              </Button>
            </>
          )}
        </Toolbar>
      </AppBar>

{/* Main content area with sidebar and dynamic screen area */}
<Box sx={{ display: 'flex', height: 'calc(100vh - 64px)' }}>
        
        {/* Sidebar */}
        <Box
               sx={{
                  width: '200px',
                  bgcolor: 'background.paper',
                  padding: 2,
                  display: 'flex',
                  flexDirection: 'column',
               }}
            >
               {pages.map((page) => (
                  <Button
                     key={page.name}
                     variant="text"
                     onClick={() => setActivePage(page.name)}
                     sx={{ textAlign: 'left', justifyContent: 'flex-start', mb: 1 }}
                  >
                     {page.name}
                  </Button>
               ))}
            </Box>

        <Divider orientation="vertical" flexItem />

        {/* Swappable screen area */}
        <Box sx={{ flex: 1, padding: 3 }}>
          {renderActivePage()}
        </Box>
      </Box>
    </div>
  );
};

export default App;
