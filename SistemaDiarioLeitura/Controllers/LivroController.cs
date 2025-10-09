using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDiarioLeitura.Context;
using SistemaDiarioLeitura.Models;

namespace SistemaDiarioLeitura.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivroController : ControllerBase
{
    private readonly AppDbContext _context;

    public LivroController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Livro>>> GetLivros()
    {
        var Livros = await _context.Livros.AsNoTracking().ToListAsync();

        if (!Livros.Any())
            return NotFound("Não há livros cadastrados.");

        return Ok(Livros);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Livro>> GetLivroById(int id)
    {
        var Livro = await _context.Livros.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);

        if (Livro == null)
            return NotFound("Livro não encontrado.");

        return Ok(Livro);
    }

    [HttpPost]
    public async Task<ActionResult<Livro>> PostLivro(Livro livro)
    {
        try
        {
            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLivroById), new { id = livro.Id }, livro);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutLivro(int id, Livro livro)
    {
        try
        {
            if (id != livro.Id)
                return BadRequest("Dados inválidos.");

            if (!await _context.Livros.AnyAsync(l => l.Id == id))
                return NotFound("Livro não encontrado.");

            _context.Entry(livro).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Livro>> DeleteLivroById(int id)
    {
        var Livro = await _context.Livros.FirstOrDefaultAsync(l => l.Id == id);

        if (Livro == null)
            return NotFound("Livro não encontrado");

        _context.Livros.Remove(Livro);
        await _context.SaveChangesAsync();
        return Ok(Livro);
    }
}
