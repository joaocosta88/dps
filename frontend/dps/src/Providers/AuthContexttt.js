

// import { createContext, useState } from "react";
// import { useNavigate } from "react-router-dom";
// import { getUserInfo, performLogin as loginUser, registerUser, refreshAccessToken } from "../Services/BackendHttpService";
// import {
//   getAccessToken, setAccessToken, clearAccessToken,
//   getRefreshToken, setRefreshToken, clearRefreshToken,
//   getUserInformation, setUserInformation, clearUserInformation
// } from "../Services/AuthClientStore";

// const AuthContext = createContext();

// export const AuthProvider = ({ children }) => {
//   const [userState, setUserState] = useState(() => getUserInformation());
//   const [accessTokenState, setAccessTokenState] = useState(() => getAccessToken());
//   const [, setRefreshTokenState] = useState(() => getRefreshToken());


//   const navigate = useNavigate();

//   const login = async (data) => {
//     try {
//       let response = await loginUser(data.email, data.password);
//       if (!response) {
//         throw new Error("unable to login");
//       }
//       storeTokenData(response.accessToken, response.refreshToken);

//       response = await getUserInfo(response.accessToken);
//       if (!response) {
//         throw new Error("unable to get user info")
//       }

//       let newUserState = JSON.stringify(response)
//       setUserInformation(newUserState);
//       setUserState(newUserState);

//       navigate("/");

//     } catch (err) {
//       console.error(err);
//     }
//   };

//   const register = async (data) => {
//     try {
//       let response = await registerUser(data.email, data.password);

//       if (!response) {
//         console.log(JSON.stringify(response))
//         throw new Error("unable to register user");
//       }
//       navigate("/");

//     }
//     catch (err) {
//       console.error(err);
//     }
//   }

//   const logout = () => {
//     //clear access token from state and from local storage
//     setAccessTokenState("");
//     clearAccessToken();

//     //clear refresh token from state and from local storage
//     setRefreshTokenState("");
//     clearRefreshToken();

//     //clear user information from state and from local storage
//     setUserState(null);
//     clearUserInformation();

//     navigate("/");
//   };

//   const refreshCurrentAccessToken = async (data) => {
//     try {
//       let response = await refreshAccessToken(data.accessToken, data.refreshToken);
//       storeTokenData(response.accessToken, response.refreshToken);
//     } catch (err) {
//       console.error(err);
//     } finally {
//       logout();
//     }
//   }

//   function storeTokenData(accessToken, refreshToken) {
//     setAccessTokenState(accessToken);
//     setAccessToken(accessToken);

//     setRefreshTokenState(refreshToken);
//     setRefreshToken(refreshToken);
//   }

//   return (
//     <AuthContext.Provider value={{
//       token: accessTokenState, user: userState,
//       login: login, logout: logout, register: register, refreshAccessToken: refreshCurrentAccessToken
//     }}>
//       {children}
//     </AuthContext.Provider>
//   );
// };

// export default AuthContext;