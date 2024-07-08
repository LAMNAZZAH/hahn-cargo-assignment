import { useQuery } from '@tanstack/react-query';
import { PingAuthReq } from '../Requests/UserRequests/PingauthReq';
import { useState } from 'react';
import LogoutLink from './LogoutLink';
import { AuthorizedUser } from './AuthorizeView';

function NavBar() {
    const [isDropdownOpen, setIsDropdownOpen] = useState(false);
    const [coinsCount, setCoinsCount] = useState<number>(1000)

    const { data: userEmail, isLoading: isUserEmailLoading, error: userEmailError } = useQuery<string, Error>({
        queryKey: ['userEmail'],
        queryFn: async () => {
            const email = await PingAuthReq();
            return email || "test@test.com";
        }
    });

    /* Assume we have a query for coins count
    const { data: coinsCount = 0 } = useQuery<number, Error>({
        queryKey: ['coinsCount'],
        queryFn: () => # fetch coins count 
    });
    */

    return (
        <nav className="bg-gray-100 p-4 shadow-md">
            <div className="container mx-auto flex justify-between items-center">
                <div className="text-gray-800 text-2xl font-bold">
                    hahnCargoSimulation
                </div>
                <div className="flex items-center space-x-4">
                    <div className="flex flex-col items-end mr-2">
                        <span className="text-gray-800">{userEmail}</span>
                        <span className="text-sm text-gray-600">{coinsCount} coins</span>
                    </div>
                    <div className="relative">
                        {isUserEmailLoading ? (
                            <div className="w-10 h-10 bg-gray-300 rounded-full animate-pulse"></div>
                        ) : userEmailError ? (
                            <div className="w-10 h-10 bg-red-500 rounded-full flex items-center justify-center text-white">!</div>
                        ) : (
                            <>
                                <div className="flex items-center space-x-2 cursor-pointer" onClick={() => setIsDropdownOpen(!isDropdownOpen)}>
                                    <img 
                                        src="https://via.placeholder.com/40" 
                                        alt="User" 
                                        className="w-10 h-10 rounded-full"
                                    />
                                    <svg className="w-4 h-4 text-gray-800" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 9l-7 7-7-7" /></svg>
                                </div>
                                {isDropdownOpen && (
                                    <div className="absolute right-0 mt-2 w-48 bg-white rounded-md shadow-lg py-1">
                                        <span className='block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100'><LogoutLink>logout <AuthorizedUser value="email" /></LogoutLink></span>
                                    </div>
                                )}
                            </>
                        )}
                    </div>
                </div>
            </div>
        </nav>
    );
}

export default NavBar;