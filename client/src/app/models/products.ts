export interface Product
 {
    productName: string
    price: number
    description: string
    imagePath: string
    stockStatus: boolean
    categoryId: number
    id: number
    isDeleted: boolean
  }
  
  export interface ProductParams {
    orderBy: string;
    searchTerm?: string;
    pageNumber: number;
    pageSize: number;
}