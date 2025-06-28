using NotesApp.Data.Entities;

namespace NotesApp.Business.DTOs;

public record FolderDto(string Id, string Name, FolderType Type);