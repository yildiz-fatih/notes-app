using Microsoft.EntityFrameworkCore;
using NotesApp.Business.DTOs;
using NotesApp.Business.Interfaces;
using NotesApp.Data;
using NotesApp.Data.Entities;

namespace NotesApp.Business.Services;

public class NoteService : INoteService
{
    private readonly AppDbContext _context;

    public NoteService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<NoteDto?> GetAsync(string noteId, string userId)
    {
        var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == noteId && n.UserId == userId);
        
        return note is null ? null : new NoteDto(
            Id: note.Id,
            Title: note.Title,
            Text: note.Text,
            Created: note.Created,
            LastModified: note.LastModified,
            FolderId: note.FolderId
        );
    }

    public async Task<NoteDto> CreateAsync(CreateNoteDto dto, string userId)
    {
        var note = new Note
        {
            Title = dto.Title,
            Text = dto.Text,
            Created = DateTime.UtcNow,
            LastModified = DateTime.UtcNow,
            FolderId = dto.FolderId,
            UserId = userId
        };

        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        return new NoteDto(
            Id: note.Id,
            Title: note.Title,
            Text: note.Text,
            Created: note.Created,
            LastModified: note.LastModified,
            FolderId: note.FolderId
        );
    }

    public async Task<NoteDto?> UpdateAsync(UpdateNoteDto dto, string noteId, string userId)
    {
        var existingNote = await _context.Notes
            .FirstOrDefaultAsync(n => n.Id == noteId && n.UserId == userId);

        if (existingNote is null) { return null; }

        existingNote.Title = dto.Title;
        existingNote.Text = dto.Text;
        existingNote.FolderId = dto.FolderId;
        existingNote.LastModified = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        
        return new NoteDto(
            Id: existingNote.Id,
            Title: existingNote.Title,
            Text: existingNote.Text,
            Created: existingNote.Created,
            LastModified: existingNote.LastModified,
            FolderId: existingNote.FolderId
        );
    }

    public async Task<bool> DeleteAsync(string noteId, string userId)
    {
        var note = await _context.Notes
            .Include(note => note.Folder)
            .FirstOrDefaultAsync(n => n.Id == noteId && n.UserId == userId);

        if (note == null) { return false; }

        if (note.Folder.Type == FolderType.Trash)
        {
            // permanently delete
            _context.Notes.Remove(note);
        }
        else
        {
            // move to trash
            var trashFolder = await _context.Folders.FirstOrDefaultAsync(f => f.Type == FolderType.Trash && f.UserId == userId);
            note.FolderId = trashFolder.Id;
        }

        await _context.SaveChangesAsync();
        return true;
    }
}