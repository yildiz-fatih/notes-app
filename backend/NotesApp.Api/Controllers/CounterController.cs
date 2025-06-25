using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesApp.Data;
using NotesApp.Data.Entities;

namespace NotesApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CounterController : ControllerBase
{
    private readonly AppDbContext _context;

    public CounterController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<int>> Get()
    {
        var counter = await _context.Counters.FirstOrDefaultAsync();
        return Ok(counter?.Value ?? 0);
    }

    [HttpPost]
    public async Task<ActionResult> Increment()
    {
        var counter = await _context.Counters.FirstOrDefaultAsync();
        if (counter == null)
        {
            counter = new Counter { Value = 1 };
            _context.Counters.Add(counter);
        }
        else
        {
            counter.Value += 1;
        }

        await _context.SaveChangesAsync();
        return Ok(counter.Value);
    }
}