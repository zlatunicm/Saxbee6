import client from './client';
import type { Treatment } from './types';

export const treatments = {
  getAll: (hiveId?: number) => client.get<Treatment[]>('/treatments', { params: { hiveId } }).then(r => r.data),
  create: (data: Partial<Treatment>) => client.post('/treatments', data).then(r => r.data),
  delete: (id: number) => client.delete(`/treatments/${id}`),
};
