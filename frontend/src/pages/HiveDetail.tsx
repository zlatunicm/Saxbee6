import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { useParams, Link } from 'react-router-dom';
import { useState } from 'react';
import { hives } from '../api/hives';
import { inspections } from '../api/inspections';
import { treatments } from '../api/treatments';
import { harvests } from '../api/harvests';
import type { ColonyStrength, BroodPattern, Temperament, TreatmentType, HarvestType } from '../api/types';
import { ArrowLeft, Plus, Crown, FlaskConical, Droplets, ClipboardList } from 'lucide-react';

const tabs = ['Inspections', 'Treatments', 'Harvests'] as const;
type Tab = typeof tabs[number];

const strengthColor: Record<string, string> = { Strong: '#10b981', Medium: '#f59e0b', Weak: '#f97316', VeryWeak: '#ef4444' };

export default function HiveDetail() {
  const { id } = useParams<{ id: string }>();
  const hiveId = Number(id);
  const qc = useQueryClient();
  const [tab, setTab] = useState<Tab>('Inspections');
  const [showForm, setShowForm] = useState(false);

  const { data: hive } = useQuery({ queryKey: ['hive', hiveId], queryFn: () => hives.get(hiveId) });
  const { data: inspectionList = [] } = useQuery({ queryKey: ['inspections', hiveId], queryFn: () => inspections.getAll(hiveId) });
  const { data: treatmentList = [] } = useQuery({ queryKey: ['treatments', hiveId], queryFn: () => treatments.getAll(hiveId) });
  const { data: harvestList = [] } = useQuery({ queryKey: ['harvests', hiveId], queryFn: () => harvests.getAll(hiveId) });

  const [inspForm, setInspForm] = useState({ strength: 'Strong' as ColonyStrength, broodPattern: 'Good' as BroodPattern, temperament: 'Normal' as Temperament, queenSeen: false, eggsSeen: false, queenCellsPresent: false, swarmingSign: false, varroaCount: '', notes: '', weather: '', temperatureCelsius: '', honeyStoresFrames: '', broodFrames: '' });
  const [treatForm, setTreatForm] = useState({ type: 'Varroa' as TreatmentType, product: '', dosage: '', startDate: '', endDate: '', notes: '' });
  const [harvestForm, setHarvestForm] = useState({ type: 'Honey' as HarvestType, quantityKg: '', notes: '' });

  const createInspection = useMutation({ mutationFn: inspections.create, onSuccess: () => { qc.invalidateQueries({ queryKey: ['inspections', hiveId] }); setShowForm(false); } });
  const createTreatment = useMutation({ mutationFn: treatments.create, onSuccess: () => { qc.invalidateQueries({ queryKey: ['treatments', hiveId] }); setShowForm(false); } });
  const createHarvest = useMutation({ mutationFn: harvests.create, onSuccess: () => { qc.invalidateQueries({ queryKey: ['harvests', hiveId] }); setShowForm(false); } });

  const deleteInspection = useMutation({ mutationFn: inspections.delete, onSuccess: () => qc.invalidateQueries({ queryKey: ['inspections', hiveId] }) });
  const deleteTreatment = useMutation({ mutationFn: treatments.delete, onSuccess: () => qc.invalidateQueries({ queryKey: ['treatments', hiveId] }) });
  const deleteHarvest = useMutation({ mutationFn: harvests.delete, onSuccess: () => qc.invalidateQueries({ queryKey: ['harvests', hiveId] }) });

  return (
    <div>
      <Link to={hive ? `/apiaries/${hive.apiaryId}` : '/apiaries'} style={{ display: 'flex', alignItems: 'center', gap: 6, color: '#888', fontSize: 13, marginBottom: 20, textDecoration: 'none' }}>
        <ArrowLeft size={14} /> Back to {hive?.apiaryName}
      </Link>

      <div style={{ background: '#fff', borderRadius: 14, padding: '1.5rem', marginBottom: 24, boxShadow: '0 1px 4px rgba(0,0,0,0.06)', borderLeft: `5px solid ${hive?.color || '#f5c842'}` }}>
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start' }}>
          <div>
            <h1 style={{ fontSize: 24, fontWeight: 700, marginBottom: 4 }}>{hive?.name}</h1>
            <div style={{ color: '#888', fontSize: 13, display: 'flex', gap: 16 }}>
              <span>{hive?.type}</span>
              <span style={{ color: hive?.status === 'Active' ? '#10b981' : '#888' }}>{hive?.status}</span>
              <span>Since {hive?.establishedDate ? new Date(hive.establishedDate).toLocaleDateString() : '—'}</span>
            </div>
          </div>
          {hive?.queen && (
            <div style={{ background: '#fef3c7', borderRadius: 10, padding: '0.75rem 1rem', fontSize: 12 }}>
              <div style={{ display: 'flex', gap: 6, alignItems: 'center', fontWeight: 600, marginBottom: 4 }}><Crown size={14} color="#d97706" /> Queen</div>
              <div>Status: {hive.queen.status}</div>
              {hive.queen.yearBorn && <div>Born: {hive.queen.yearBorn}</div>}
            </div>
          )}
        </div>
      </div>

      <div style={{ display: 'flex', gap: 0, borderBottom: '2px solid #f0f0f0', marginBottom: 24 }}>
        {tabs.map(t => (
          <button key={t} onClick={() => setTab(t)} style={{ border: 'none', background: 'none', padding: '0.6rem 1.25rem', fontWeight: tab === t ? 600 : 400, borderBottom: tab === t ? '2px solid #f5c842' : '2px solid transparent', cursor: 'pointer', fontSize: 14, color: tab === t ? '#1a1a2e' : '#888', marginBottom: -2 }}>
            {t === 'Inspections' && <ClipboardList size={14} style={{ marginRight: 6, verticalAlign: 'middle' }} />}
            {t === 'Treatments' && <FlaskConical size={14} style={{ marginRight: 6, verticalAlign: 'middle' }} />}
            {t === 'Harvests' && <Droplets size={14} style={{ marginRight: 6, verticalAlign: 'middle' }} />}
            {t}
          </button>
        ))}
        <button onClick={() => setShowForm(true)} style={{ marginLeft: 'auto', display: 'flex', alignItems: 'center', gap: 6, background: '#f5c842', border: 'none', borderRadius: 8, padding: '0.4rem 1rem', fontWeight: 600, cursor: 'pointer', fontSize: 13 }}>
          <Plus size={14} /> Add {tab.slice(0, -1)}
        </button>
      </div>

      {tab === 'Inspections' && (
        <div style={{ display: 'flex', flexDirection: 'column', gap: 12 }}>
          {inspectionList.map(i => (
            <div key={i.id} style={{ background: '#fff', borderRadius: 10, padding: '1rem 1.25rem', boxShadow: '0 1px 3px rgba(0,0,0,0.05)', display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start' }}>
              <div>
                <div style={{ fontWeight: 600, marginBottom: 6 }}>{new Date(i.inspectedAt).toLocaleDateString('en-GB', { year: 'numeric', month: 'short', day: 'numeric' })}</div>
                <div style={{ display: 'flex', gap: 10, flexWrap: 'wrap', fontSize: 12 }}>
                  <span style={{ background: strengthColor[i.strength] + '20', color: strengthColor[i.strength], padding: '2px 8px', borderRadius: 12 }}>{i.strength}</span>
                  <span style={{ background: '#f3f4f6', padding: '2px 8px', borderRadius: 12 }}>Brood: {i.broodPattern}</span>
                  <span style={{ background: '#f3f4f6', padding: '2px 8px', borderRadius: 12 }}>{i.temperament}</span>
                  {i.queenSeen && <span style={{ background: '#d1fae5', color: '#059669', padding: '2px 8px', borderRadius: 12 }}>Queen seen</span>}
                  {i.queenCellsPresent && <span style={{ background: '#fee2e2', color: '#dc2626', padding: '2px 8px', borderRadius: 12 }}>Queen cells!</span>}
                  {i.varroaCount != null && <span style={{ background: '#fef3c7', padding: '2px 8px', borderRadius: 12 }}>Varroa: {i.varroaCount}</span>}
                </div>
                {i.notes && <p style={{ fontSize: 12, color: '#666', marginTop: 6, marginBottom: 0 }}>{i.notes}</p>}
              </div>
              <button onClick={() => deleteInspection.mutate(i.id)} style={{ border: 'none', background: 'none', cursor: 'pointer', color: '#ddd', fontSize: 16 }}>×</button>
            </div>
          ))}
          {inspectionList.length === 0 && <p style={{ color: '#aaa' }}>No inspections recorded yet.</p>}
        </div>
      )}

      {tab === 'Treatments' && (
        <div style={{ display: 'flex', flexDirection: 'column', gap: 12 }}>
          {treatmentList.map(t => (
            <div key={t.id} style={{ background: '#fff', borderRadius: 10, padding: '1rem 1.25rem', boxShadow: '0 1px 3px rgba(0,0,0,0.05)', display: 'flex', justifyContent: 'space-between' }}>
              <div>
                <div style={{ fontWeight: 600 }}>{t.product} <span style={{ fontWeight: 400, fontSize: 12, color: '#888' }}>({t.type})</span></div>
                <div style={{ fontSize: 12, color: '#888', marginTop: 4 }}>
                  {new Date(t.startDate).toLocaleDateString()} {t.endDate && `→ ${new Date(t.endDate).toLocaleDateString()}`}
                  {t.dosage && ` · ${t.dosage}`}
                </div>
                {t.notes && <p style={{ fontSize: 12, color: '#666', margin: '4px 0 0' }}>{t.notes}</p>}
              </div>
              <button onClick={() => deleteTreatment.mutate(t.id)} style={{ border: 'none', background: 'none', cursor: 'pointer', color: '#ddd', fontSize: 16 }}>×</button>
            </div>
          ))}
          {treatmentList.length === 0 && <p style={{ color: '#aaa' }}>No treatments recorded.</p>}
        </div>
      )}

      {tab === 'Harvests' && (
        <div style={{ display: 'flex', flexDirection: 'column', gap: 12 }}>
          {harvestList.map(h => (
            <div key={h.id} style={{ background: '#fff', borderRadius: 10, padding: '1rem 1.25rem', boxShadow: '0 1px 3px rgba(0,0,0,0.05)', display: 'flex', justifyContent: 'space-between' }}>
              <div>
                <div style={{ fontWeight: 600 }}>{h.type} · <span style={{ color: '#10b981' }}>{h.quantityKg} kg</span></div>
                <div style={{ fontSize: 12, color: '#888', marginTop: 4 }}>{new Date(h.harvestedAt).toLocaleDateString()}</div>
                {h.notes && <p style={{ fontSize: 12, color: '#666', margin: '4px 0 0' }}>{h.notes}</p>}
              </div>
              <button onClick={() => deleteHarvest.mutate(h.id)} style={{ border: 'none', background: 'none', cursor: 'pointer', color: '#ddd', fontSize: 16 }}>×</button>
            </div>
          ))}
          {harvestList.length === 0 && <p style={{ color: '#aaa' }}>No harvests recorded.</p>}
        </div>
      )}

      {showForm && tab === 'Inspections' && (
        <div style={{ position: 'fixed', inset: 0, background: 'rgba(0,0,0,0.4)', display: 'flex', alignItems: 'center', justifyContent: 'center', zIndex: 100 }}>
          <div style={{ background: '#fff', borderRadius: 14, padding: '2rem', minWidth: 480, maxHeight: '90vh', overflow: 'auto' }}>
            <h2 style={{ marginBottom: 20, fontWeight: 600 }}>Log Inspection</h2>
            <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: 14 }}>
              {([['Strength', 'strength', ['Strong','Medium','Weak','VeryWeak']], ['Brood Pattern', 'broodPattern', ['Excellent','Good','Fair','Poor']], ['Temperament', 'temperament', ['Calm','Gentle','Normal','Defensive','Aggressive']]] as [string, string, string[]][]).map(([label, key, opts]) => (
                <div key={key}>
                  <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>{label}</label>
                  <select value={(inspForm as unknown as Record<string, string>)[key]} onChange={e => setInspForm(f => ({ ...f, [key]: e.target.value }))}
                    style={{ width: '100%', padding: '0.45rem 0.7rem', borderRadius: 8, border: '1px solid #ddd', fontSize: 13 }}>
                    {opts.map(o => <option key={o}>{o}</option>)}
                  </select>
                </div>
              ))}
              {([['Varroa Count', 'varroaCount'], ['Temp (°C)', 'temperatureCelsius'], ['Honey Frames', 'honeyStoresFrames'], ['Brood Frames', 'broodFrames']] as [string, string][]).map(([label, key]) => (
                <div key={key}>
                  <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>{label}</label>
                  <input type="number" value={(inspForm as unknown as Record<string, string>)[key]} onChange={e => setInspForm(f => ({ ...f, [key]: e.target.value }))}
                    style={{ width: '100%', padding: '0.45rem 0.7rem', borderRadius: 8, border: '1px solid #ddd', fontSize: 13, boxSizing: 'border-box' }} />
                </div>
              ))}
            </div>
            <div style={{ display: 'flex', gap: 20, margin: '14px 0', flexWrap: 'wrap' }}>
              {[['Queen Seen', 'queenSeen'], ['Eggs Seen', 'eggsSeen'], ['Queen Cells', 'queenCellsPresent'], ['Swarming', 'swarmingSign']].map(([label, key]) => (
                <label key={key} style={{ display: 'flex', gap: 6, alignItems: 'center', fontSize: 13 }}>
                  <input type="checkbox" checked={(inspForm as unknown as Record<string, boolean>)[key]} onChange={e => setInspForm(f => ({ ...f, [key]: e.target.checked }))} />
                  {label}
                </label>
              ))}
            </div>
            <div style={{ marginBottom: 14 }}>
              <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>Notes</label>
              <textarea value={inspForm.notes} onChange={e => setInspForm(f => ({ ...f, notes: e.target.value }))} rows={2}
                style={{ width: '100%', padding: '0.45rem 0.7rem', borderRadius: 8, border: '1px solid #ddd', fontSize: 13, boxSizing: 'border-box', resize: 'vertical' }} />
            </div>
            <div style={{ display: 'flex', gap: 12 }}>
              <button onClick={() => setShowForm(false)} style={{ flex: 1, border: '1px solid #ddd', borderRadius: 8, padding: '0.6rem', cursor: 'pointer', background: '#fff' }}>Cancel</button>
              <button onClick={() => createInspection.mutate({ hiveId, inspectedAt: new Date().toISOString(), strength: inspForm.strength as ColonyStrength, broodPattern: inspForm.broodPattern as BroodPattern, temperament: inspForm.temperament as Temperament, queenSeen: inspForm.queenSeen, eggsSeen: inspForm.eggsSeen, queenCellsPresent: inspForm.queenCellsPresent, swarmingSign: inspForm.swarmingSign, varroaCount: inspForm.varroaCount ? +inspForm.varroaCount : undefined, notes: inspForm.notes, temperatureCelsius: inspForm.temperatureCelsius ? +inspForm.temperatureCelsius : undefined, honeyStoresFrames: inspForm.honeyStoresFrames ? +inspForm.honeyStoresFrames : undefined, broodFrames: inspForm.broodFrames ? +inspForm.broodFrames : undefined })}
                style={{ flex: 1, background: '#f5c842', border: 'none', borderRadius: 8, padding: '0.6rem', fontWeight: 600, cursor: 'pointer' }}>
                Save
              </button>
            </div>
          </div>
        </div>
      )}

      {showForm && tab === 'Treatments' && (
        <div style={{ position: 'fixed', inset: 0, background: 'rgba(0,0,0,0.4)', display: 'flex', alignItems: 'center', justifyContent: 'center', zIndex: 100 }}>
          <div style={{ background: '#fff', borderRadius: 14, padding: '2rem', minWidth: 420 }}>
            <h2 style={{ marginBottom: 20, fontWeight: 600 }}>Log Treatment</h2>
            <div style={{ marginBottom: 14 }}>
              <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>Type</label>
              <select value={treatForm.type} onChange={e => setTreatForm(f => ({ ...f, type: e.target.value as TreatmentType }))}
                style={{ width: '100%', padding: '0.45rem', borderRadius: 8, border: '1px solid #ddd', fontSize: 13 }}>
                {['Varroa','Nosema','AmericanFoulbrood','EuropeanFoulbrood','Chalkbrood','Pesticide','Other'].map(t => <option key={t}>{t}</option>)}
              </select>
            </div>
            {[['Product*', 'product', 'text'], ['Dosage', 'dosage', 'text'], ['Start Date*', 'startDate', 'date'], ['End Date', 'endDate', 'date']].map(([label, key, type]) => (
              <div key={key as string} style={{ marginBottom: 14 }}>
                <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>{label}</label>
                <input type={type as string} value={(treatForm as Record<string, string>)[key as string]} onChange={e => setTreatForm(f => ({ ...f, [key as string]: e.target.value }))}
                  style={{ width: '100%', padding: '0.45rem', borderRadius: 8, border: '1px solid #ddd', fontSize: 13, boxSizing: 'border-box' }} />
              </div>
            ))}
            <div style={{ display: 'flex', gap: 12 }}>
              <button onClick={() => setShowForm(false)} style={{ flex: 1, border: '1px solid #ddd', borderRadius: 8, padding: '0.6rem', cursor: 'pointer', background: '#fff' }}>Cancel</button>
              <button onClick={() => createTreatment.mutate({ hiveId, type: treatForm.type, product: treatForm.product, dosage: treatForm.dosage, startDate: treatForm.startDate, endDate: treatForm.endDate || undefined, notes: treatForm.notes })}
                style={{ flex: 1, background: '#f5c842', border: 'none', borderRadius: 8, padding: '0.6rem', fontWeight: 600, cursor: 'pointer' }}>Save</button>
            </div>
          </div>
        </div>
      )}

      {showForm && tab === 'Harvests' && (
        <div style={{ position: 'fixed', inset: 0, background: 'rgba(0,0,0,0.4)', display: 'flex', alignItems: 'center', justifyContent: 'center', zIndex: 100 }}>
          <div style={{ background: '#fff', borderRadius: 14, padding: '2rem', minWidth: 380 }}>
            <h2 style={{ marginBottom: 20, fontWeight: 600 }}>Log Harvest</h2>
            <div style={{ marginBottom: 14 }}>
              <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>Type</label>
              <select value={harvestForm.type} onChange={e => setHarvestForm(f => ({ ...f, type: e.target.value as HarvestType }))}
                style={{ width: '100%', padding: '0.45rem', borderRadius: 8, border: '1px solid #ddd', fontSize: 13 }}>
                {['Honey','Beeswax','Propolis','RoyalJelly','Pollen'].map(t => <option key={t}>{t}</option>)}
              </select>
            </div>
            <div style={{ marginBottom: 14 }}>
              <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>Quantity (kg)*</label>
              <input type="number" step="0.1" value={harvestForm.quantityKg} onChange={e => setHarvestForm(f => ({ ...f, quantityKg: e.target.value }))}
                style={{ width: '100%', padding: '0.45rem', borderRadius: 8, border: '1px solid #ddd', fontSize: 13, boxSizing: 'border-box' }} />
            </div>
            <div style={{ marginBottom: 14 }}>
              <label style={{ display: 'block', fontSize: 13, fontWeight: 500, marginBottom: 4 }}>Notes</label>
              <textarea value={harvestForm.notes} onChange={e => setHarvestForm(f => ({ ...f, notes: e.target.value }))} rows={2}
                style={{ width: '100%', padding: '0.45rem', borderRadius: 8, border: '1px solid #ddd', fontSize: 13, resize: 'vertical', boxSizing: 'border-box' }} />
            </div>
            <div style={{ display: 'flex', gap: 12 }}>
              <button onClick={() => setShowForm(false)} style={{ flex: 1, border: '1px solid #ddd', borderRadius: 8, padding: '0.6rem', cursor: 'pointer', background: '#fff' }}>Cancel</button>
              <button onClick={() => createHarvest.mutate({ hiveId, type: harvestForm.type, quantityKg: +harvestForm.quantityKg, harvestedAt: new Date().toISOString(), notes: harvestForm.notes })}
                style={{ flex: 1, background: '#f5c842', border: 'none', borderRadius: 8, padding: '0.6rem', fontWeight: 600, cursor: 'pointer' }}>Save</button>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
