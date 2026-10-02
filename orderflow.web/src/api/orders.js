const BASE_URL = import.meta.env.VITE_API_BASE_URL;
export async function createOrder(order) {
    const response = await fetch(`${BASE_URL}/orders`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(order),
    });
    if (!response.ok) throw new Error("Failed to create order");
    return response.json();
}
export async function getOrder(id) {
    const response = await fetch(`${BASE_URL}/orders/${id}`);
    if (!response.ok) throw new Error("Order not found");
    return response.json();
}