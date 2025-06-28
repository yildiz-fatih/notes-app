using System.ComponentModel.DataAnnotations;

namespace NotesApp.Business.DTOs;

public record UpdateFolderDto([Required, StringLength(50)] string Name);