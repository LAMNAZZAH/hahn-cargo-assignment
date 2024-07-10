import AuthorizeView, { AuthorizedUser } from "../Components/AuthorizeView";
import NavBar from "../Components/NavBar";

function HomePage() {
  return (
    <AuthorizeView>
      <div className="flex items-center flex-col justify-start bg-neutral-200 h-screen">
        <NavBar />
        <div id="home-page-body" className="flex justify-start flex-col items-center bg-white h-full w-screen m-3">
          <div id="first-block" className="flex justify-start items-center bg-neutral-100 w-screen h-full rounded-md m-1">
            <div id="simulation-section" className="bg-slate-200 h-full w-1/4 m-2">
              <div className="flex justify-center items-center bg-emerald-500 m-3 h-1/5 rounded-sm font-bold text-white">Start Simulation</div>
              <div className="flex justify-center items-center m-3 h-1/5 rounded-sm font-bold text-white">
                <div className="flex items-center justify-center bg-stone-500 w-full h-full mr-2">View Grid</div>
                <div className="flex items-center justify-center bg-stone-500 w-full h-full">Create Order</div>
              </div>
            </div>
            <div id="history-section" className="bg-slate-200 m-2 h-full w-full">
              <div className="flex justify-center items-center bg-slate-400 h-1/5 rounded-sm font-bold text-white">Live Actions History</div>
              <div className="bg-slate-3  00 h-36 overflow-auto gray-scrollbar">
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
              </div>
            </div>
          </div>
          <div id="second-block" className="flex justify-start items-center bg-neutral-100 w-screen h-full rounded-md m-1">
            <div id="my-transporters-section" className="bg-slate-200 h-full w-1/4 m-2">
              <div className="flex justify-center items-center bg-slate-400 h-1/6 rounded-sm font-bold text-white">My Transporters</div>
              <div className="bg-slate-3  00 h-36 overflow-auto gray-scrollbar">
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
              </div>
            </div>
            <div id="available-orders-section" className="bg-slate-200 m-2 h-full w-full">
              <div className="flex justify-center items-center bg-slate-400 h-1/6 rounded-sm font-bold text-white">Available Orders</div>
              <div className="bg-slate-3  00 h-36 overflow-auto gray-scrollbar">
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
              </div>
            </div>
          </div>
          <div id="second-block" className="flex justify-start items-center bg-neutral-100 w-screen h-full rounded-md m-1">
            <div id="available-orders-section" className="bg-slate-200 m-2 h-full w-full">
              <div className="flex justify-center items-center bg-slate-400 h-1/6 rounded-sm font-bold text-white">Accepted Orders</div>
              <div className="bg-slate-3  00 h-36 overflow-auto gray-scrollbar">
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
                <div className="flex items-center justify-center font-bold bg-slate-100 mt-1 p-2">test</div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </AuthorizeView>
  );
}

export default HomePage;