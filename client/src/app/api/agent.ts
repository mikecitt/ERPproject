import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { history } from "../..";
import { PaginatedResponse } from "../models/pagination";
import {  store } from "../store/configureStore";

axios.defaults.baseURL = 'https://localhost:7189/api/'
axios.defaults.withCredentials = true;
const responseBody = (response: AxiosResponse) => response.data;



const sleep = () => new Promise(resolve => setTimeout(resolve, 1000));



axios.interceptors.response.use(async response => {
  await sleep();
  const pagination = response.headers['pagination'];
  if (pagination) {
    response.data = new PaginatedResponse(response.data, JSON.parse(pagination));
    return response;
  }
  return response
}, (error: AxiosError) => {

  const { data, status } = error.response!;
  switch (status) {
    case 400:
      toast.error((data as { title: string }).title);
      break;
    case 401:
      toast.error((data as { title: string }).title);
      break;
    case 404:
      toast.error((data as { title: string }).title);
      break;
    case 500:
      history.push({
        pathname: '/server-error',
        state: { error: data }
      });
      break;
    default:
      break;
  }
  return Promise.reject(error.response);
})

const requests = {
  get: (url: string, params?: URLSearchParams) => axios.get(url, { params }).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  delete: (url: string) => axios.delete(url).then(responseBody),
}

const Catalog = {
  list: (params: URLSearchParams) => requests.get('product', params),
  details: (id: number) => requests.get(`product/${id}`)
}


const Account = {
  login: (values: any) => requests.post('authentication/login', values),
  register: (values: any) => requests.post('authentication/register', values),
  currentUser: () => requests.get('authentication/currentUser')
}





const Basket = {
  get: () => requests.get('cart'),
  addItem: (productId: number, quantity = 1) => requests.post(`cart?productId=${productId}&quantity=${quantity}`, {}),
  removeItem: (productId: number, quantity = 1) => requests.delete(`cart?productId=${productId}&quantity=${quantity}`)

}


const agent = {
  Catalog,
  Basket,
  Account
}

export default agent;