import './App.css';
import React, { useEffect, useState } from 'react';
import { Routes, Route } from 'react-router-dom'
import RequireAuth from './Components/RequiredAuth';
import Header from './Components/Header/Header';


import Login from './Pages/Login';
import Register from './Pages/Register';
import Users from './Pages/Users';
import Missing from './Pages/Missing';

import Layout from './Components/Layout';


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


          {/* Private routes */}
          <Route element = {<RequireAuth />}> 
            <Route path="users" element={<Users />} />
           </Route>
          {/* Catch all */}
          <Route path="*" element={<Missing /> } />
        </Route>
      </Routes>
    </main>
  );
}
 
export default App;