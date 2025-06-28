import { createContext, useContext, useState, useEffect } from "react";
import api from "../api";

const AuthContext = createContext(null);
export const useAuth = () => useContext(AuthContext);

export function AuthProvider({ children }) {
  // Initialise token from localStorage once at startup
  const [token, setToken] = useState(() => localStorage.getItem("token"));

  // Whenever the token changes, persist it and update axios defaults
  useEffect(() => {
    if (token) {
      localStorage.setItem("token", token);
      api.defaults.headers.Authorization = `Bearer ${token}`;
    } else {
      localStorage.removeItem("token");
      delete api.defaults.headers.Authorization;
    }
  }, [token]);

  const login = async (email, password) => {
    const { data } = await api.post("/auth/login", { email, password });
    setToken(data.token);
  };

  const logout = async () => {
    try {
      await api.post("/auth/logout");
    } catch (_) {}
    setToken(null);
  };

  const register = (email, password, name) =>
    api.post("/auth/register", { email, password, name });

  return (
    <AuthContext.Provider value={{ token, login, logout, register }}>
      {children}
    </AuthContext.Provider>
  );
}
