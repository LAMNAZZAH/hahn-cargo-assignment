import { useQuery } from "@tanstack/react-query";
import { FetchAvailableOrdersReq } from "../../Requests/FetchAvailableOrdersReq";
import { Order } from "../../Types/Order";
import { isDeliveryDatePassed } from "../../Utils/isDeliveryDatePassed";

const AvailableOrders = () => {
    const { data: availableOrders, isLoading: isAvailableOrdersLoading, error: availableOrdersError } = useQuery<Order[], Error>({
        queryKey: ['availableOrders'],
        queryFn: async () => {
            const availableOrders = await FetchAvailableOrdersReq();
            return availableOrders || ["No Available Orders Found"]
        },
        refetchInterval: 10000
    });

    return (
        <>
            <div className="flex justify-center items-center bg-slate-400 h-1/6 rounded-sm font-bold text-white">Available Orders</div>
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
                        {availableOrders?.map((order, index) => {
                            const isLate = isDeliveryDatePassed(order.deliveryDateUtc);
                            return (
                                <tr 
                                    key={order.id} 
                                    className={`w-full ${
                                        isLate 
                                        ? index % 2 === 0 ? 'bg-red-300' : 'bg-red-200'
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

export default AvailableOrders;