namespace NotesApp.Business.DTOs;

public record NoteDto(string Id, string Title, string Text, DateTime Created, DateTime LastModified, string FolderId);