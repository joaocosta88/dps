import './App.css';
import React, { useEffect, useState } from 'react';

import Register from './Pages/Register';

import Header from './Components/Header/Header';

import { Notifications } from '@mantine/notifications';
import { MantineProvider } from '@mantine/core';
import Login from './Pages/Login';


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
      <MantineProvider withGlobalStyles withNormalizeCSS>
        <Notifications autoClose={5000} />
        <Header />
        <Login />
      </MantineProvider>
    </main>
  );
}

export default App;