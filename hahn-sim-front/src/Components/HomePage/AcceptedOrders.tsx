import { useQuery } from "@tanstack/react-query";
import { FetchAcceptedOrdersReq } from "../../Requests/FetchAcceptedOrdersReq";
import { Order } from "../../Types/Order";
import { isDeliveryDatePassed } from "../../Utils/isDeliveryDatePassed";

const AcceptedOrders = () => {
    const { data: acceptedOrders, isLoading: isAcceptedOrdersLoading, error: acceptedOrdersError } = useQuery<Order[], Error>({
        queryKey: ['acceptedOrders'],
        queryFn: async () => {
            const acceptedOrders = await FetchAcceptedOrdersReq();
            return acceptedOrders;
        },
        refetchInterval: 10000
    });

    return (
        <>
            <div className="flex justify-center items-center bg-slate-400 h-1/6 rounded-sm font-bold text-white">Accepted Orders</div>
            <div className="bg-slate-300 h-36 overflow-auto gray-scrollbar">
                <table className="w-full bg-red-50 border-collapse">
                    <thead className="w-full bg-stone-100">
                        <tr>
                            <th className="p-2 text-center text-stone-600">ID</th>
                            <th className="p-2 text-center text-stone-600">Origin</th>
                            <th className="p-2 text-center text-stone-600">Target</th>
                            <th className="p-2 text-center text-stone-600">Load</th>
                            <th className="p-2 text-center text-stone-600">Value</th>
                            <th className="p-2 text-center text-stone-600">Delivery</th>
                            <th className="p-2 text-center text-stone-600">Expiration</th>
                        </tr>
                    </thead>
                    <tbody className="w-full">
                        {acceptedOrders?.map((order, index) => {
                            const isLate = isDeliveryDatePassed(order.deliveryDateUtc);
                            return (
                                <tr 
                                    key={order.id} 
                                    className={`w-full ${
                                        isLate 
                                        ? 'bg-red-300' 
                                        : index % 2 === 0 ? 'bg-slate-200' : 'bg-slate-100'
                                    }`}
                                >
                                    <td className="p-1 text-center text-slate-800 font-medium">{order.id}</td>
                                    <td className="p-1 text-center text-slate-800 font-medium">{order.originNodeId}</td>
                                    <td className="p-1 text-center text-slate-800 font-medium">{order.targetNodeId}</td>
                                    <td className="p-1 text-center text-slate-800 font-medium">{order.load}</td>
                                    <td className="p-1 text-center text-slate-800 font-medium">{order.value}</td>
                                    <td className="p-1 text-center text-slate-800 font-medium">{order.deliveryDateUtc.toString()}</td>
                                    <td className="p-1 text-center text-slate-800 font-medium">{order.expirationDateUtc.toString()}</td>
                                </tr>
                            );
                        })}
                    </tbody>
                </table>
            </div>
        </>
    );
}

export default AcceptedOrders;