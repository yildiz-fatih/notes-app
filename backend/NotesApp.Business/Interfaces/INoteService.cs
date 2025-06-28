using NotesApp.Business.DTOs;

namespace NotesApp.Business.Interfaces;

public interface INoteService
{
    Task<NoteDto?> GetAsync(string noteId, string userId);
    Task<NoteDto> CreateAsync(CreateNoteDto dto, string userId);
    Task<NoteDto?> UpdateAsync(UpdateNoteDto dto, string noteId, string userId);
    Task<bool> DeleteAsync(string noteId, string userId);
}