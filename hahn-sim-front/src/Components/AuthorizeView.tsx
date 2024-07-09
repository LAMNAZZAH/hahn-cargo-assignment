import { useEffect, useState, createContext, useContext } from "react";
import { Navigate } from "react-router-dom";
import { PingAuthReq } from "../Requests/UserRequests/PingauthReq";
import { useNavigate } from "react-router-dom";

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

    const navigate = useNavigate();

    useEffect(() => {
        let isMounted = true;

        async function checkAuth() {
            try {
                const email = await PingAuthReq()
                if (isMounted) {
                    setUser({ email: email });
                    setAuthorized(true);
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