import { useQuery } from '@tanstack/react-query';
import { harvests } from '../api/harvests';
import { Link } from 'react-router-dom';
import { Droplets } from 'lucide-react';

const typeColor: Record<string, string> = { Honey: '#f59e0b', Beeswax: '#d97706', Propolis: '#92400e', RoyalJelly: '#ec4899', Pollen: '#84cc16' };

export default function Harvests() {
  const { data = [], isLoading } = useQuery({ queryKey: ['harvests'], queryFn: () => harvests.getAll() });
  const { data: stats = [] } = useQuery({ queryKey: ['harvest-stats'], queryFn: harvests.stats });

  const totalAll = (stats as { totalKg: number }[]).reduce((s, r) => s + r.totalKg, 0);

  return (
    <div>
      <h1 style={{ fontSize: 26, fontWeight: 700, marginBottom: 24 }}>Harvests</h1>

      <div style={{ display: 'flex', gap: 16, marginBottom: 32, flexWrap: 'wrap' }}>
        {(stats as { type: string; totalKg: number; count: number }[]).map(s => (
          <div key={s.type} style={{ background: '#fff', borderRadius: 10, padding: '1rem 1.5rem', boxShadow: '0 1px 3px rgba(0,0,0,0.05)', display: 'flex', alignItems: 'center', gap: 12, minWidth: 160 }}>
            <Droplets size={20} color={typeColor[s.type] || '#888'} />
            <div>
              <div style={{ fontWeight: 600, fontSize: 20 }}>{s.totalKg.toFixed(1)} kg</div>
              <div style={{ fontSize: 12, color: '#888' }}>{s.type} · {s.count} harvest{s.count !== 1 ? 's' : ''}</div>
            </div>
          </div>
        ))}
        {stats.length > 1 && (
          <div style={{ background: '#f5c842', borderRadius: 10, padding: '1rem 1.5rem', display: 'flex', alignItems: 'center', gap: 12, minWidth: 160 }}>
            <div>
              <div style={{ fontWeight: 700, fontSize: 20 }}>{totalAll.toFixed(1)} kg</div>
              <div style={{ fontSize: 12, fontWeight: 500 }}>Total all types</div>
            </div>
          </div>
        )}
      </div>

      {isLoading ? <p>Loading...</p> : (
        <div style={{ display: 'flex', flexDirection: 'column', gap: 10 }}>
          {data.map(h => (
            <div key={h.id} style={{ background: '#fff', borderRadius: 10, padding: '1rem 1.25rem', boxShadow: '0 1px 3px rgba(0,0,0,0.05)', display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
              <div>
                <div style={{ display: 'flex', gap: 10, alignItems: 'center' }}>
                  <span style={{ fontWeight: 600 }}>{h.quantityKg} kg</span>
                  <span style={{ fontSize: 12, color: typeColor[h.type] || '#888', background: (typeColor[h.type] || '#888') + '20', padding: '2px 8px', borderRadius: 12 }}>{h.type}</span>
                </div>
                <div style={{ fontSize: 12, color: '#888', marginTop: 4 }}>
                  {new Date(h.harvestedAt).toLocaleDateString()} · <Link to={`/hives/${h.hiveId}`} style={{ color: '#6366f1', textDecoration: 'none' }}>{h.hiveName}</Link>
                </div>
                {h.notes && <p style={{ fontSize: 12, color: '#666', marginTop: 4, marginBottom: 0 }}>{h.notes}</p>}
              </div>
            </div>
          ))}
          {data.length === 0 && <p style={{ color: '#aaa', textAlign: 'center', marginTop: 40 }}>No harvests recorded yet.</p>}
        </div>
      )}
    </div>
  );
}
