import AcceptedOrders from "../Components/HomePage/AcceptedOrders";
import AuthorizeView, { AuthorizedUser } from "../Components/AuthorizeView";
import AvailableOrders from "../Components/HomePage/AvailableOrders";
import LiveActionHistory from "../Components/HomePage/LiveActionHistory";
import MyTransporters from "../Components/HomePage/MyTransporters";
import NavBar from "../Components/NavBar";
import SimulationControllers from "../Components/HomePage/SimulationControllers";

function HomePage() {
  return (
    <AuthorizeView>
      <div className="flex items-center flex-col justify-start bg-neutral-200 h-screen">
        <NavBar />
        <div id="home-page-body" className="flex justify-start flex-col items-center bg-white h-full w-screen m-3">
          <div id="first-block" className="flex justify-start items-center bg-neutral-100 w-screen h-full rounded-md m-1">
            <div id="simulation-section" className="bg-slate-200 h-full w-1/4 m-2">
              <SimulationControllers />
            </div>
            <div id="history-section" className="bg-slate-200 m-2 h-full w-full">
              <LiveActionHistory />
            </div>
          </div>
          <div id="second-block" className="flex justify-start items-center bg-neutral-100 w-screen h-full rounded-md m-1">
            <div id="my-transporters-section" className="bg-slate-200 h-full w-1/4 m-2">
              <MyTransporters />
            </div>
            <div id="available-orders-section" className="bg-slate-200 m-2 h-full w-full">
              <AvailableOrders />
            </div>
          </div>
          <div id="third-block" className="flex justify-start items-center bg-neutral-100 w-screen h-full rounded-md m-1">
            <div id="available-orders-section" className="bg-slate-200 m-2 h-full w-full">
              <AcceptedOrders />
            </div>
          </div>
        </div>
      </div>
    </AuthorizeView>
  );
}

export default HomePage;