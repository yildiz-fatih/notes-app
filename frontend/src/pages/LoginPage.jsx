import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../contexts/AuthContext";

export default function LoginPage() {
  const { login } = useAuth();
  const nav = useNavigate();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await login(email, password);
      nav("/notes");
    } catch {
      setError("Invalid credentials");
    }
  };

  return (
    <div className="d-flex align-items-center justify-content-center vh-100 bg-light">
      <form
        className="card shadow-sm p-4 w-100"
        style={{ maxWidth: 420 }}
        onSubmit={handleSubmit}
      >
        <h3 className="mb-3 text-center">Sign in</h3>

        {error && <div className="alert alert-danger py-1">{error}</div>}

        <input
          type="email"
          className="form-control mb-2"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
        <input
          type="password"
          className="form-control mb-3"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />

        <button className="btn btn-primary w-100 mb-2" type="submit">
          Sign in
        </button>

        <p className="text-center small mb-0">
          Need an account? <Link to="/register">Register</Link>
        </p>
      </form>
    </div>
  );
}
