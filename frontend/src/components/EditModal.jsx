import { useState } from "react";

export default function EditModal({ note, folders, onClose, onSave }) {
  const [form, set] = useState({
    title: note.title,
    text: note.text,
    folderId: note.folderId,
  });

  const update = (key) => (e) => set((f) => ({ ...f, [key]: e.target.value }));
  const submit = (e) => {
    e.preventDefault();
    onSave({ ...note, ...form });
  };

  return (
    <div
      className="modal d-block"
      tabIndex="-1"
      style={{ background: "rgba(0,0,0,0.5)" }}
      onClick={onClose}
    >
      <div className="modal-dialog" onClick={(e) => e.stopPropagation()}>
        <div className="modal-content">
          <form onSubmit={submit}>
            <div className="modal-header py-2">
              <h5 className="modal-title">Edit Note</h5>
              <button type="button" className="btn-close" onClick={onClose} />
            </div>
            <div className="modal-body d-flex flex-column gap-2">
              <input
                className="form-control"
                placeholder="Title"
                value={form.title}
                onChange={update("title")}
              />
              <textarea
                className="form-control"
                placeholder="Text"
                value={form.text}
                onChange={update("text")}
              />
              <select
                className="form-select"
                value={form.folderId}
                onChange={update("folderId")}
              >
                {folders.map((f) => (
                  <option key={f.id} value={f.id}>
                    {f.name}
                  </option>
                ))}
              </select>
            </div>
            <div className="modal-footer py-2">
              <button
                type="button"
                className="btn btn-secondary btn-sm"
                onClick={onClose}
              >
                Cancel
              </button>
              <button type="submit" className="btn btn-primary btn-sm">
                Save
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
