using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDiarioLeitura.Context;
using SistemaDiarioLeitura.Models;

namespace SistemaDiarioLeitura.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuarioController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
    {
        var Usuarios = await _context.Usuarios.AsNoTracking().ToListAsync();

        if (!Usuarios.Any())
            return NotFound("Nenhum usuário cadastrado.");

        return Ok(Usuarios);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Usuario>> GetUsuarioById(int id)
    {
        var Usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

        if (Usuario == null)
            return NotFound("Usuário não encontrado.");

        return Ok(Usuario);
    }

    [HttpPost]
    public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.Id }, usuario);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutUsuario(int id, Usuario usuario)
    {
        if (id != usuario.Id)
            return BadRequest("Dados inválidos.");

        _context.Entry(usuario).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Usuarios.Any(u => u.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Usuario>> DeleteUsuarioById(int id)
    {
        var Usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

        if (Usuario == null)
            return NotFound("Usuário não encontrado");

        _context.Usuarios.Remove(Usuario);
        _context.SaveChanges();
        return Ok(Usuario);
    }
}
