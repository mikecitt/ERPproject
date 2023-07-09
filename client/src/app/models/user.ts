import { Basket } from "./basket";

export interface User{
    email: string,
    token: string,
    basket?: Basket;
    roles?: string[];
}

export interface SiteUser
{
    username: string,
    email: string,
    firstName : string,
    lastName: string,
    phone: string
}