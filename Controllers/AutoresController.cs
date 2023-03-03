using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/Autores")]
    public class AutoresController: ControllerBase
    {
        private ApplicationDbContext context { get; }

        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Autor>>> Get()
        {
            return await context.Autores.Include(a=> a.Libros).ToListAsync();
        }
        [HttpGet("primero")] 
        public async Task<ActionResult<Autor>> PrimerAutor()
        {
            return await context.Autores.FirstOrDefaultAsync();
        }
        [HttpGet("{nombre}")]
        public async Task<ActionResult<Autor>> Get(string nombre)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(a => a.Name.Contains(nombre));
            if(autor == null)
            {
                return NotFound();
            }
            return Ok(autor);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Autor>> Get(int id)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(a => a.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            return Ok(autor);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id )
        {
            if(autor.Id != id )
            {
                return BadRequest("El id del autor no coincide con el id del URL");
            }
            var existe = await context.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Update(autor);
            await context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            context.Remove(new Autor() { Id= id });
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
