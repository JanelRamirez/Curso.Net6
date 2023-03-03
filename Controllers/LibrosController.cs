﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    [ApiController]
    [Route("api/Libros")]
    public class LibrosController: ControllerBase
    {

        private readonly ApplicationDbContext context;
        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            return await context.Libros.Include(x => x.Autor).FirstOrDefaultAsync(context => context.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Libro libro)
        {
            var existeAutor = await context.Autores.AnyAsync(content => content.Id == libro.AutorId);
            if (!existeAutor)
            {
                return BadRequest($"No existe el autor del id: {libro.AutorId}");
            }
            context.Add(libro);
            await context.SaveChangesAsync();
            
            return Ok();
        }
    }
}
