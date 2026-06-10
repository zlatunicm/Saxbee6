import { useQuery } from '@tanstack/react-query';
import { inspections } from '../api/inspections';
import { Link } from 'react-router-dom';

const strengthColor: Record<string, string> = { Strong: '#10b981', Medium: '#f59e0b', Weak: '#f97316', VeryWeak: '#ef4444' };

export default function Inspections() {
  const { data = [], isLoading } = useQuery({ queryKey: ['inspections'], queryFn: () => inspections.getAll() });

  return (
    <div>
      <h1 style={{ fontSize: 26, fontWeight: 700, marginBottom: 8 }}>All Inspections</h1>
      <p style={{ color: '#888', marginBottom: 28, fontSize: 13 }}>A chronological log of all hive inspections. Add inspections from individual hive pages.</p>

      {isLoading ? <p>Loading...</p> : (
        <div style={{ display: 'flex', flexDirection: 'column', gap: 10 }}>
          {data.map(i => (
            <div key={i.id} style={{ background: '#fff', borderRadius: 10, padding: '1rem 1.25rem', boxShadow: '0 1px 3px rgba(0,0,0,0.05)' }}>
              <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 8 }}>
                <div>
                  <span style={{ fontWeight: 600, marginRight: 12 }}>{new Date(i.inspectedAt).toLocaleDateString('en-GB', { year: 'numeric', month: 'short', day: 'numeric' })}</span>
                  <Link to={`/hives/${i.hiveId}`} style={{ fontSize: 13, color: '#6366f1', textDecoration: 'none' }}>{i.hiveName}</Link>
                </div>
                <span style={{ background: strengthColor[i.strength] + '20', color: strengthColor[i.strength], padding: '2px 10px', borderRadius: 12, fontSize: 12 }}>{i.strength}</span>
              </div>
              <div style={{ display: 'flex', gap: 8, flexWrap: 'wrap', fontSize: 12 }}>
                <span style={{ background: '#f3f4f6', padding: '2px 8px', borderRadius: 12 }}>Brood: {i.broodPattern}</span>
                <span style={{ background: '#f3f4f6', padding: '2px 8px', borderRadius: 12 }}>{i.temperament}</span>
                {i.queenSeen && <span style={{ background: '#d1fae5', color: '#059669', padding: '2px 8px', borderRadius: 12 }}>Queen seen</span>}
                {i.queenCellsPresent && <span style={{ background: '#fee2e2', color: '#dc2626', padding: '2px 8px', borderRadius: 12 }}>Queen cells!</span>}
                {i.swarmingSign && <span style={{ background: '#fef3c7', color: '#d97706', padding: '2px 8px', borderRadius: 12 }}>Swarm sign</span>}
                {i.varroaCount != null && <span style={{ background: '#fef3c7', padding: '2px 8px', borderRadius: 12 }}>Varroa: {i.varroaCount}</span>}
              </div>
              {i.notes && <p style={{ fontSize: 12, color: '#666', marginTop: 8, marginBottom: 0 }}>{i.notes}</p>}
            </div>
          ))}
          {data.length === 0 && <p style={{ color: '#aaa', textAlign: 'center', marginTop: 40 }}>No inspections yet. Open a hive to log the first one.</p>}
        </div>
      )}
    </div>
  );
}
