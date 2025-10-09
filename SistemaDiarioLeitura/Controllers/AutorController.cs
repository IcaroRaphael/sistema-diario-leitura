using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDiarioLeitura.Context;
using SistemaDiarioLeitura.Models;

namespace SistemaDiarioLeitura.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AutorController : ControllerBase
{
    private readonly AppDbContext _context;

    public AutorController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Autor>>> GetAutores()
    {
        var Autores = await _context.Autores.AsNoTracking().ToArrayAsync();

        if (!Autores.Any())
            return NotFound("Não há autor cadastrado.");

        return Ok(Autores);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Autor>> GetAutorById(int id)
    {
        var Autor = await _context.Autores.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

        if (Autor == null)
            return NotFound("Autor não encontrado.");

        return Ok(Autor);
    }

    [HttpPost]
    public async Task<ActionResult<Autor>> PostAutor(Autor autor)
    {
        try
        {
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAutorById), new { id = autor.Id }, autor);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutAutor(int id, Autor autor)
    {
        try
        {
            if (id != autor.Id)
                return BadRequest("Dados inválidos.");

            if (!await _context.Autores.AnyAsync(a => a.Id == id))
                return NotFound("Autor não encontrado.");

            _context.Entry(autor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Autor>> DeleteAutorById(int id)
    {
        var Autor = await _context.Autores.FirstOrDefaultAsync(a => a.Id == id);

        if (Autor == null)
            return NotFound("Autor não encontrado");

        _context.Autores.Remove(Autor);
        await _context.SaveChangesAsync();
        return Ok(Autor);
    }
}
