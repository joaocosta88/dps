import { Routes, BrowserRouter, Route } from 'react-router-dom';
import './App.css';
import React, { useEffect, useState } from 'react'; // Add this line
// import { useAuthContext } from './Providers/AuthContext'; // Add this line
import { useAuthContext } from './Hooks/useAuthContext';

import LoginForm from './Pages/Login'
import RegisterForm from './Pages/Register';

import { Notifications } from '@mantine/notifications';
import { MantineProvider } from '@mantine/core';

import PrivateRoute from './Components/Routes/PrivateRoute';
import Dashboard from './Pages/Dashboard';
import CreateComponent from './Pages/CreateComponent';
import Header from './Components/Header/Header';

function App() {
  const { isAuthenticated, login, logout, me } = useAuthContext();
  const [appIsLoading, setAppIsLoading] = useState(true);
  
  useEffect(() => {
    me()
      .catch(() => {})
      .finally(() => setAppIsLoading(false));
  }, []);

  if (appIsLoading) {
    return <div>Loading...</div>;
  }

  
  return (
    <div className="App">
      <BrowserRouter>
      <MantineProvider withGlobalStyles withNormalizeCSS>
      <Notifications autoClose={40000}/>
            <Header />
            <Routes>
              <Route path="/login" element={<LoginForm />} />
              <Route path="/register" element={<RegisterForm />} />

              <Route element={<PrivateRoute />}>
                <Route path="/upload" element={<CreateComponent />} />
                <Route path="/dashboard" element={<Dashboard />} />
              </Route>
            </Routes>
        </MantineProvider>
      </BrowserRouter>
      
    </div>
  );
}

export default App;