using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDiarioLeitura.Context;
using SistemaDiarioLeitura.Models;

namespace SistemaDiarioLeitura.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GeneroController : ControllerBase
{
    private readonly AppDbContext _context;

    public GeneroController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genero>>> GetGeneros()
    {
        var Generos = await _context.Generos.AsNoTracking().ToListAsync();

        if (!Generos.Any())
            return NotFound("Não há gêneros cadastrados.");

        return Ok(Generos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Genero>> GetGeneroById(int id)
    {
        var Genero = await _context.Generos.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);

        if (Genero == null)
            return NotFound("Gênero não encontrado.");

        return Ok(Genero);
    }

    [HttpPost]
    public async Task<ActionResult<Genero>> PostGenero(Genero genero)
    {
        try
        {
            _context.Generos.Add(genero);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGeneroById), new { id = genero.Id }, genero);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutGenero(int id, Genero genero)
    {
        try
        {
            if (id != genero.Id)
                return BadRequest("Dados inválidos.");

            if (!await _context.Generos.AnyAsync(g => g.Id == id))
                return NotFound("Gênero não encontrado.");

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Genero>> DeleteGeneroById(int id)
    {
        var Genero = await _context.Generos.FirstOrDefaultAsync(g => g.Id == id);

        if (Genero == null)
            return NotFound("Gênero não encontrado");

        _context.Generos.Remove(Genero);
        await _context.SaveChangesAsync();
        return Ok(Genero);
    }
}
