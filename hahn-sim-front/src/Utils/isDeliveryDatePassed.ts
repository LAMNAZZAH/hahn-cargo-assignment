export const isDeliveryDatePassed = (deliveryDateUtc: string) => {
    const deliveryDate = new Date(deliveryDateUtc);
    const currentDate = new Date();
    return deliveryDate < currentDate;
}