import { useQuery } from '@tanstack/react-query';
import { treatments } from '../api/treatments';
import { Link } from 'react-router-dom';

export default function Treatments() {
  const { data = [], isLoading } = useQuery({ queryKey: ['treatments'], queryFn: () => treatments.getAll() });

  return (
    <div>
      <h1 style={{ fontSize: 26, fontWeight: 700, marginBottom: 8 }}>All Treatments</h1>
      <p style={{ color: '#888', marginBottom: 28, fontSize: 13 }}>Complete treatment log across all hives.</p>
      {isLoading ? <p>Loading...</p> : (
        <div style={{ display: 'flex', flexDirection: 'column', gap: 10 }}>
          {data.map(t => (
            <div key={t.id} style={{ background: '#fff', borderRadius: 10, padding: '1rem 1.25rem', boxShadow: '0 1px 3px rgba(0,0,0,0.05)', display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start' }}>
              <div>
                <div style={{ fontWeight: 600 }}>{t.product} <span style={{ fontWeight: 400, fontSize: 12, color: '#888' }}>({t.type})</span></div>
                <div style={{ fontSize: 12, color: '#888', margin: '4px 0' }}>
                  {new Date(t.startDate).toLocaleDateString()} {t.endDate && `→ ${new Date(t.endDate).toLocaleDateString()}`}
                  {t.dosage && ` · ${t.dosage}`}
                </div>
                <Link to={`/hives/${t.hiveId}`} style={{ fontSize: 12, color: '#6366f1', textDecoration: 'none' }}>{t.hiveName}</Link>
              </div>
            </div>
          ))}
          {data.length === 0 && <p style={{ color: '#aaa', textAlign: 'center', marginTop: 40 }}>No treatments recorded yet.</p>}
        </div>
      )}
    </div>
  );
}
