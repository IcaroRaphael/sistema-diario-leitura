using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaDiarioLeitura.Context;
using SistemaDiarioLeitura.Models;

namespace SistemaDiarioLeitura.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        var Usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

        if (Usuario == null)
            return NotFound("Usuário não encontrado.");

        return Ok(Usuario);
    }

    [HttpPost]
    public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
    {
        try
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.Id }, usuario);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> PutUsuario(int id, Usuario usuario)
    {
        try
        {
            if (id != usuario.Id)
                return BadRequest("Dados inválidos.");

            if (!await _context.Usuarios.AnyAsync(u => u.Id == id))
                return NotFound("Usuário não encontrado");

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Usuario>> DeleteUsuarioById(int id)
    {
        var Usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

        if (Usuario == null)
            return NotFound("Usuário não encontrado");

        _context.Usuarios.Remove(Usuario);
        await _context.SaveChangesAsync();
        return Ok(Usuario);
    }
}
