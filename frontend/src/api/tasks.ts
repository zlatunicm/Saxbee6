import client from './client';
import type { BeeTask } from './types';

export const tasks = {
  getAll: (completed?: boolean) => client.get<BeeTask[]>('/tasks', { params: { completed } }).then(r => r.data),
  create: (data: Partial<BeeTask>) => client.post('/tasks', data).then(r => r.data),
  update: (id: number, data: Partial<BeeTask>) => client.put(`/tasks/${id}`, data),
  toggleComplete: (id: number) => client.patch(`/tasks/${id}/complete`),
  delete: (id: number) => client.delete(`/tasks/${id}`),
};
