import client from './client';
import type { Hive, HiveSummary } from './types';

export const hives = {
  getAll: (apiaryId?: number) => client.get<HiveSummary[]>('/hives', { params: { apiaryId } }).then(r => r.data),
  get: (id: number) => client.get<Hive>(`/hives/${id}`).then(r => r.data),
  create: (data: Partial<Hive>) => client.post('/hives', data).then(r => r.data),
  update: (id: number, data: Partial<Hive>) => client.put(`/hives/${id}`, data),
  delete: (id: number) => client.delete(`/hives/${id}`),
};
