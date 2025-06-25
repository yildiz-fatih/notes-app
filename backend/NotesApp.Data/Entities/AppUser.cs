using Microsoft.AspNetCore.Identity;

namespace NotesApp.Data.Entities;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
}