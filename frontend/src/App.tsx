import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import Layout from './components/Layout';
import Dashboard from './pages/Dashboard';
import Apiaries from './pages/Apiaries';
import ApiaryDetail from './pages/ApiaryDetail';
import HiveDetail from './pages/HiveDetail';
import Inspections from './pages/Inspections';
import Treatments from './pages/Treatments';
import Harvests from './pages/Harvests';
import Tasks from './pages/Tasks';

const queryClient = new QueryClient({ defaultOptions: { queries: { retry: 1, staleTime: 30_000 } } });

export default function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Routes>
          <Route element={<Layout />}>
            <Route path="/" element={<Dashboard />} />
            <Route path="/apiaries" element={<Apiaries />} />
            <Route path="/apiaries/:id" element={<ApiaryDetail />} />
            <Route path="/hives/:id" element={<HiveDetail />} />
            <Route path="/inspections" element={<Inspections />} />
            <Route path="/treatments" element={<Treatments />} />
            <Route path="/harvests" element={<Harvests />} />
            <Route path="/tasks" element={<Tasks />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
}
