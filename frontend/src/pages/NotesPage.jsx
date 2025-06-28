import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Sidebar from "../components/Sidebar";
import Composer from "../components/Composer";
import NoteCard from "../components/NoteCard";
import EditModal from "../components/EditModal";

import api from "../api";
import { useAuth } from "../contexts/AuthContext";

export default function NotesPage() {
  const { logout } = useAuth();
  const navigate = useNavigate();
  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  const [folders, setFolders] = useState([]);
  const [currentFolderId, setId] = useState(null);
  const [notes, setNotes] = useState([]);
  const [editingNote, setEditing] = useState(null);
  const [isComposing, setCompose] = useState(false);
  const [loading, setLoading] = useState({ folders: true, notes: false });

  const currentFolder = folders.find((f) => f.id === currentFolderId);
  const isTrash = currentFolder?.type === 2;

  useEffect(() => {
    fetchFolders();
  }, []);
  useEffect(() => {
    if (currentFolderId) fetchNotes();
  }, [currentFolderId]);

  async function fetchFolders() {
    setLoading((l) => ({ ...l, folders: true }));
    try {
      const { data } = await api.get("/folders");
      setFolders(data);
      setId(
        (prev) => prev ?? data.find((f) => f.type === 1)?.id ?? data[0]?.id
      );
    } finally {
      setLoading((l) => ({ ...l, folders: false }));
    }
  }

  async function fetchNotes() {
    setLoading((l) => ({ ...l, notes: true }));
    try {
      const { data } = await api.get(`/folders/${currentFolderId}/notes`);
      setNotes(data);
    } finally {
      setLoading((l) => ({ ...l, notes: false }));
    }
  }

  const apiCalls = {
    createFolder: (name) => api.post("/folders", { name }).then(fetchFolders),
    renameFolder: (id, name) =>
      api.put(`/folders/${id}`, { name }).then(fetchFolders),
    deleteFolder: (id) => api.delete(`/folders/${id}`).then(fetchFolders),
    emptyTrash: () => api.delete("/folders/trash").then(fetchNotes),
    createNote: (body) =>
      api
        .post("/notes", { ...body, folderId: currentFolderId })
        .then(fetchNotes),
    updateNote: (n) => api.put(`/notes/${n.id}`, n).then(fetchNotes),
    deleteNote: (id) => api.delete(`/notes/${id}`).then(fetchNotes),
  };

  return (
    <div className="d-flex flex-column vh-100">
      <nav className="navbar navbar-light bg-light px-3">
        <span className="navbar-brand mb-0 h1">NotesApp</span>
        <button
          className="btn btn-outline-secondary btn-sm"
          onClick={handleLogout}
        >
          Logout
        </button>
      </nav>

      <div className="d-flex flex-grow-1 overflow-hidden">
        <Sidebar
          folders={folders}
          currentId={currentFolderId}
          setCurrentId={setId}
          loading={loading.folders}
          {...apiCalls}
        />

        <main className="flex-grow-1 p-3 overflow-auto">
          {isTrash ? (
            <button
              className="btn btn-sm btn-outline-danger mb-3"
              onClick={apiCalls.emptyTrash}
            >
              Empty Trash
            </button>
          ) : (
            <Composer
              isOpen={isComposing}
              open={() => setCompose(true)}
              close={() => setCompose(false)}
              onSave={apiCalls.createNote}
            />
          )}

          {loading.notes ? (
            <p className="text-muted">Loading…</p>
          ) : notes.length === 0 ? (
            <p className="text-muted">No notes</p>
          ) : (
            <ul className="list-unstyled">
              {notes.map((n) => (
                <NoteCard
                  key={n.id}
                  note={n}
                  disabled={isTrash}
                  onEdit={() => setEditing(n)}
                  onDelete={apiCalls.deleteNote}
                />
              ))}
            </ul>
          )}
        </main>
      </div>

      {!!editingNote && (
        <EditModal
          note={editingNote}
          folders={folders.filter((f) => f.type !== 2)}
          onClose={() => setEditing(null)}
          onSave={(n) => apiCalls.updateNote(n).then(() => setEditing(null))}
        />
      )}
    </div>
  );
}
