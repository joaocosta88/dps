import { Routes, BrowserRouter, Route } from 'react-router-dom';
import './App.css';

import LoginForm from './Pages/Login'
import RegisterForm from './Pages/Register';

import { AuthProvider } from './Providers/AuthContext';
import { Notifications } from '@mantine/notifications';
import { MantineProvider } from '@mantine/core';

import PrivateRoute from './Components/Routes/PrivateRoute';
import Dashboard from './Pages/Dashboard';
import Header from './Components/Header/Header';

function App() {
  return (
    <div className="App">
      <BrowserRouter>
      <MantineProvider withGlobalStyles withNormalizeCSS>
      <Notifications autoClose={40000}/>
          <AuthProvider>
            <Header />
            <Routes>
              <Route path="/login" element={<LoginForm />} />
              <Route path="/register" element={<RegisterForm />} />

              <Route element={<PrivateRoute />}>
                <Route path="/dashboard" element={<Dashboard />} />
              </Route>
            </Routes>
          </AuthProvider>
        </MantineProvider>
      </BrowserRouter>
      
    </div>
  );
}

export default App;