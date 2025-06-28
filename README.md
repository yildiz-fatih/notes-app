# NotesApp

A minimal full‑stack notes application

| Layer                | Tech                                      |
| -------------------- | ----------------------------------------- |
| **Frontend**         | React, Vite, Bootstrap                    |
| **Backend**          | ASP.NET Core, EF Core, JWT Authentication |
| **Database**         | PostgreSQL                                |
| **Containerization** | Docker, Docker Compose                    |
| **Tooling**          | Adminer (Web GUI for PostgreSQL)          |

---

## Getting Started

### Run with Docker

```bash
# clone the repo
git clone git@github.com:yildiz-fatih/notes-app.git
cd notes-app

# start the app
docker compose up --build
```

- Frontend → <http://localhost:3000>
- Backend → <http://localhost:5001/swagger>
- Adminer (DB GUI) → <http://localhost:1234>
  > Login to Adminer using:
  >
  > - System: PostgreSQL
  > - Server: db
  > - Username: notesappuser
  > - Password: SpeakFriendAndEnter!
  > - Database: notesappdb

## Run in Development Mode

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
