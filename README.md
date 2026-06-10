# SaxBee — Beekeeping Management

> *For the grandfather who rose before dawn, tended every hive as though it were a living letter, and taught us that patience and gentleness are never wasted. His bees still hum somewhere.*

---

SaxBee is a full-stack beekeeping management web application for hobbyist and small-scale commercial beekeepers. Track your apiaries, hives, inspections, treatments, harvests, and tasks — all in one place.

---

## Features

| Module | What you can do |
|---|---|
| **Apiaries** | Create and manage multiple apiary locations with GPS coordinates |
| **Hives** | Track each hive: type (Langstroth, Dadant, Farrar, Warré…), status, color-code |
| **Queen tracking** | Log queen origin, year, marking color, clip status, and health status |
| **Inspections** | Record brood pattern, colony strength, temperament, varroa count, honey stores, queen sightings, swarm signs |
| **Treatments** | Track varroa and disease treatments with product, dosage, and duration |
| **Harvests** | Log honey, beeswax, propolis, pollen, and royal jelly harvests in kg |
| **Feedings** | Track sugar syrup, fondant, dry sugar, and pollen feeds |
| **Tasks** | Create prioritized tasks with due dates for inspections, treatments, and other work |
| **Dashboard** | At-a-glance stats: total apiaries, active hives, honey harvested, pending tasks |

---

## Tech Stack

### Backend — `backend/Saxbee6.Api`
- **.NET 8** Web API
- **Entity Framework Core 8** (SQLite by default for development, SQL Server for production)
- RESTful controllers with full CRUD for all domain entities
- Swagger / OpenAPI enabled in development
- Seed data on first run

### Frontend — `frontend/`
- **React 18** + **TypeScript**
- **Vite 5** build tool
- **React Router v6** for client-side routing
- **TanStack Query** (React Query v5) for server state management
- **Axios** for HTTP
- **Lucide React** for icons
- No CSS framework — clean inline styles, consistent design system

---

## Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 20+](https://nodejs.org/)

### 1. Start the API

```bash
cd backend/Saxbee6.Api
dotnet run
```

The API starts at `http://localhost:5000`. Swagger UI is available at `http://localhost:5000/swagger`.

On first run, EF Core creates a `saxbee.db` SQLite file with seed data (one sample apiary + two hives).

> **SQL Server**: set `ConnectionStrings:DefaultConnection` in `appsettings.json` to your SQL Server connection string. EF Core will use it automatically.

### 2. Start the frontend

```bash
cd frontend
npm install
npm run dev
```

The React app starts at `http://localhost:5173`.

---

## Project Structure

```
Saxbee6/
├── README.md
├── Saxbee6.sln               ← original legacy solution (kept for reference)
├── Saxbee6/                  ← original legacy ASP.NET Core + AngularJS app
├── backend/
│   └── Saxbee6.Api/
│       ├── Controllers/      ← Apiaries, Hives, Queens, Inspections, Treatments, Harvests, Feedings, Tasks
│       ├── Data/
│       │   ├── Models/       ← EF Core entity models
│       │   └── SaxBeeContext.cs
│       ├── DTOs/             ← Request/response record types
│       └── Program.cs
└── frontend/
    └── src/
        ├── api/              ← Typed API clients + shared types
        ├── components/       ← Layout, shared UI
        └── pages/            ← Dashboard, Apiaries, ApiaryDetail, HiveDetail, Inspections, Treatments, Harvests, Tasks
```

---

## API Reference

All endpoints return JSON with enum values as strings.

| Method | Path | Description |
|---|---|---|
| GET | `/api/apiaries` | List all apiaries |
| POST | `/api/apiaries` | Create apiary |
| GET/PUT/DELETE | `/api/apiaries/{id}` | Get, update, or delete apiary |
| GET | `/api/hives?apiaryId=` | List hives (optionally filtered) |
| POST | `/api/hives` | Create hive |
| GET/PUT/DELETE | `/api/hives/{id}` | Get, update, or delete hive |
| POST | `/api/queens` | Assign queen to hive (replaces existing) |
| GET | `/api/inspections?hiveId=` | List inspections |
| POST | `/api/inspections` | Log inspection |
| GET | `/api/treatments?hiveId=` | List treatments |
| POST | `/api/treatments` | Log treatment |
| GET | `/api/harvests?hiveId=` | List harvests |
| POST | `/api/harvests` | Log harvest |
| GET | `/api/harvests/stats` | Harvest totals grouped by type |
| GET | `/api/tasks?completed=` | List tasks |
| POST | `/api/tasks` | Create task |
| PATCH | `/api/tasks/{id}/complete` | Toggle task completion |

---

## Migration from Legacy App

The original `Saxbee6/` directory contains the 2017 AngularJS + ASP.NET Core (legacy) application. It is preserved for reference. The rewrite retains the same domain concepts but uses modern naming in English and a normalized schema.

| Legacy (Bosnian) | New (English) |
|---|---|
| Pcelinjak | Apiary |
| PZajednica | Hive |
| Matica | Queen |
| Radovi / RadnoIzvjesce | Inspection |
| Pcelar | (planned: User/Auth) |

---

## Roadmap

- [ ] User authentication (ASP.NET Core Identity)
- [ ] Weather API integration for inspection planning
- [ ] QR code labels for hives
- [ ] CSV/PDF export of inspection records
- [ ] Mobile-responsive layout
- [ ] Photo attachments on inspections

---

## License

MIT

---

*Built with care — for every beekeeper who keeps notes in a worn notebook and smells faintly of smoke.*
