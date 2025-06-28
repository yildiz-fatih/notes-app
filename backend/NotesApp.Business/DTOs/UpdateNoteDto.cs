using System.ComponentModel.DataAnnotations;

namespace NotesApp.Business.DTOs;

public record UpdateNoteDto([Required, StringLength(50)] string Title, [Required] string Text, [Required] string FolderId);