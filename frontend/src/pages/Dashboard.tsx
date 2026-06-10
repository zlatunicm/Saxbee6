import { useQuery } from '@tanstack/react-query';
import { apiaries } from '../api/apiaries';
import { hives } from '../api/hives';
import { tasks } from '../api/tasks';
import { harvests } from '../api/harvests';
import { Warehouse, Layers, CheckSquare, Droplets } from 'lucide-react';

function StatCard({ icon: Icon, label, value, color }: { icon: typeof Warehouse; label: string; value: number | string; color: string }) {
  return (
    <div style={{ background: '#fff', borderRadius: 12, padding: '1.5rem', display: 'flex', gap: 16, alignItems: 'center', boxShadow: '0 1px 4px rgba(0,0,0,0.06)', flex: 1, minWidth: 160 }}>
      <div style={{ background: color + '20', borderRadius: 10, padding: 12, color }}>
        <Icon size={24} />
      </div>
      <div>
        <div style={{ fontSize: 28, fontWeight: 700 }}>{value}</div>
        <div style={{ color: '#888', fontSize: 13 }}>{label}</div>
      </div>
    </div>
  );
}

export default function Dashboard() {
  const { data: apiaryList = [] } = useQuery({ queryKey: ['apiaries'], queryFn: apiaries.getAll });
  const { data: hiveList = [] } = useQuery({ queryKey: ['hives'], queryFn: () => hives.getAll() });
  const { data: taskList = [] } = useQuery({ queryKey: ['tasks-pending'], queryFn: () => tasks.getAll(false) });
  const { data: harvestStats = [] } = useQuery({ queryKey: ['harvest-stats'], queryFn: harvests.stats });

  const activeHives = hiveList.filter(h => h.status === 'Active').length;
  const pendingTasks = taskList.length;
  const totalHoney = (harvestStats as { type: string; totalKg: number }[])
    .filter(s => s.type === 'Honey')
    .reduce((sum, s) => sum + s.totalKg, 0);

  return (
    <div>
      <h1 style={{ marginBottom: 8, fontSize: 26, fontWeight: 700 }}>Dashboard</h1>
      <p style={{ color: '#888', marginBottom: 32 }}>Welcome back to your apiary management.</p>

      <div style={{ display: 'flex', gap: 20, flexWrap: 'wrap', marginBottom: 40 }}>
        <StatCard icon={Warehouse} label="Apiaries" value={apiaryList.length} color="#6366f1" />
        <StatCard icon={Layers} label="Active Hives" value={activeHives} color="#f59e0b" />
        <StatCard icon={Droplets} label="Honey Harvested (kg)" value={totalHoney.toFixed(1)} color="#10b981" />
        <StatCard icon={CheckSquare} label="Pending Tasks" value={pendingTasks} color="#ef4444" />
      </div>

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 24 }}>
        <div style={{ background: '#fff', borderRadius: 12, padding: '1.5rem', boxShadow: '0 1px 4px rgba(0,0,0,0.06)' }}>
          <h2 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>Your Apiaries</h2>
          {apiaryList.length === 0 ? (
            <p style={{ color: '#aaa' }}>No apiaries yet. Add one to get started.</p>
          ) : apiaryList.map(a => (
            <div key={a.id} style={{ display: 'flex', justifyContent: 'space-between', padding: '10px 0', borderBottom: '1px solid #f0f0f0' }}>
              <div>
                <div style={{ fontWeight: 500 }}>{a.name}</div>
                <div style={{ fontSize: 12, color: '#aaa' }}>{a.location}</div>
              </div>
              <div style={{ background: '#f5c84220', color: '#d97706', borderRadius: 20, padding: '2px 12px', fontSize: 13, display: 'flex', alignItems: 'center' }}>
                {a.hiveCount} hives
              </div>
            </div>
          ))}
        </div>

        <div style={{ background: '#fff', borderRadius: 12, padding: '1.5rem', boxShadow: '0 1px 4px rgba(0,0,0,0.06)' }}>
          <h2 style={{ fontSize: 16, fontWeight: 600, marginBottom: 16 }}>Upcoming Tasks</h2>
          {taskList.length === 0 ? (
            <p style={{ color: '#aaa' }}>All caught up! No pending tasks.</p>
          ) : taskList.slice(0, 6).map(t => (
            <div key={t.id} style={{ display: 'flex', justifyContent: 'space-between', padding: '10px 0', borderBottom: '1px solid #f0f0f0' }}>
              <div style={{ fontWeight: 500 }}>{t.title}</div>
              <div style={{ fontSize: 12, color: t.priority === 'High' ? '#ef4444' : t.priority === 'Medium' ? '#f59e0b' : '#888' }}>
                {t.priority}
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}
