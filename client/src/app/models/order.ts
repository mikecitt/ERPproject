export interface OrderItem {
    id: number
    isDeleted: boolean

    productId: number;
    name: string;
    imagePath: string;
    price: number;
    quantity: number;
}

export interface Order {
    id: number;
    buyerId: string;
    orderDate: string;
    orderItems: OrderItem[];
    subtotal: number;
    userName: string;
    deliveryFee: number;
    orderStatus: string;
    total: number;
    fullName: string;
    address1: string;
    address2: string;
    city: string;
    state: string;
    zip: string;
    country: string;
}