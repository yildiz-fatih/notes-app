import { useState } from "react";
import { FolderClosed, Trash2, Edit, CirclePlus } from "lucide-react";

export default function Sidebar({
  folders,
  currentId,
  setCurrentId,
  loading,
  createFolder,
  renameFolder,
  deleteFolder,
}) {
  const [adding, setAdding] = useState(false);
  const [newName, setNewName] = useState("");
  const [renaming, setRenaming] = useState({ id: null, text: "" });

  const handleAdd = (e) => {
    e.preventDefault();
    if (!newName.trim()) return;
    createFolder(newName.trim());
    setNewName("");
    setAdding(false);
  };

  const handleRename = (e) => {
    e.preventDefault();
    if (!renaming.text.trim()) return;
    renameFolder(renaming.id, renaming.text.trim());
    setRenaming({ id: null, text: "" });
  };

  return (
    <div className="border-end" style={{ width: 240, overflowY: "auto" }}>
      {loading && <p className="text-muted p-2">Loading…</p>}

      {!loading && (
        <ul className="list-group list-group-flush">
          {folders.map((f) => (
            <li
              key={f.id}
              className={`list-group-item d-flex align-items-center gap-2 ${
                f.id === currentId ? "active" : ""
              }`}
              role="button"
              onClick={() => setCurrentId(f.id)}
            >
              {f.type === 2 ? <Trash2 size={16} /> : <FolderClosed size={16} />}

              {renaming.id === f.id ? (
                <form onSubmit={handleRename} className="flex-grow-1">
                  <input
                    autoFocus
                    className="form-control form-control-sm"
                    value={renaming.text}
                    onChange={(e) =>
                      setRenaming((r) => ({ ...r, text: e.target.value }))
                    }
                    onBlur={() => setRenaming({ id: null, text: "" })}
                  />
                </form>
              ) : (
                <span className="flex-grow-1 text-truncate">{f.name}</span>
              )}

              {f.type === 0 && renaming.id !== f.id && (
                <span className="d-flex gap-1">
                  <IconBtn
                    onClick={(e) => {
                      e.stopPropagation();
                      setRenaming({ id: f.id, text: f.name });
                    }}
                  >
                    <Edit size={14} />
                  </IconBtn>
                  <IconBtn
                    onClick={(e) => {
                      e.stopPropagation();
                      deleteFolder(f.id);
                    }}
                  >
                    <Trash2 size={14} />
                  </IconBtn>
                </span>
              )}
            </li>
          ))}
        </ul>
      )}

      <div className="p-2">
        {adding ? (
          <form className="d-flex gap-1" onSubmit={handleAdd}>
            <input
              autoFocus
              className="form-control form-control-sm"
              value={newName}
              onChange={(e) => setNewName(e.target.value)}
              onBlur={() => setAdding(false)}
              placeholder="Folder name"
            />
          </form>
        ) : (
          <button
            className="btn btn-sm d-flex align-items-center gap-1"
            onClick={() => setAdding(true)}
          >
            <CirclePlus size={16} /> Add folder
          </button>
        )}
      </div>
    </div>
  );
}

function IconBtn({ children, ...rest }) {
  return (
    <button
      className="btn btn-sm btn-outline-secondary p-0 d-flex align-items-center justify-content-center"
      {...rest}
    >
      {children}
    </button>
  );
}
