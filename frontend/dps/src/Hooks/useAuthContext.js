import { useContext } from "react";
import { AuthContext } from "../Providers/AuthProvider.bkp.";

export const useAuthContext = () => {
    const ctx = useContext(AuthContext);
  
    if (!ctx) {
      throw new Error("useAuthContext must be within AuthProvider");
    }
  
    return ctx;
  };
  