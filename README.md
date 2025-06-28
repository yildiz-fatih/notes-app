# NotesApp

A minimal full‑stack notes application:

| Layer                | Tech                                          |
| -------------------- | --------------------------------------------- |
| **Frontend**         | React, Vite, Bootstrap                        |
| **Backend**          | ASP.NET Core, Entity Framework Core, JWT auth |
| **Database**         | PostgreSQL                                    |
| **Containerization** | Docker + docker‑compose                       |

---

## Getting Started

### 1. Run It (using Docker)

```bash
# clone the repo
git clone git@github.com:yildiz-fatih/notes-app.git
cd notes-app

# start the app
docker compose up --build
```

- Frontend -> <http://localhost:3000>
- Backend -> <http://localhost:5001/swagger>

### 2. Run It (Dev Mode)

#### Backend

```bash
cd backend
dotnet watch --project NotesApp.Api
```

#### Frontend

```bash
cd frontend
npm install
npm run dev
```

---
