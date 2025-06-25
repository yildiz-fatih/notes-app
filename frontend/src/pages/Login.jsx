import { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

export default function Login() {
    const navigate = useNavigate();
    const [email, setEmail] = useState("");
    const [password, setPass] = useState("");

    const doLogin = async () => {
        try {
            const { data } = await axios.post(`http://localhost:5001/api/auth/login`, { email, password });
            localStorage.setItem("token", data.token);
            navigate("/");
        } catch {
            alert("Login failed");
        }
    };

    return (
        <div>
            <h2>Login</h2>
            <input placeholder="Email" value={email} onChange={e => setEmail(e.target.value)} />
            <input placeholder="Password" type="password" value={password} onChange={e => setPass(e.target.value)} />
            <button onClick={doLogin}>Login</button>
        </div>
    );
}
