import { Trash2 } from "lucide-react";

export default function NoteCard({ note, disabled, onEdit, onDelete }) {
  return (
    <li
      className="card mb-2"
      role="button"
      onClick={() => !disabled && onEdit(note)}
    >
      <div className="card-body position-relative">
        <h5 className="card-title mb-1 text-truncate">{note.title}</h5>
        <p className="card-text small" style={{ whiteSpace: "pre-wrap" }}>
          {note.text.length > 140 ? note.text.slice(0, 140) + "…" : note.text}
        </p>
        {!disabled && (
          <button
            className="btn btn-sm btn-outline-danger position-absolute top-0 end-0 m-1"
            onClick={(e) => {
              e.stopPropagation();
              onDelete(note.id);
            }}
          >
            <Trash2 size={14} />
          </button>
        )}
      </div>
    </li>
  );
}
