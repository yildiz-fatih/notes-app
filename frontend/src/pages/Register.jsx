import { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";


export default function Register() {
    const navigate = useNavigate();
    const [email, setEmail] = useState("");
    const [password, setPass] = useState("");
    const [name, setName] = useState("");

    const doRegister = async () => {
        try {
            await axios.post(`http://localhost:5001/api/auth/register`, { email, password, name });
            // clear form
            setName("");
            setEmail("");
            setPass("");
            // redirect home
            navigate("/");
        } catch (e) {
            alert("Registration failed");
        }
    };

    return (
        <div>
            <h2>Sign Up</h2>
            <input placeholder="Name" value={name} onChange={e => setName(e.target.value)} />
            <input placeholder="Email" value={email} onChange={e => setEmail(e.target.value)} />
            <input placeholder="Password" type="password" value={password} onChange={e => setPass(e.target.value)} />
            <button onClick={doRegister}>Sign Up</button>
        </div>
    );
}
