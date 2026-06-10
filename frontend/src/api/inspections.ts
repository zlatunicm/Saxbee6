import client from './client';
import type { Inspection } from './types';

export const inspections = {
  getAll: (hiveId?: number) => client.get<Inspection[]>('/inspections', { params: { hiveId } }).then(r => r.data),
  get: (id: number) => client.get<Inspection>(`/inspections/${id}`).then(r => r.data),
  create: (data: Partial<Inspection>) => client.post('/inspections', data).then(r => r.data),
  delete: (id: number) => client.delete(`/inspections/${id}`),
};
