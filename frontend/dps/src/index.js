import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';

import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { AuthProvider } from './providers/AuthProvider';
import { MantineProvider } from '@mantine/core';
import { Notifications } from '@mantine/notifications';

import '@mantine/notifications/styles.css';
import '@mantine/core/styles.css';
import 'bootstrap/dist/css/bootstrap.min.css';



const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <AuthProvider>

      <BrowserRouter>
        <MantineProvider withGlobalStyles withNormalizeCSS>
          <Notifications autoClose={5000} />

          <Routes>
            <Route path='/*' element={<App />} />

          </Routes>
        </MantineProvider>
      </BrowserRouter>
    </AuthProvider>

  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();