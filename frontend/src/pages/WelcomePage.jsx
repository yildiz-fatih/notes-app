import { Navigate, useNavigate } from "react-router-dom";
import { useAuth } from "../contexts/AuthContext";

export default function WelcomePage() {
  const { token } = useAuth();
  const nav = useNavigate();

  if (token) return <Navigate to="/notes" replace />;

  return (
    <div className="d-flex flex-column justify-content-center align-items-center vh-100 bg-light">
      <h1 className="mb-4 fw-normal">NotesApp</h1>
      <div className="d-flex gap-2">
        <button className="btn btn-primary" onClick={() => nav("/login")}>
          Log In
        </button>
        <button
          className="btn btn-outline-primary"
          onClick={() => nav("/register")}
        >
          Register
        </button>
      </div>
    </div>
  );
}
