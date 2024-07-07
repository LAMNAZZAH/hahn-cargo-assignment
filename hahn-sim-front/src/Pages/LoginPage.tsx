import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { API_BASE_URL } from '../config';

function LoginPage() {
    const [email, setEmail] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [rememberme, setRememberme] = useState<boolean>(false);
    const [error, setError] = useState<string>("");
    const navigate = useNavigate();

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        if (name === "email") setEmail(value);
        if (name === "password") setPassword(value);
        if (name === "rememberme") setRememberme(e.target.checked);
    };

    const handleRegisterClick = () => {
        navigate("/register");
    }

    const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        if (!email || !password) {
            setError("Please fill in all fields.");
            return;
        }

        setError("");

        let loginurl = rememberme
            ? `${API_BASE_URL}/login?useCookies=true`
            : `${API_BASE_URL}/login?useSessionCookies=true`;

        try {
            const response = await fetch(loginurl, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    email: email,
                    password: password,
                    twoFactorCode: "string",
                    twoFactorRecoveryCode: "string"
                }),
                credentials: 'include',
            });

            if (response.ok) {
                console.log("Login successful");
                setError("Successful Login.");
                navigate('/');
            } else {
                const errorData = await response.json();
                console.error("Login failed:", errorData);
                setError(errorData.message || "Error Logging In.");
            }
        } catch (error) {
            console.error("Error during login:", error);
            setError("Error Logging in.");
        }
    };

    return (
        <div className="flex items-center justify-center min-h-screen bg-gray-100">
            <div className="w-full max-w-md p-8 space-y-6 bg-white rounded-lg shadow-md">
                <h1 className="text-3xl font-bold text-center text-gray-800">Login</h1>
                <form onSubmit={handleSubmit} className="space-y-4">
                    <div>
                        <label className="block text-sm font-medium text-gray-700" htmlFor="email">Email:</label>
                        <input
                            type="email"
                            id="email"
                            name="email"
                            value={email}
                            onChange={handleChange}
                            className="w-full px-3 py-2 mt-1 text-gray-700 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                        />
                    </div>
                    <div>
                        <label className="block text-sm font-medium text-gray-700" htmlFor="password">Password:</label>
                        <input
                            type="password"
                            id="password"
                            name="password"
                            value={password}
                            onChange={handleChange}
                            className="w-full px-3 py-2 mt-1 text-gray-700 border rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
                        />
                    </div>
                    <div className="flex items-center">
                        <input
                            type="checkbox"
                            id="rememberme"
                            name="rememberme"
                            checked={rememberme}
                            onChange={handleChange}
                            className="w-4 h-4 text-blue-600 border-gray-300 rounded focus:ring-blue-500"
                        />
                        <label htmlFor="rememberme" className="ml-2 text-sm text-gray-600">Remember Me</label>
                    </div>
                    <div className="space-y-2">
                        <button
                            type="submit"
                            className="w-full px-4 py-2 text-white bg-blue-600 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
                        >
                            Login
                        </button>
                        <button
                            onClick={handleRegisterClick}
                            className="w-full px-4 py-2 text-blue-600 bg-transparent border border-blue-600 rounded-md hover:bg-blue-50 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
                        >
                            Register
                        </button>
                    </div>
                </form>
                {error && <p className="text-sm text-center text-red-600">{error}</p>}
            </div>
        </div>
    );
}

export default LoginPage;
