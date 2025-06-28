using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApp.Business.DTOs;
using NotesApp.Business.Interfaces;

namespace NotesApp.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/notes")]
public class NotesController : ControllerBase
{
    private string CurrentUserId => HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    private readonly INoteService _noteService;

    public NotesController(INoteService noteService)
    {
        _noteService = noteService;
    }
    
    // GET /api/notes/{noteId}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var note = await _noteService.GetAsync(id, CurrentUserId);
        return note is null ? NotFound() : Ok(note);
    }

    // POST /api/notes
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNoteDto dto)
    {
        var note = await _noteService.CreateAsync(dto, CurrentUserId);
        return CreatedAtAction(nameof(Get), new { id = note.Id }, note);
    }
    
    // PUT /api/notes/{noteId}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateNoteDto dto)
    {
        var updated = await _noteService.UpdateAsync(dto, id, CurrentUserId);
        return updated is null ? NotFound() : Ok(updated);
    }

    // DELETE /api/notes/{noteId}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        return await _noteService.DeleteAsync(id, CurrentUserId) ? NoContent() : NotFound();
    }
}