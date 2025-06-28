using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Business.DTOs;
using NotesApp.Business.Interfaces;

namespace NotesApp.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/folders")]
public class FoldersController : ControllerBase
{
    private string CurrentUserId => HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    private readonly IFolderService _folderService;

    public FoldersController(IFolderService folderService)
    {
        _folderService = folderService;
    }

    // GET /api/folders/{folderId}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var folder = await _folderService.GetAsync(id, CurrentUserId);
        return folder is null ? NotFound() : Ok(folder);
    }
    
    // GET /api/folders
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _folderService.GetAllAsync(CurrentUserId));
    }
    
    // GET /api/folders/{folderId}/notes
    [HttpGet("{id}/notes")]
    public async Task<IActionResult> GetNotes([FromRoute] string id)
    {
        var notes = await _folderService.GetNotesAsync(id, CurrentUserId);
        return notes is null ? NotFound() : Ok(notes);
    }

    // POST /api/folders
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFolderDto dto)
    {
        var folder = await _folderService.CreateAsync(dto, CurrentUserId);
        return CreatedAtAction(nameof(Get), new { id = folder.Id }, folder);
    }

    // PUT /api/folders/{folderId}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateFolderDto dto)
    {
        var updated = await _folderService.UpdateAsync(dto, id, CurrentUserId);
        return updated is null ? NotFound() : Ok(updated);
    }

    // DELETE /api/folders/{folderId}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        return await _folderService.DeleteAsync(id, CurrentUserId) ? NoContent() : NotFound();
    }
    
    // DELETE /api/folders/trash
    [HttpDelete("trash")]
    public async Task<IActionResult> EmptyThrash()
    {
        return await _folderService.ClearTrashAsync(CurrentUserId) ? NoContent() : NotFound();
    }
}