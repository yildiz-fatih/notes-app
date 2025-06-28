namespace NotesApp.Data.Entities;

public class Note
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastModified { get; set; } = DateTime.UtcNow;
    public string FolderId { get; set; }
    public Folder Folder { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }
}