import { useApi } from '../base/useApi';
import { routes } from '../routes';

let debouncedPromise;
let debouncedResolve;
let debouncedReject;
let timeout;

export const useAuthApi = () => {
  const { sendProtectedRequest, sendUnprotectedRequest } = useApi();

  const login = async (email, password) => {
    console.log("values "+email + " "+password)
    const response = await sendUnprotectedRequest("POST",
      routes.auth.login, {
      email,
      password,
    });

    return response;
  };

  const register = async (email, password) => {
    const response = await sendUnprotectedRequest("POST",
      routes.auth.register, {
      email,
      password,
    });

    return response;
  }

  const refreshTokens = async (accessToken, refreshToken, callback) => {
    clearTimeout(timeout);
    if (!debouncedPromise) {
      debouncedPromise = new Promise((resolve, reject) => {
        debouncedResolve = resolve;
        debouncedReject = reject;
      });
    }

    timeout = setTimeout(() => {
      const executeLogic = async () => {
        const response = await sendUnprotectedRequest(
          "POST",
          routes.auth.refreshToken,
          {
            "accessToken": accessToken,
            "refreshToken": refreshToken,
          }
        );

        callback(response.access_token, response.refresh_token);
      };

      executeLogic().then(debouncedResolve).catch(debouncedReject);

      debouncedPromise = null;
    }, 200);

    return debouncedPromise;
  };

  const sendAuthGuardedRequest = async (
    userIsNotAuthenticatedCallback,
    method,
    path,
    body,
    accessToken,
    refreshToken
  ) => {
    try {
      return await sendProtectedRequest(method, path, body, accessToken);
    } catch (e) {
      if (e?.status === 401) {

        try {
          await refreshTokens();
        } catch (e) {
          userIsNotAuthenticatedCallback();
          throw e;
        }

        return await sendProtectedRequest(method, path, body, accessToken);
      }

      throw e;
    }
  };

  const me = (userIsNotAuthenticatedCallback) => {
    return sendAuthGuardedRequest(
      userIsNotAuthenticatedCallback,
      "GET",
      routes.auth.me,
    )
  };

  return { login, register, me, sendAuthGuardedRequest };

}