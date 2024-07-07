import AuthorizeView, { AuthorizedUser } from "../Components/AuthorizeView";
import LogoutLink from "../Components/LogoutLink";
import WeatherForcast from "../Components/WeatherForcast";

function HomePage() {
  return (
      <AuthorizeView>
          <span><LogoutLink>logout <AuthorizedUser value="email" /></LogoutLink></span>
      <WeatherForcast />
      </AuthorizeView>
  );
}

export default HomePage;