export interface Basket {
    id: number
    isDeleted: boolean
    buyerId: string
    items: BasketItem[]
    paymentIntentId?: string;
    clientSecret?: string;
  }
  
  export interface BasketItem {
    id: number
    isDeleted: boolean
    productId: number
    price: number
    name : string
    imagePath: string
    quantity: number
  }
  