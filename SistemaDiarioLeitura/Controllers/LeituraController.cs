using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDiarioLeitura.Context;
using SistemaDiarioLeitura.Models;

namespace SistemaDiarioLeitura.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeituraController : ControllerBase
{
    private readonly AppDbContext _context;

    public LeituraController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Leitura>>> GetLeituras()
    {
        var Leituras = await _context.Leituras.AsNoTracking().ToListAsync();

        if (!Leituras.Any())
            return NotFound("Não há leituras cadastradas.");

        return Ok(Leituras);
    }

    [HttpGet("usuario/{idUsuario:int}")]
    public async Task<ActionResult<IEnumerable<Leitura>>> GetLeiturasByUsuario(int idUsuario)
    {
        var Leituras = await _context.Leituras.Where(l => l.IdUsuario == idUsuario).ToListAsync();

        if (!Leituras.Any())
            return NotFound($"Não há leituras cadastrados para o usuario com Id {idUsuario}.");

        return Ok(Leituras);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Leitura>> GetLeituraById(int id)
    {
        var Leitura = await _context.Leituras.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);

        if (Leitura == null)
            return NotFound("Leitura não encontrada.");

        return Ok(Leitura);
    }

    [HttpPost]
    public async Task<ActionResult<Leitura>> PostLeitura(Leitura leitura)
    {
        try
        {
            _context.Leituras.Add(leitura);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLeituraById), new { id = leitura.Id }, leitura);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutLeitura(int id, Leitura leitura)
    {
        try
        {
            if (id != leitura.Id)
                return BadRequest("Dados inválidos.");

            if (!await _context.Leituras.AnyAsync(l => l.Id == id))
                return NotFound("Leitura não encontrada.");

            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Leitura>> DeleteLeituraById(int id)
    {
        var Leitura = await _context.Leituras.FirstOrDefaultAsync(l => l.Id == id);

        if (Leitura == null)
            return NotFound("Leitura não encontrada");

        _context.Leituras.Remove(Leitura);
        await _context.SaveChangesAsync();
        return Ok(Leitura);
    }

    [HttpDelete("usuario/{idUsuario}")]
    public async Task<ActionResult<IEnumerable<Leitura>>> DeleteLeiturasByIdUsuario(int idUsuario)
    {
        var Leituras = await _context.Leituras.Where(l => l.IdUsuario == idUsuario).ToListAsync();

        if (!Leituras.Any(l => l.IdUsuario == idUsuario))
            return NotFound("Não há leituras cadastradas para esse usuário");

        _context.Leituras.RemoveRange(Leituras);
        await _context.SaveChangesAsync();
        return Ok(Leituras);
    }
}
