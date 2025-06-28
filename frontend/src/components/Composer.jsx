import { useState } from "react";

export default function Composer({ isOpen, open, close, onSave }) {
  const [title, setTitle] = useState("");
  const [text, setText] = useState("");

  const submit = (e) => {
    e.preventDefault();
    onSave({ title, text });
    setTitle("");
    setText("");
    close();
  };

  if (!isOpen)
    return (
      <div className="card mb-3" role="button" onClick={open}>
        <div className="card-body py-2 text-muted">Take a note…</div>
      </div>
    );

  return (
    <div className="card mb-3" style={{ maxWidth: 600 }}>
      <div className="card-body">
        <form onSubmit={submit} className="d-flex flex-column gap-2">
          <input
            className="form-control"
            placeholder="Title"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
          />
          <textarea
            className="form-control"
            placeholder="Text"
            value={text}
            onChange={(e) => setText(e.target.value)}
          />
          <div className="d-flex gap-2">
            <button type="submit" className="btn btn-primary btn-sm">
              Save
            </button>
            <button
              type="button"
              className="btn btn-secondary btn-sm"
              onClick={close}
            >
              Cancel
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
