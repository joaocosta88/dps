import './App.css';
import React, { useEffect, useState } from 'react';
import { Routes, Route } from 'react-router-dom'
import RequireAuth from './components/RequiredAuth';
import Header from './components/Header/Header';
import PersistLogin from './components/PersistLogin';

import Login from './pages/Auth/Login';
import Register from './pages/Auth/Register';
import Missing from './pages/Missing';
import Admin from './pages/Admin';
import AddListing from './pages/AddListing';
import Home from './pages/Home';
import Unauthorized from './pages/Unauthorized';
import Layout from './components/Layout';
import UserShop from './pages/UserShop';
import PasswordReset from './pages/Auth/ForgotPassword';
import ResetPassword from './pages/Auth/ResetPassword';
import ConfirmAccount from './pages/Auth/ConfirmAccount';

function App() {
  const [appIsLoading, setAppIsLoading] = useState(true);

  useEffect(() => {
    setAppIsLoading(false);
  }, []);

  if (appIsLoading) {
    return <div>Loading...</div>;
  }


  return (
    <main className="App">
      <Header />

      <Routes>
        <Route element={<PersistLogin />}>
          <Route path="/" element={<Layout />} >

            {/* Public routes  */}
            {/*  Auth  */}
            <Route path="login" element={<Login />} />
            <Route path="register" element={<Register />} />
            <Route path="forgotpassword" element={<PasswordReset /> } />
            <Route path="resetpassword" element={<ResetPassword /> } />
            <Route path="confirmaccount" element={<ConfirmAccount />} />
            <Route path="unauthorized" element={<Unauthorized />} />
            <Route path="/" element={<Home />} />
            <Route path="/shop/:userId" element={<UserShop />} />
            {/* Private routes */}
            <Route element={<RequireAuth />}>
              <Route path="admin" element={<Admin />} />
            </Route>
            <Route element={<RequireAuth />}>
              <Route path="addlisting" element={<AddListing />} />
            </Route>

            {/* Catch all */}
            <Route path="*" element={<Missing />} />
          </Route>

        </Route>
      </Routes>
    </main >
  );
}

export default App;