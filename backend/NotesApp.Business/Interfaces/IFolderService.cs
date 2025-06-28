using NotesApp.Business.DTOs;

namespace NotesApp.Business.Interfaces;

public interface IFolderService
{
    Task<FolderDto?> GetAsync(string folderId, string userId);
    Task<IEnumerable<FolderDto>> GetAllAsync(string userId);
    Task<IEnumerable<NoteDto>?> GetNotesAsync(string folderId, string userId);

    Task<FolderDto> CreateAsync(CreateFolderDto dto, string userId);
    Task<FolderDto?> UpdateAsync(UpdateFolderDto dto, string folderId, string userId);
    Task<bool> DeleteAsync(string folderId, string userId);
    Task<bool> ClearTrashAsync(string userId);
}