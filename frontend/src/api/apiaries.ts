import client from './client';
import type { Apiary } from './types';

export const apiaries = {
  getAll: () => client.get<Apiary[]>('/apiaries').then(r => r.data),
  get: (id: number) => client.get<Apiary>(`/apiaries/${id}`).then(r => r.data),
  create: (data: Partial<Apiary>) => client.post('/apiaries', data).then(r => r.data),
  update: (id: number, data: Partial<Apiary>) => client.put(`/apiaries/${id}`, data),
  delete: (id: number) => client.delete(`/apiaries/${id}`),
};
