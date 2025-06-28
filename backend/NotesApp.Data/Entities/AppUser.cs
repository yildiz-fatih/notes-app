using Microsoft.AspNetCore.Identity;

namespace NotesApp.Data.Entities;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public ICollection<Folder> Folders { get; set; } = new List<Folder>();
    public ICollection<Note> Notes { get; set; } = new List<Note>();
}