
import { useNavigate } from "react-router-dom";
import { API_BASE_URL } from '../config';

function LogoutLink(props: { children: React.ReactNode }) {

    const navigate = useNavigate();


    const handleSubmit = (e: React.FormEvent<HTMLAnchorElement>) => {
        e.preventDefault();
        fetch(`${API_BASE_URL}/api/user/logout`, {
            method: "GET",
            credentials: "include"
        })
            .then((data) => {
                if (data.ok) {
                    navigate("/login");
                }
                else { }


            })
            .catch((error) => {
                console.error(error);
            })

    };

    return (
        <>
            <a href="#" onClick={handleSubmit}>{props.children}</a>
        </>
    );
}

export default LogoutLink;