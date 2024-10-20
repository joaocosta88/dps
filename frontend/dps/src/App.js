import './App.css';
import React, { useEffect, useState } from 'react';
import { Routes, Route } from 'react-router-dom'
import RequireAuth from './components/RequiredAuth';
import Header from './components/Header/Header';


import Login from './pages/Login';
import Register from './pages/Register';
import Missing from './pages/Missing';
import Admin from './pages/Admin';
import Home from './pages/Home';

import Unauthorized from './pages/Unauthorized';
import Layout from './components/Layout';

const ROLES = {
  Admin: 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role: Administrator'
}

function App() {
  const [appIsLoading, setAppIsLoading] = useState(true);

  useEffect(() => {
    console.log("testing")
    setAppIsLoading(false);
  }, []);

  if (appIsLoading) {
    return <div>Loading...</div>;
  }


  return (
    <main className="App">
      <Header />


      <Routes>
        <Route path="/" element={<Layout />} >

          {/* Public routes  */}
          <Route path="login" element={<Login />} />
          <Route path="register" element={<Register />} />
          <Route path="unauthorized" element={<Unauthorized />} />
          
          <Route element={<RequireAuth />}>
            <Route path="home" element={<Home />} />
          </Route>

          {/* Private routes */}
          <Route element={<RequireAuth allowedRoles={[ROLES.Admin]} />}>
            <Route path="admin" element={<Admin />} />
          </Route>
          {/* Catch all */}
          <Route path="*" element={<Missing />} />
        </Route>
      </Routes>
    </main>
  );
}

export default App;