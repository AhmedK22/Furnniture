import axios from 'axios';
import { Product } from '../models/types';

const api = axios.create({
  baseURL: 'https://localhost:7200/api', // Update this if your backend uses a different port
});

// GET all products
export const getProducts = async (): Promise<Product[]> => {
  const response = await api.get('/products');
  return response.data;
};

// POST a new product
export const createProduct = async (product: Product): Promise<Product> => {
  const response = await api.post('/products', product);
  return response.data;
};

// PUT update a product
export const updateProduct = async (product: Product): Promise<Product> => {
  const response = await api.put(`/products/${product.id}`, product);
  return response.data;
};

// DELETE a product
export const deleteProduct = async (id: string): Promise<void> => {
  await api.delete(`/products/${id}`);
};
