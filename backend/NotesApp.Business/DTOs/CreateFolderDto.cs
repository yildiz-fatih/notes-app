using System.ComponentModel.DataAnnotations;

namespace NotesApp.Business.DTOs;

public record CreateFolderDto([Required, StringLength(50)] string Name);