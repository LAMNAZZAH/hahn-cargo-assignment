import AuthorizeView, { AuthorizedUser } from "../Components/AuthorizeView";
import LogoutLink from "../Components/LogoutLink";
import NavBar from "../Components/NavBar";

function HomePage() {
  return (
      <AuthorizeView>
      <NavBar />
      </AuthorizeView>
  );
}

export default HomePage;