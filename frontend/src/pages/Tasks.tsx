import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { useState } from 'react';
import { tasks } from '../api/tasks';
import type { TaskPriority } from '../api/types';
import { Plus, Check, Trash2 } from 'lucide-react';

const priorityColor: Record<TaskPriority, string> = { High: '#ef4444', Medium: '#f59e0b', Low: '#6b7280' };

export default function Tasks() {
  const qc = useQueryClient();
  const [showDone, setShowDone] = useState(false);
  const [showAdd, setShowAdd] = useState(false);
  const [form, setForm] = useState({ title: '', description: '', priority: 'Medium' as TaskPriority, dueDate: '' });

  const { data = [] } = useQuery({ queryKey: ['tasks', showDone], queryFn: () => tasks.getAll(showDone ? undefined : false) });

  const createMutation = useMutation({ mutationFn: tasks.create, onSuccess: () => { qc.invalidateQueries({ queryKey: ['tasks'] }); setShowAdd(false); } });
  const toggleMutation = useMutation({ mutationFn: tasks.toggleComplete, onSuccess: () => qc.invalidateQueries({ queryKey: ['tasks'] }) });
  const deleteMutation = useMutation({ mutationFn: tasks.delete, onSuccess: () => qc.invalidateQueries({ queryKey: ['tasks'] }) });

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 28 }}>
        <div>
          <h1 style={{ fontSize: 26, fontWeight: 700, marginBottom: 4 }}>Tasks</h1>
          <label style={{ fontSize: 13, color: '#888', display: 'flex', gap: 6, alignItems: 'center', cursor: 'pointer' }}>
            <input type="checkbox" checked={showDone} onChange={e => setShowDone(e.target.checked)} />
            Show completed
          </label>
        </div>
        <button onClick={() => setShowAdd(true)} style={{ display: 'flex', alignItems: 'center', gap: 8, background: '#f5c842', border: 'none', borderRadius: 8, padding: '0.6rem 1.2rem', fontWeight: 600, cursor: 'pointer', fontSize: 14 }}>
          <Plus size={16} /> New Task
        </button>
      </div>

      <div style={{ display: 'flex', flexDirection: 'column', gap: 10 }}>
        {data.map(t => (
          <div key={t.id} style={{ background: '#fff', borderRadius: 10, padding: '1rem 1.25rem', boxShadow: '0 1px 3px rgba(0,0,0,0.05)', display: 'flex', alignItems: 'center', gap: 14, opacity: t.isCompleted ? 0.6 : 1 }}>
            <button onClick={() => toggleMutation.mutate(t.id)} style={{ border: `2px solid ${t.isCompleted ? '#10b981' : '#ddd'}`, borderRadius: 6, width: 24, height: 24, display: 'flex', alignItems: 'center', justifyContent: 'center', cursor: 'pointer', background: t.isCompleted ? '#10b981' : '#fff', flexShrink: 0 }}>
              {t.isCompleted && <Check size={12} color="#fff" strokeWidth={3} />}
            </button>
            <div style={{ flex: 1 }}>
              <div style={{ fontWeight: 500, textDecoration: t.isCompleted ? 'line-through' : 'none' }}>{t.title}</div>
              {t.description && <div style={{ fontSize: 12, color: '#888' }}>{t.description}</div>}
            </div>
            <div style={{ display: 'flex', gap: 10, alignItems: 'center', flexShrink: 0 }}>
              {t.dueDate && <span style={{ fontSize: 12, color: '#888' }}>{new Date(t.dueDate).toLocaleDateString()}</span>}
              <span style={{ fontSize: 11, color: priorityColor[t.priority], background: priorityColor[t.priority] + '20', padding: '2px 8px', borderRadius: 12 }}>{t.priority}</span>
              <button onClick={() => deleteMutation.mutate(t.id)} style={{ border: 'none', background: 'none', cursor: 'pointer', color: '#ddd' }}><Trash2 size={14} /></button>
            </div>
          </div>
        ))}
        {data.length === 0 && <p style={{ color: '#aaa', textAlign: 'center', marginTop: 40 }}>No tasks here.</p>}
      </div>

      {showAdd && (
        <div style={{ position: 'fixed', inset: 0, background: 'rgba(0,0,0,0.4)', display: 'flex', alignItems: 'center', justifyContent: 'center', zIndex: 100 }}>
          <div style={{ background: '#fff', borderRadius: 14, padding: '2rem', minWidth: 400 }}>
            <h2 style={{ marginBottom: 20, fontWeight: 600 }}>New Task</h2>
            <div style={{ marginBottom: 14 }}>
              <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>Title*</label>
              <input value={form.title} onChange={e => setForm(f => ({ ...f, title: e.target.value }))}
                style={{ width: '100%', padding: '0.5rem 0.75rem', borderRadius: 8, border: '1px solid #ddd', fontSize: 14, boxSizing: 'border-box' }} />
            </div>
            <div style={{ marginBottom: 14 }}>
              <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>Description</label>
              <textarea value={form.description} onChange={e => setForm(f => ({ ...f, description: e.target.value }))} rows={2}
                style={{ width: '100%', padding: '0.5rem 0.75rem', borderRadius: 8, border: '1px solid #ddd', fontSize: 14, resize: 'vertical', boxSizing: 'border-box' }} />
            </div>
            <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 14, marginBottom: 14 }}>
              <div>
                <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>Priority</label>
                <select value={form.priority} onChange={e => setForm(f => ({ ...f, priority: e.target.value as TaskPriority }))}
                  style={{ width: '100%', padding: '0.45rem', borderRadius: 8, border: '1px solid #ddd', fontSize: 13 }}>
                  <option>Low</option><option>Medium</option><option>High</option>
                </select>
              </div>
              <div>
                <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>Due Date</label>
                <input type="date" value={form.dueDate} onChange={e => setForm(f => ({ ...f, dueDate: e.target.value }))}
                  style={{ width: '100%', padding: '0.45rem', borderRadius: 8, border: '1px solid #ddd', fontSize: 13, boxSizing: 'border-box' }} />
              </div>
            </div>
            <div style={{ display: 'flex', gap: 12 }}>
              <button onClick={() => setShowAdd(false)} style={{ flex: 1, border: '1px solid #ddd', borderRadius: 8, padding: '0.6rem', cursor: 'pointer', background: '#fff' }}>Cancel</button>
              <button onClick={() => createMutation.mutate({ title: form.title, description: form.description, priority: form.priority, dueDate: form.dueDate || undefined })}
                style={{ flex: 1, background: '#f5c842', border: 'none', borderRadius: 8, padding: '0.6rem', fontWeight: 600, cursor: 'pointer' }}>Create</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
