import { useMutation } from "@tanstack/react-query";
import { CreateOrderReq } from "../../Requests/CreateOrderReq";
import { useState } from "react";

const SimulationControllers = () => {
    const [orderCreated, setOrderCreated] = useState(false);

    const createOrderMutation = useMutation({
        mutationFn: CreateOrderReq,
        onSuccess: (data) => {
            setOrderCreated(data);
        },
        onError: (error) => {
            console.error("Error creating order:", error);
            setOrderCreated(false);
        }
    });

    const handleCreateOrder = () => {
        createOrderMutation.mutate();
    };

    return (
        <>
            <div className="flex justify-center items-center bg-emerald-500 m-3 h-1/5 rounded-sm font-bold text-white">
                Start Simulation
            </div>
            <div className="flex justify-center items-center m-3 h-1/5 rounded-sm font-bold text-white">
                <div className="flex items-center justify-center bg-stone-500 w-full h-full mr-2">
                    View Grid
                </div>
                <div 
                    className="flex items-center justify-center bg-stone-500 w-full h-full cursor-pointer"
                    onClick={handleCreateOrder}
                >
                    Create Order
                </div>
            </div>
            {createOrderMutation.isPending && <p className="w-full bg-stone-100 text-center"><span className="text-stone-800">Creation Pending...</span></p>}
            {createOrderMutation.isError && <p className="w-full bg-stone-100 text-center"><span className="text-stone-800">Error Creating Order</span></p>}
            {createOrderMutation.isSuccess && (
                <p className="w-full bg-stone-100 text-center">{orderCreated ? <span className="text-green-600">Order created successfully</span> : <span className="text-red-400">Error Creating Order</span>}</p>
            )}
        </>
    );
}

export default SimulationControllers;