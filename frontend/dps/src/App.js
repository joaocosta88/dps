import './App.css';
import React, { useEffect, useState } from 'react';
import { Routes, Route } from 'react-router-dom'
import RequireAuth from './components/RequiredAuth';
import Header from './components/Header/Header';
import PersistLogin from './components/PersistLogin';

import Login from './pages/Login';
import Register from './pages/Register';
import Missing from './pages/Missing';
import Admin from './pages/Admin';
import Home from './pages/Home';
import LinkPage from './pages/LinkPage';
import Unauthorized from './pages/Unauthorized';
import Layout from './components/Layout';

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
        <Route path="/" element={<Layout />} >

          {/* Public routes  */}
          <Route path="login" element={<Login />} />
          <Route path="register" element={<Register />} />
          <Route path="linkpage" element={<LinkPage />} />
          <Route path="unauthorized" element={<Unauthorized />} />

          {/* Private routes */}
          <Route element={<PersistLogin />}>
            <Route element={<RequireAuth />}>
              <Route path="/" element={<Home />} />
            </Route>
            <Route element={<RequireAuth />}>
              <Route path="admin" element={<Admin />} />
            </Route>
          </Route>
          {/* Catch all */}
          <Route path="*" element={<Missing />} />
        </Route>
      </Routes>
    </main >
  );
}

export default App;