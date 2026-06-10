import client from './client';
import type { Harvest } from './types';

export const harvests = {
  getAll: (hiveId?: number) => client.get<Harvest[]>('/harvests', { params: { hiveId } }).then(r => r.data),
  create: (data: Partial<Harvest>) => client.post('/harvests', data).then(r => r.data),
  delete: (id: number) => client.delete(`/harvests/${id}`),
  stats: () => client.get('/harvests/stats').then(r => r.data),
};
