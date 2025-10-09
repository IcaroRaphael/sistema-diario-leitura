using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDiarioLeitura.Context;
using SistemaDiarioLeitura.Models;

namespace SistemaDiarioLeitura.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusLeituraController : ControllerBase
{
    private readonly AppDbContext _context;

    public StatusLeituraController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StatusLeitura>>> GetStatusLeituras()
    {
        var StatusLeituras = await _context.StatusLeituras.AsNoTracking().ToListAsync();

        if (!StatusLeituras.Any())
            return NotFound("Não há Status de Leitura Cadastrado.");

        return Ok(StatusLeituras);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StatusLeitura>> GetStatusLeituraById(int id)
    {
        var StatusLeitura = await _context.StatusLeituras.AsNoTracking().FirstOrDefaultAsync(sl => sl.Id == id);

        if (StatusLeitura == null)
            return NotFound("Status de Leitura não encontrado.");

        return Ok(StatusLeitura);
    }


    [HttpPost]
    public async Task<ActionResult<StatusLeitura>> PostStatusLeitura(StatusLeitura statusLeitura)
    {
        try
        {
            _context.StatusLeituras.Add(statusLeitura);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStatusLeituraById), new { id = statusLeitura.Id }, statusLeitura);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutStatusLeitura(int id, StatusLeitura statusLeitura)
    {
        try
        {
            if (id != statusLeitura.Id)
                return BadRequest("Dados inválidos.");

            if (!await _context.StatusLeituras.AnyAsync(sl => sl.Id == id))
                return NotFound("Status de Leitura não encontrado");

            _context.Entry(statusLeitura).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<StatusLeitura>> DeleteStatusLeituraById(int id)
    {
        var StatusLeitura = await _context.StatusLeituras.AsNoTracking().FirstOrDefaultAsync(sl => sl.Id == id);

        if (StatusLeitura == null)
            return NotFound("Status de Leitura não encontrado");

        _context.StatusLeituras.Remove(StatusLeitura);
        await _context.SaveChangesAsync();
        return Ok(StatusLeitura);
    }
}
