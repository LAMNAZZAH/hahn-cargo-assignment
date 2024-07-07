import { useEffect, useState, createContext, useContext } from "react";
import { Navigate } from "react-router-dom";
import { API_BASE_URL } from '../config';

const UserContext = createContext({});

interface User {
    email: string
}

function AuthorizeView(props: { children: React.ReactNode }) {
    const [authorized, setAuthorized] = useState<boolean>(false);
    const [loading, setLoading] = useState<boolean>(true);

    const emptyuser: User = { email: "" };

    const [user, setUser] = useState(emptyuser);

    useEffect(() => {
        let retriesCount = 0;
        const maxRetries = 10;
        const delay: number = 1000;

        function wait(delay: number) {
            return new Promise((resolve) => setTimeout(resolve, delay));
        }

        async function fetchWithRetry(url: string, options: any): Promise<Response> {
            try {
                const response = await fetch(url, options);

                if (response.status == 200) {
                    console.log("Authorized");
                    const j: any = await response.json();
                    setUser({ email: j.email });
                    setAuthorized(true);
                    return response;
                } else if (response.status == 401) {
                    console.log("Unauthorized!");
                    return response;
                } else {
                    await wait(delay);
                    return fetchWithRetry(url, options);
                }
            } catch (err) {
                retriesCount++;
                if (retriesCount > maxRetries) {
                    throw err
                } else {
                    await wait(delay);
                    return fetchWithRetry(url, options);
                }
            }
        }

        fetchWithRetry(`${API_BASE_URL}/api/user/pingauth`, {
            method: "GET"
        })
        .catch(err => {
            console.log(err.message);
        })
        .finally(() => {
            setLoading(false);
        });

    }, [])

    if (loading) {
        return (
            <>
            <p>Loading.....</p>
            </>
        )
    } else {
        if (authorized && !loading) {
            return (
                <>
                <UserContext.Provider value={user}>{props.children}</UserContext.Provider>
                </>
            )
        } else {
            return (
                <>
                <Navigate to="/login" />
                </>
            )
        }
    } 
    
    
}


export function AuthorizedUser(props: { value: string }) {
    const user: any = useContext(UserContext);
    if (props.value == "email")
        return <>{user.email}</>;
    else
        return <></>
}


export default AuthorizeView;