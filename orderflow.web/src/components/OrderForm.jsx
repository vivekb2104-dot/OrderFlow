import { useState } from "react";
import { createOrder } from "../api/orders";
export default function OrderForm({ onOrderCreated }) {
    const [customerId, setCustomerId] = useState("");
    const [productId, setProductId] = useState("");
    const [quantity, setQuantity] = useState(1);
    const [unitPrice, setUnitPrice] = useState(0);
    const [error, setError] = useState(null);
    async function handleSubmit(e) {
        e.preventDefault();
        setError(null);
        try {
            const result = await createOrder({
                customerId,
                lines: [{ productId, quantity: Number(quantity), unitPrice: Number(unitPrice) }],
            });
            onOrderCreated(result.id);
        } catch (err) {
            setError(err.message);
        }
    }
    return (
        <form onSubmit={handleSubmit}>
            <input placeholder="Customer ID" value={customerId} onChange={e => setCustomerId(e.target.value)} required />
            <input placeholder="Product ID" value={productId} onChange={e => setProductId(e.target.value)} required />
            <input type="number" min="1" value={quantity} onChange={e => setQuantity(e.target.value)} required />
            <input type="number" step="0.01" min="0" value={unitPrice} onChange={e => setUnitPrice(e.target.value)} required />
            <button type="submit">Place Order</button>
            {error && <p style={{ color: "red" }}>{error}</p>}
        </form>
    );
}