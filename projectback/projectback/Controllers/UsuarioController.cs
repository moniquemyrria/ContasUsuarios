using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryMR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectback.Context;
using projectback.ModelsData;
using projectback.ModelView;

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

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            return await _context.Usuarios.Where(t => t.deletado == "N").ToListAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuarioDTO(long id)
        {
            var usuarioDTO = await _context
                                    .Usuarios
                                    .Include(t => t.Endereco)
                                    .FirstOrDefaultAsync(t => t.id == id);

            if (usuarioDTO == null)
            {
                return NotFound();
            }

            return usuarioDTO;
        }

        // PUT: api/Usuario/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<KRetorno> PutUsuarioDTO(long id, UsuarioViewModel usuarioViewModel)
        {
            var retorno = new KRetorno();

            var usuario = _context
                                .Usuarios
                                .Include(t => t.Endereco)
                                .FirstOrDefault(t => t.id == id);

            if (id != usuario.id)
            {
                retorno.Sucesso = false;
                retorno.Message = "Usuário não localizado.";
                //return BadRequest();
            }
            else
            {
                try
                {
                    usuario.email = usuarioViewModel.email;
                    usuario.cpf = usuarioViewModel.cpf;
                    usuario.dataNascimento = Convert.ToDateTime(usuarioViewModel.dataNascimento);
                    usuario.senha = usuarioViewModel.senha;

                    _context.Entry(usuario).State = EntityState.Modified;


                    foreach (var ende in usuario.Endereco.ToList())
                    {
                        _context.Enderecos.Remove(ende);
                    }

                    foreach (var ende in usuarioViewModel.endereco.ToList())
                    {
                        var endereco = new EnderecoDTO
                        {
                            bairro = ende.bairro,
                            cep = ende.cep,
                            complemento = ende.complemento,
                            localidade = ende.localidade,
                            logradouro = ende.logradouro,
                            numero = ende.numero,
                            uf = ende.uf,
                            tipo = ende.tipo,
                            UsuarioDTO = usuario
                        };

                        _context.Enderecos.Add(endereco);
                    }



                    await _context.SaveChangesAsync();
                    retorno.Sucesso = true;
                    retorno.Message = "Dados do Usuário alterado com sucesso.";
                }
                catch (Exception e)
                {

                    retorno.Sucesso = false;
                    retorno.Message = "Não foi possível alterar os dados do usuário. " + e;

                }
            }

            return retorno;//NoContent();
        }

        // POST: api/Usuario
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<KRetorno>> PostUsuarioDTO(UsuarioViewModel usuarioViewModel)
        {
            var retorno = new KRetorno();

            try
            {
                //inserindo usuario
                var usuario = new UsuarioDTO
                {
                    cpf = usuarioViewModel.cpf,
                    dataCadastro = usuarioViewModel.dataCadastro,
                    dataNascimento = Convert.ToDateTime(usuarioViewModel.dataNascimento),
                    deletado = usuarioViewModel.deletado,
                    email = usuarioViewModel.email,
                    nome = usuarioViewModel.nome,
                    senha = usuarioViewModel.senha
                };

                _context.Usuarios.Add(usuario);

                //inserindo endereços
                if (usuarioViewModel.endereco != null)
                {
                    foreach (var ende in usuarioViewModel.endereco)
                    {
                        var endereco = new EnderecoDTO
                        {
                            bairro = ende.bairro,
                            cep = ende.cep,
                            complemento = ende.complemento,
                            localidade = ende.localidade,
                            logradouro = ende.logradouro,
                            numero = ende.numero,
                            uf = ende.uf,
                            tipo = ende.tipo,
                            UsuarioDTO = usuario
                        };

                        _context.Enderecos.Add(endereco);
                    };
                }


                await _context.SaveChangesAsync();

                retorno.Sucesso = true;
                retorno.Message = "Dados do novo Usuário cadastrado com sucesso.";

            }
            catch (Exception e)
            {
                retorno.Sucesso = false;
                retorno.Message = "Não foi possível cadastrar o novo usuário. " + e;
            }


            return retorno;

        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<KRetorno>> DeleteUsuarioDTO(long id)
        {
            var retorno = new KRetorno();

            var usuarioDTO = await _context.Usuarios.FindAsync(id);
            if (usuarioDTO == null)
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
                retorno.Message = "Usuário Deletado com sucesso.";
            }
            catch
            {
                if (!UsuarioDTOExists(id))
                {
                    //return NotFound();
                    retorno.Sucesso = false;
                    retorno.Message = "Usuário não localizado.";
                }
                else
                {
                    throw;
                }
            }

            return retorno;
        }

        private bool UsuarioDTOExists(long id)
        {
            return _context.Usuarios.Any(e => e.id == id);
        }
    }
}
