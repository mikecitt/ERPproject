import axios, { AxiosError, AxiosResponse } from "axios";
import { PaginatedResponse } from "../models/pagination";
import { router } from "../router/Routes";

axios.defaults.baseURL = 'https://localhost:7189/api/'
axios.defaults.withCredentials = true;
const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.request.use((request) => {
  console.log(`${request.method}: ${request.url}`);
  if (request.params) console.log(`params:${request.params}`);
  if (request.data) console.log(`data:${JSON.stringify(request.data)}`);

  try {

    const user = JSON.parse(localStorage.getItem('user')!);
    const token = user.token;
    if (token) request.headers.Authorization = `Bearer ${token}`;

  } catch (error) {
    console.log(error);
  }

  return request;
});



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
      break;
    case 401:
      break;
    case 404:
      break;
    case 500:
      router.navigate('/server-error', {state: {error: data}});
      break;
    default:
      break;
  }
  return Promise.reject(error.response);
})

function createFormData(item: any) {
  let formData = new FormData();
  for (const key in item) {
      formData.append(key, item[key])
  }
  return formData;
}


const requests = {
  get: (url: string, params?: URLSearchParams) => axios.get(url, { params }).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  delete: (url: string) => axios.delete(url).then(responseBody),
  postForm: (url: string, data: FormData) => axios.post(url, data, {
    headers: { 'Content-type': 'multipart/form-data' }
  }).then(responseBody),
  putForm: (url: string, data: FormData) => axios.put(url, data, {
    headers: { 'Content-type': 'multipart/form-data' }
  }).then(responseBody)
}

const Catalog = {
  list: (params: URLSearchParams) => requests.get('product', params),
  details: (id: number) => requests.get(`product/${id}`)
}


const Account = {
  login: (values: any) => requests.post('authentication/login', values),
  register: (values: any) => requests.post('authentication/register', values),
  currentUser: () => requests.get('authentication/currentUser'),
  profile: () => requests.get('user/profile'),
  getAll: () => requests.get('user'),
  delete: (id: number) => requests.delete(`user/${id}`),
  deletePersonal: () => requests.delete('user/profile')
}

const Category = {
  get: () => requests.get('category')
}

const Orders = {
  list: () => requests.get('order'),
  fetch: (id: number) => requests.get(`order/${id}`),
  create: (values: any) => requests.post('order', values),
  all: () => requests.get('order/allOrders')
}

const Payments = {
  createPaymentIntent: () => requests.post('payment', {})
}


const Basket = {
  get: () => requests.get('cart'),
  addItem: (productId: number, quantity = 1) => requests.post(`cart?productId=${productId}&quantity=${quantity}`, {}),
  removeItem: (productId: number, quantity = 1) => requests.delete(`cart?productId=${productId}&quantity=${quantity}`)

}

const Admin = {
  createProduct: (product: any) => requests.postForm('product', createFormData(product)),
  updateProduct: (product: any) => requests.putForm('product', createFormData(product)),
  deleteProduct: (id: number) => requests.delete(`product/${id}`)
}

const agent = {
  Catalog,
  Basket,
  Account,
  Orders,
  Payments,
  Admin,
  Category
}

export default agent;