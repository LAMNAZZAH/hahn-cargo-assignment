
import { useNavigate } from "react-router-dom";
import { LogoutReq } from "../Requests/UserRequests/LogoutReq";

function LogoutLink(props: { children: React.ReactNode }) {

    const navigate = useNavigate();


    const handleSubmit = (e: React.FormEvent<HTMLAnchorElement>) => {
        e.preventDefault();
        LogoutReq().then(data => {
            if (data?.success === true) {
                navigate("/login");
            }
        }).catch(err => {
            console.log(err)
        })
    };

    return (
        <>
            <a href="#" onClick={handleSubmit}>{props.children}</a>
        </>
    );
}

export default LogoutLink;