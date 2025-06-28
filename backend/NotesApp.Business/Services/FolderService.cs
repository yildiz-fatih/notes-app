using Microsoft.EntityFrameworkCore;
using NotesApp.Business.DTOs;
using NotesApp.Business.Interfaces;
using NotesApp.Data;
using NotesApp.Data.Entities;

namespace NotesApp.Business.Services;

public class FolderService : IFolderService
{
    private readonly AppDbContext _context;

    public FolderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FolderDto?> GetAsync(string folderId, string userId)
    {
        var folder = await _context.Folders.FirstOrDefaultAsync(f => f.Id == folderId && f.UserId == userId);

        return folder is null ? null : new FolderDto(folder.Id, folder.Name, folder.Type);
    }

    public async Task<IEnumerable<FolderDto>> GetAllAsync(string userId)
    {
        var folders = await _context.Folders.Where(f => f.UserId == userId).ToListAsync();

        return folders.Select(f => new FolderDto(Id: f.Id, Name: f.Name, Type: f.Type));
    }
    
    public async Task<IEnumerable<NoteDto>?> GetNotesAsync(string folderId, string userId)
    {
        var folderExists = await _context.Folders
            .AnyAsync(f => f.Id == folderId && f.UserId == userId);
        if (!folderExists) { return null; }

        var notes = await _context.Notes
            .Where(n => n.FolderId == folderId && n.UserId == userId)
            .OrderByDescending(n => n.LastModified)
            .ToListAsync();

        return notes.Select(n => new NoteDto(n.Id, n.Title, n.Text, n.Created, n.LastModified, n.FolderId));
    }

    public async Task<FolderDto> CreateAsync(CreateFolderDto dto, string userId)
    {
        var folder = new Folder { Name = dto.Name, Type = FolderType.Custom, UserId = userId, Notes = new List<Note>() };

        _context.Folders.Add(folder);
        await _context.SaveChangesAsync();

        return new FolderDto(Id: folder.Id, Name: folder.Name, Type: folder.Type);
    }
    
    public async Task<FolderDto?> UpdateAsync(UpdateFolderDto dto, string folderId, string userId)
    {
        var folder = await _context.Folders.FirstOrDefaultAsync(f => f.Id == folderId && f.UserId == userId);

        if (folder is null || folder.Type != FolderType.Custom) { return null; }

        folder.Name = dto.Name;
        await _context.SaveChangesAsync();

        return new FolderDto(Id: folder.Id, Name: folder.Name, Type: folder.Type);
    }


    public async Task<bool> DeleteAsync(string folderId, string userId)
    {
        var folder = await _context.Folders.FirstOrDefaultAsync(f => f.Id == folderId && f.UserId == userId);
        if (folder is null || folder.Type != FolderType.Custom) { return false; }
        
        _context.Folders.Remove(folder);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ClearTrashAsync(string userId)
    {
        var trashFolder = await _context.Folders.FirstOrDefaultAsync(f => f.Type == FolderType.Trash && f.UserId == userId);
        var notes = await _context.Notes.Where(n => n.FolderId == trashFolder.Id).ToListAsync();
        if (notes.Count == 0) { return false; }

        _context.Notes.RemoveRange(notes);
        await _context.SaveChangesAsync();
        return true;
    }
}
