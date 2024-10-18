// import { createContext, useState } from 'react';
// import { useAuthApi } from "../http/apis/useAuthApi";
// import AuthClientStore from '../Store/AuthClientStore';

// const AuthContext = createContext(undefined);

// function AuthProvider({ children }) {
//   const [isAuthenticated, setIsAuthenticated] = useState(false);
//   const {
//     login: authLogin,
//     logout: authLogout,
//     me: authMe,
//     sendAuthGuardedRequest: authSendAuthGuardedRequest,
//   } = useAuthApi();

//   const login = async (email, password) => {
//     try {
//       var response = await authLogin(email, password);

//       AuthClientStore.setAccessToken(response.access_token);
//       AuthClientStore.setRefreshToken(response.refresh_token);

//       setIsAuthenticated(true);
//     } catch (e) {
//       setIsAuthenticated(false);
//       throw e;
//     }
//   };

  

//   const logout = () => {
//     AuthClientStore.removeAccessToken();
//     AuthClientStore.removeRefreshToken();

//     setIsAuthenticated(false);
//   };

//   const me = async () => {
//     const user = await authMe(() => {
//       setIsAuthenticated(false);
//     });
//     setIsAuthenticated(true);

//     return user;
//   };

//   const sendAuthGuardedRequest =
//     async (method, path, body, init,) => {
//       return authSendAuthGuardedRequest(
//         () => {
//           setIsAuthenticated(false);
//         },
//         method,
//         path,
//         body,
//         init,
//       );
//     };

//   return (
//     <AuthContext.Provider
//       value={{
//         isAuthenticated,
//         login,
//         logout,
//         me,
//         sendAuthGuardedRequest,
//       }}
//     >
//       {children}
//     </AuthContext.Provider>
//   );
// }

// export { AuthProvider, AuthContext };
