namespace NotesApp.Data.Entities;

public class Folder
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public FolderType Type { get; set; } = FolderType.Custom;
    public string UserId { get; set; }
    public AppUser User { get; set; }
    public ICollection<Note> Notes { get; set; } = new List<Note>();
}