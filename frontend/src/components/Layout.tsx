import { NavLink, Outlet } from 'react-router-dom';
import { Home, Warehouse, Search, FlaskConical, Droplets, CheckSquare, Leaf } from 'lucide-react';

const nav = [
  { to: '/', label: 'Dashboard', icon: Home },
  { to: '/apiaries', label: 'Apiaries', icon: Warehouse },
  { to: '/inspections', label: 'Inspections', icon: Search },
  { to: '/treatments', label: 'Treatments', icon: FlaskConical },
  { to: '/harvests', label: 'Harvests', icon: Droplets },
  { to: '/tasks', label: 'Tasks', icon: CheckSquare },
];

export default function Layout() {
  return (
    <div style={{ display: 'flex', minHeight: '100vh', fontFamily: 'system-ui, sans-serif' }}>
      <aside style={{ width: 220, background: '#1a1a2e', color: '#eee', display: 'flex', flexDirection: 'column', padding: '1.5rem 0' }}>
        <div style={{ padding: '0 1.5rem 1.5rem', borderBottom: '1px solid #333' }}>
          <div style={{ display: 'flex', alignItems: 'center', gap: 10 }}>
            <Leaf size={28} color="#f5c842" />
            <div>
              <div style={{ fontWeight: 700, fontSize: 18, color: '#f5c842' }}>SaxBee</div>
              <div style={{ fontSize: 11, color: '#aaa' }}>Hive Management</div>
            </div>
          </div>
        </div>
        <nav style={{ flex: 1, padding: '1rem 0' }}>
          {nav.map(({ to, label, icon: Icon }) => (
            <NavLink
              key={to}
              to={to}
              end={to === '/'}
              style={({ isActive }) => ({
                display: 'flex', alignItems: 'center', gap: 12,
                padding: '0.65rem 1.5rem', color: isActive ? '#f5c842' : '#ccc',
                background: isActive ? 'rgba(245,200,66,0.08)' : 'transparent',
                borderLeft: isActive ? '3px solid #f5c842' : '3px solid transparent',
                textDecoration: 'none', fontSize: 14, transition: 'all 0.15s',
              })}
            >
              <Icon size={18} />
              {label}
            </NavLink>
          ))}
        </nav>
        <div style={{ padding: '1rem 1.5rem', fontSize: 11, color: '#555', borderTop: '1px solid #333' }}>
          In memory of a beekeeper<br />who tended every hive with love.
        </div>
      </aside>
      <main style={{ flex: 1, background: '#f8f7f4', padding: '2rem' }}>
        <Outlet />
      </main>
    </div>
  );
}
