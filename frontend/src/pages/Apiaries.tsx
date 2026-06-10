import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { useState } from 'react';
import { Link } from 'react-router-dom';
import { apiaries } from '../api/apiaries';
import { Plus, MapPin, Layers, Trash2 } from 'lucide-react';

function Modal({ onClose, children }: { onClose: () => void; children: React.ReactNode }) {
  return (
    <div style={{ position: 'fixed', inset: 0, background: 'rgba(0,0,0,0.4)', display: 'flex', alignItems: 'center', justifyContent: 'center', zIndex: 100 }}>
      <div style={{ background: '#fff', borderRadius: 14, padding: '2rem', minWidth: 420, position: 'relative' }}>
        <button onClick={onClose} style={{ position: 'absolute', top: 16, right: 16, border: 'none', background: 'none', fontSize: 20, cursor: 'pointer', color: '#888' }}>×</button>
        {children}
      </div>
    </div>
  );
}

export default function Apiaries() {
  const qc = useQueryClient();
  const [showAdd, setShowAdd] = useState(false);
  const [form, setForm] = useState({ name: '', description: '', location: '', latitude: '', longitude: '' });

  const { data = [], isLoading } = useQuery({ queryKey: ['apiaries'], queryFn: apiaries.getAll });

  const createMutation = useMutation({
    mutationFn: apiaries.create,
    onSuccess: () => { qc.invalidateQueries({ queryKey: ['apiaries'] }); setShowAdd(false); setForm({ name: '', description: '', location: '', latitude: '', longitude: '' }); },
  });

  const deleteMutation = useMutation({
    mutationFn: apiaries.delete,
    onSuccess: () => qc.invalidateQueries({ queryKey: ['apiaries'] }),
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    createMutation.mutate({ name: form.name, description: form.description, location: form.location, latitude: form.latitude ? +form.latitude : undefined, longitude: form.longitude ? +form.longitude : undefined });
  };

  return (
    <div>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 28 }}>
        <h1 style={{ fontSize: 26, fontWeight: 700 }}>Apiaries</h1>
        <button onClick={() => setShowAdd(true)} style={{ display: 'flex', alignItems: 'center', gap: 8, background: '#f5c842', border: 'none', borderRadius: 8, padding: '0.6rem 1.2rem', fontWeight: 600, cursor: 'pointer', fontSize: 14 }}>
          <Plus size={16} /> Add Apiary
        </button>
      </div>

      {isLoading ? <p>Loading...</p> : (
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(300px, 1fr))', gap: 20 }}>
          {data.map(a => (
            <div key={a.id} style={{ background: '#fff', borderRadius: 12, padding: '1.5rem', boxShadow: '0 1px 4px rgba(0,0,0,0.06)' }}>
              <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 12 }}>
                <Link to={`/apiaries/${a.id}`} style={{ fontWeight: 600, fontSize: 18, color: '#1a1a2e', textDecoration: 'none' }}>{a.name}</Link>
                <button onClick={() => deleteMutation.mutate(a.id)} style={{ border: 'none', background: 'none', cursor: 'pointer', color: '#ccc' }}>
                  <Trash2 size={16} />
                </button>
              </div>
              {a.description && <p style={{ color: '#666', fontSize: 13, marginBottom: 12 }}>{a.description}</p>}
              <div style={{ display: 'flex', gap: 16, fontSize: 13, color: '#888' }}>
                {a.location && <span style={{ display: 'flex', gap: 4, alignItems: 'center' }}><MapPin size={13} />{a.location}</span>}
                <span style={{ display: 'flex', gap: 4, alignItems: 'center' }}><Layers size={13} />{a.hiveCount} hives</span>
              </div>
            </div>
          ))}
        </div>
      )}

      {showAdd && (
        <Modal onClose={() => setShowAdd(false)}>
          <h2 style={{ marginBottom: 20, fontSize: 18, fontWeight: 600 }}>New Apiary</h2>
          <form onSubmit={handleSubmit}>
            {[['Name*', 'name'], ['Description', 'description'], ['Location', 'location'], ['Latitude', 'latitude'], ['Longitude', 'longitude']].map(([label, key]) => (
              <div key={key} style={{ marginBottom: 14 }}>
                <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>{label}</label>
                <input required={label.includes('*')} value={(form as Record<string, string>)[key]} onChange={e => setForm(f => ({ ...f, [key]: e.target.value }))}
                  style={{ width: '100%', padding: '0.5rem 0.75rem', borderRadius: 8, border: '1px solid #ddd', fontSize: 14, boxSizing: 'border-box' }} />
              </div>
            ))}
            <button type="submit" style={{ width: '100%', background: '#f5c842', border: 'none', borderRadius: 8, padding: '0.7rem', fontWeight: 600, cursor: 'pointer', fontSize: 15 }}>
              Create Apiary
            </button>
          </form>
        </Modal>
      )}
    </div>
  );
}
