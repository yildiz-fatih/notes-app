using Microsoft.AspNetCore.Identity;
using NotesApp.Data;
using NotesApp.Data.Entities;

namespace NotesApp.Api;

internal static class SeedData
{
    public static async Task EnsureDefaultUserAsync(IServiceProvider services)
    {
        const string email = "fatih@coolmail.com";
        const string password = "SpeakFriendAndEnter?123";
        const string name = "Fatih";

        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var db = services.GetRequiredService<AppDbContext>();

        // Skip if the user already exists
        if (await userManager.FindByEmailAsync(email) is not null) return;

        var user = new AppUser { UserName = email, Email = email, Name = name };
        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Default user seeding failed: ");
        }

        db.Folders.AddRange(
            new Folder { Name = "Notes", Type = FolderType.Default, UserId = user.Id },
            new Folder { Name = "Trash", Type = FolderType.Trash, UserId = user.Id }
        );
        await db.SaveChangesAsync();
    }
}