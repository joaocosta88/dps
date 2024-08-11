import { createContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getUserInfo, performLogin as loginUser, registerUser, refreshAccessToken } from "../Services/BackendHttpService";
import {
  getAccessToken, setAccessToken, clearAccessToken,
  getRefreshToken, setRefreshToken, clearRefreshToken,
  getAccessTokenTimeoutTaskId, setAccessTokenTimeoutTaskId, clearAccessTokenTimeoutTaskId,
  getUserInformation, setUserInformation, clearUserInformation
} from "../Services/StorageManager";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [userState, setUserState] = useState(() => getUserInformation());
  const [accessTokenState, setAccessTokenState] = useState(() => getAccessToken());
  const [, setRefreshTokenState] = useState(() => getRefreshToken());
  const [timeoutIdState, setTimeoutIdState] = useState(() => getAccessTokenTimeoutTaskId());


  const navigate = useNavigate();

  const login = async (data) => {
    try {
      let response = await loginUser(data.email, data.password);
      if (!response) {
        throw new Error("unable to login");
      }
      storeTokenData(response.accessToken, response.refreshToken);
      setTokenRefreshInterval(response.accessToken, response.refreshToken, response.expiresIn);

      response = await getUserInfo(response.accessToken);
      if (!response) {
        throw new Error("unable to get user info")
      }

      let newUserState = JSON.stringify(response)
      setUserInformation(newUserState);
      setUserState(newUserState);

      navigate("/");

    } catch (err) {
      console.error(err);
    }
  };

  const register = async (data) => {
    try {
      let response = await registerUser(data.email, data.password);

      if (!response) {
        console.log(JSON.stringify(response))
        throw new Error("unable to register user");
      }
      navigate("/");

    }
    catch (err) {
      console.error(err);
    }
  }

  const logout = () => {
    //clear access token from state and from local storage
    setAccessTokenState("");
    clearAccessToken();

    //clear refresh token from state and from local storage
    setRefreshTokenState("");
    clearRefreshToken();

    //clear timeout id from state, local storage and current timeout
    setTimeoutIdState("");
    clearTimeout(timeoutIdState); //javascript native function
    clearAccessTokenTimeoutTaskId();

    //clear user information from state and from local storage
    setUserState(null);
    clearUserInformation();

    navigate("/");
  };

  function storeTokenData(accessToken, refreshToken) {
    setAccessTokenState(accessToken);
    setAccessToken(accessToken);

    setRefreshTokenState(refreshToken);
    setRefreshToken(refreshToken);
  }

  function setTokenRefreshInterval(accessToken, refreshToken, expiresIn) {
    console.log(expiresIn)
    if (refreshToken === "") {
      console.log("User logged out. Will not refresh token");
      return;
    }

    const timeoutId = setTimeout(async () => {
      var response = await refreshAccessToken(accessToken, refreshToken);
      storeTokenData(response.accessToken, response.refreshToken);
      setTokenRefreshInterval(response.accessToken, response.refreshToken, response.expiresIn)

      //expiresIn should be 36000 seconds
      //we will renew the token 30 minutes before expiring
    }, expiresIn * 1000 - 1800);

    setTimeoutIdState(timeoutId);
    setAccessTokenTimeoutTaskId(timeoutId);
  }

  return (
    <AuthContext.Provider value={{
      token: accessTokenState, user: userState,
      login: login, logout: logout, register: register
    }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;