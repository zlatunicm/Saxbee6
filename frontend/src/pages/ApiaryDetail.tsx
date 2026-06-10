import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { useParams, Link } from 'react-router-dom';
import { useState } from 'react';
import { apiaries } from '../api/apiaries';
import { hives } from '../api/hives';
import type { HiveType } from '../api/types';
import { Plus, ArrowLeft, Layers, Crown, Clock } from 'lucide-react';

const hiveStatusColor: Record<string, string> = { Active: '#10b981', Inactive: '#6b7280', Dead: '#ef4444', Swarmed: '#f59e0b' };

export default function ApiaryDetail() {
  const { id } = useParams<{ id: string }>();
  const apiaryId = Number(id);
  const qc = useQueryClient();
  const [showAdd, setShowAdd] = useState(false);
  const [form, setForm] = useState({ name: '', type: 'Langstroth' as HiveType, color: '', description: '' });

  const { data: apiary } = useQuery({ queryKey: ['apiary', apiaryId], queryFn: () => apiaries.get(apiaryId) });
  const { data: hiveList = [] } = useQuery({ queryKey: ['hives', apiaryId], queryFn: () => hives.getAll(apiaryId) });

  const createMutation = useMutation({
    mutationFn: hives.create,
    onSuccess: () => { qc.invalidateQueries({ queryKey: ['hives', apiaryId] }); qc.invalidateQueries({ queryKey: ['apiaries'] }); setShowAdd(false); },
  });

  return (
    <div>
      <Link to="/apiaries" style={{ display: 'flex', alignItems: 'center', gap: 6, color: '#888', fontSize: 13, marginBottom: 20, textDecoration: 'none' }}>
        <ArrowLeft size={14} /> Back to Apiaries
      </Link>
      <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 28 }}>
        <div>
          <h1 style={{ fontSize: 26, fontWeight: 700, marginBottom: 4 }}>{apiary?.name}</h1>
          {apiary?.location && <p style={{ color: '#888', fontSize: 13 }}>{apiary.location}</p>}
        </div>
        <button onClick={() => setShowAdd(true)} style={{ display: 'flex', alignItems: 'center', gap: 8, background: '#f5c842', border: 'none', borderRadius: 8, padding: '0.6rem 1.2rem', fontWeight: 600, cursor: 'pointer', fontSize: 14 }}>
          <Plus size={16} /> Add Hive
        </button>
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(260px, 1fr))', gap: 18 }}>
        {hiveList.map(h => (
          <Link key={h.id} to={`/hives/${h.id}`} style={{ textDecoration: 'none' }}>
            <div style={{ background: '#fff', borderRadius: 12, padding: '1.25rem', boxShadow: '0 1px 4px rgba(0,0,0,0.06)', borderLeft: `4px solid ${h.color || '#ccc'}`, transition: 'box-shadow 0.15s' }}>
              <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 10 }}>
                <span style={{ fontWeight: 600, color: '#1a1a2e' }}>{h.name}</span>
                <span style={{ fontSize: 11, color: hiveStatusColor[h.status] || '#888', background: hiveStatusColor[h.status] + '20', padding: '2px 10px', borderRadius: 20 }}>{h.status}</span>
              </div>
              <div style={{ fontSize: 12, color: '#999', display: 'flex', flexDirection: 'column', gap: 4 }}>
                <span style={{ display: 'flex', gap: 6, alignItems: 'center' }}><Layers size={12} />{h.type}</span>
                <span style={{ display: 'flex', gap: 6, alignItems: 'center' }}><Crown size={12} />Queen: {h.queenStatus ?? 'Unknown'}</span>
                {h.lastInspection && <span style={{ display: 'flex', gap: 6, alignItems: 'center' }}><Clock size={12} />Inspected {new Date(h.lastInspection).toLocaleDateString()}</span>}
              </div>
            </div>
          </Link>
        ))}
      </div>

      {showAdd && (
        <div style={{ position: 'fixed', inset: 0, background: 'rgba(0,0,0,0.4)', display: 'flex', alignItems: 'center', justifyContent: 'center', zIndex: 100 }}>
          <div style={{ background: '#fff', borderRadius: 14, padding: '2rem', minWidth: 400 }}>
            <h2 style={{ marginBottom: 20, fontWeight: 600 }}>New Hive</h2>
            <div style={{ marginBottom: 14 }}>
              <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>Name*</label>
              <input value={form.name} onChange={e => setForm(f => ({ ...f, name: e.target.value }))}
                style={{ width: '100%', padding: '0.5rem 0.75rem', borderRadius: 8, border: '1px solid #ddd', fontSize: 14, boxSizing: 'border-box' }} />
            </div>
            <div style={{ marginBottom: 14 }}>
              <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>Type</label>
              <select value={form.type} onChange={e => setForm(f => ({ ...f, type: e.target.value as HiveType }))}
                style={{ width: '100%', padding: '0.5rem 0.75rem', borderRadius: 8, border: '1px solid #ddd', fontSize: 14 }}>
                {['Langstroth', 'Dadant', 'Farrar', 'Warré', 'TopBar', 'Other'].map(t => <option key={t}>{t}</option>)}
              </select>
            </div>
            <div style={{ marginBottom: 14 }}>
              <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>Color (hex or name)</label>
              <input value={form.color} onChange={e => setForm(f => ({ ...f, color: e.target.value }))}
                style={{ width: '100%', padding: '0.5rem 0.75rem', borderRadius: 8, border: '1px solid #ddd', fontSize: 14, boxSizing: 'border-box' }} />
            </div>
            <div style={{ display: 'flex', gap: 12, marginTop: 20 }}>
              <button onClick={() => setShowAdd(false)} style={{ flex: 1, border: '1px solid #ddd', borderRadius: 8, padding: '0.6rem', cursor: 'pointer', background: '#fff' }}>Cancel</button>
              <button onClick={() => createMutation.mutate({ name: form.name, type: form.type, color: form.color, apiaryId })}
                style={{ flex: 1, background: '#f5c842', border: 'none', borderRadius: 8, padding: '0.6rem', fontWeight: 600, cursor: 'pointer' }}>
                Create
              </button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
