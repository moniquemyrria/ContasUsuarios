using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryKodigos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectback.Context;
using projectback.ModelsData;

namespace projectback.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UsuarioController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Setor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetSetores()
        {
            return await _context.Usuarios.Where(t => t.deletado == "N").ToListAsync();
        }

        // GET: api/Setor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetSetorDTO(long id)
        {
            var setorDTO = await _context.Usuarios.FindAsync(id);

            if (setorDTO == null)
            {
                return NotFound();
            }

            return setorDTO;
        }

        // PUT: api/Setor/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<KRetorno> PutsetorDTO(long id, UsuarioDTO setorDTO)
        {
            var retorno = new KRetorno();
            if (id != setorDTO.id)
            {
                retorno.Sucesso = false;
                retorno.Message = "Setor não localizado.";
                //return BadRequest();
            }

            _context.Entry(setorDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                retorno.Sucesso = true;
                retorno.Message = "Dados do Setor alterado com sucesso.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetorDTOExists(id))
                {
                    retorno.Sucesso = false;
                    retorno.Message = "Setor não localizado.";
                    //return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return retorno;//NoContent();
        }

        // POST: api/Setor
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<KRetorno>> PostSetorDTO(UsuarioDTO setorDTO)
        {
            var retorno = new KRetorno();

            try
            {
                _context.Usuarios.Add(setorDTO);
                await _context.SaveChangesAsync();

                retorno.Sucesso = true;
                retorno.Message = "Dados do novo Setor cadastrado com sucesso.";

            }
            catch
            {
                retorno.Sucesso = false;
                retorno.Message = "Não foi possível cadastrar o novo setor.";
            }


            return retorno;

            //return CreatedAtAction("GetSetorDTO", new { id = setorDTO.id }, setorDTO);
        }

        // DELETE: api/Setor/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<KRetorno>> DeleteSetorDTO(long id)
        {
            var retorno = new KRetorno();

            var setorDTO = await _context.Usuarios.FindAsync(id);
            if (setorDTO == null)
            {
                return NotFound();
            }

            try
            {
                var data = _context.Usuarios.FirstOrDefault(t => t.id == id);

                data.deletado = "S";

                _context.Usuarios.Update(data);
                _context.SaveChanges();

                retorno.Sucesso = true;
                retorno.Message = "Setor Deletado com sucesso.";
            }
            catch
            {
                if (!SetorDTOExists(id))
                {
                    //return NotFound();
                    retorno.Sucesso = false;
                    retorno.Message = "Setor não localizado.";
                }
                else
                {
                    throw;
                }
            }

            return retorno;
        }

        private bool SetorDTOExists(long id)
        {
            return _context.Usuarios.Any(e => e.id == id);
        }
    }
}
