import { useState, useEffect } from "react";
import { decodeJwt } from "jose";

export default function Home() {
    const token = localStorage.getItem("token");
    const [user, setUser] = useState(null);

    useEffect(() => {
        if (token) {
            try {
                const decoded = decodeJwt(token);
                setUser({
                    id: decoded.sub,
                    email: decoded.email,
                    name: decoded.name
                });
            } catch {
                console.warn("Invalid token");
            }
        }
    }, [token]);

    const doLogout = () => {
        localStorage.removeItem("token");
        window.location.reload();
    };

    return (
        <div>
            <h1>Welcome</h1>

            {token && user ? (
                <>
                    <p>User: {user.name} ({user.email})</p>
                    <button onClick={doLogout}>
                        Logout
                    </button>
                </>
            ) : (
                <p>
                    <a href="/auth/login">Login</a> or{" "}
                    <a href="/auth/signup">Sign Up</a>
                </p>
            )}
        </div>
    );
}
