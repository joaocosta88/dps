import { useAuthContext } from "../../Hooks/base/useAuthContext";
import buildQueryParams from "../../utils/buildQueryParams";

export const useUserApi = () => {
    const { sendAuthGuardedRequest } = useAuthContext();
  
    const findAllUsers = async (
      limit,
      offset,
    ) => {
      const queryString = buildQueryParams([
        { key: "limit", value: limit.toString() },
        { key: "offset", value: offset.toString() },
      ]);
  
      return sendAuthGuardedRequest(
        "GET",
        "routes.user.findAll + queryString",
      );
    };
  
    const findOneUser = async (id) => {
      return sendAuthGuardedRequest("GET", "routes.user.findOne(id)");
    };
  
    return { findAllUsers, findOneUser };
  };
  