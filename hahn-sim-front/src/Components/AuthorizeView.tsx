import { useEffect, useState, createContext, useContext } from "react";
import { Navigate } from "react-router-dom";
import { API_BASE_URL } from '../config';

const UserContext = createContext({});

interface User {
    email: string
}

interface AuthorizeViewProps {
    children: React.ReactNode;
    triggerRecheck?: boolean;
}

function AuthorizeView({ children, triggerRecheck = false }: AuthorizeViewProps) {
    const [authorized, setAuthorized] = useState<boolean>(false);
    const [loading, setLoading] = useState<boolean>(true);
    const [user, setUser] = useState<User>({ email: "" });

    useEffect(() => {
        let isMounted = true;

        async function checkAuth() {
            try {
                const response = await fetch(`${API_BASE_URL}/api/user/pingauth`, {
                    method: "GET",
                    credentials: 'include',
                });

                if (response.status === 200) {
                    const data = await response.json();
                    if (isMounted) {
                        setUser({ email: data.email });
                        setAuthorized(true);
                    }
                } else {
                    if (isMounted) {
                        setAuthorized(false);
                    }
                }
            } catch (error) {
                console.error("Auth check failed:", error);
                if (isMounted) {
                    setAuthorized(false);
                }
            } finally {
                if (isMounted) {
                    setLoading(false);
                }
            }
        }

        checkAuth();

        return () => {
            isMounted = false;
        };
    }, [triggerRecheck]);

    if (loading) {
        return <p>Loading.....</p>;
    } else if (authorized) {
        return (
            <UserContext.Provider value={user}>{children}</UserContext.Provider>
        );
    } else {
        return <Navigate to="/login" />;
    }
}

export function AuthorizedUser(props: { value: string }) {
    const user: any = useContext(UserContext);
    if (props.value === "email")
        return <>{user.email}</>;
    else
        return null;
}

export default AuthorizeView;