
using GerencidorDeEventos.Model;
using GerencidorDeEventos.Repository;
using GerencidorDeEventos.Repository.Interface;
using GerencidorDeEventos.Service.inteface;
using GerencidorDeEventos.Service.Validations;

namespace GerencidorDeEventos.Service
{
    public class InscricoesService : IInscricaoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IMinicursoRepository _minicursoRepository;
        private readonly IInscricoesRepository _inscricoesRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPalestraRepository _palestraRepository;
        public InscricoesService(IEventoRepository eventoRepository, IInscricoesRepository inscricoesRepository, IUsuarioRepository usuarioRepository, IMinicursoRepository minicursoRepository, IPalestraRepository palestraRepository)
        {
            _eventoRepository = eventoRepository;
            _inscricoesRepository = inscricoesRepository;
            _usuarioRepository = usuarioRepository;
            _minicursoRepository = minicursoRepository;
            _palestraRepository = palestraRepository;
        }

        #region Evento
        public async Task<dynamic> InscricaoEventoService(int Idevento, string token, string telefone)
        {

            var id = TokenService.GetIdFromToken(token);
            var evento = await _eventoRepository.GetEventoById(Idevento);
            var usuario = await _usuarioRepository.GetUsuarioById(int.Parse(id));
            var usuarios = _inscricoesRepository.ObterUsuariosInscritosEvento(Idevento);

            if (usuarios.Any(u => u.Id == usuario.Id))
            {
                var erromessage = new ErroMessage("usuário já cadastrado no evento");
                return erromessage;
            }
            if (ValidaTelefone.ValidarTelefone(telefone))
            {
                var Erromessage = new ErroMessage("Telefone Inválido, por favor digite corretamente!");
                return Erromessage;
            }
            if (evento == null)
            {
                var Erromessage = new ErroMessage("Evento não Encontrado, por favor digite outro ID");
                return Erromessage;
            }
            if (evento.DataLimiteInscricao < DateTime.Now)
            {
                var Erromessage = new ErroMessage("Prazo para a inscrição já passou");
                return Erromessage;
            }
            if(usuarios.Count() >= evento.NumVagas)
            {
                var Erromessage = new ErroMessage("Não é possivel se inscrever no Evento pois não tem mais vagas disponiveis");
                return Erromessage;
            }

            var inscricao = new InscricaoEvento
            {
                EventoId = evento.Id,
                UsuarioId = usuario.Id,
                Telefone = telefone
            };

            var inscrito = await _inscricoesRepository.CriarInscricaoEvento(inscricao);
            return inscrito;

        }

        public async Task<dynamic> RemoveIncricaoEvento(int Idevento, int id_usuario)
        {
            var evento = await _eventoRepository.GetEventoById(Idevento);
            var usuarios = _inscricoesRepository.ObterUsuariosInscritosEvento(Idevento);
            var usuario = usuarios.FirstOrDefault(u => u.Id == id_usuario);

            if (evento == null)
            {
                var Erromessage = new ErroMessage("Não foi encontrado evento com o id digitado");
                return Erromessage;
            }
            if (usuario == null)
            {
                var Erromessage = new ErroMessage("Usuário não está cadastrado no evento");
                return Erromessage;
            }
            if (evento.DataInicio <= DateTime.Now.AddDays(1))
            {
                var Erromessage = new ErroMessage("O cancelamento da inscrição deve ser feita com 24 horas de antecedência");
                return Erromessage;
            }

            var inscricaoRemovida = await _inscricoesRepository.RemoverInscricaoEvento(evento.Id, usuario.Id);

            if (inscricaoRemovida == false)
            {
                var Erromessage = new ErroMessage("Não foi possível remover a inscrição");
                return Erromessage;
            }
            return inscricaoRemovida;

        }
   
        public async Task<dynamic> GetTodosOsInscritos(int id_evento)
        {
            var evento = await _eventoRepository.GetEventoById(id_evento);

            if (evento == null)
            {
                var Erromessage = new ErroMessage("Não existe eventos com o Id digitado");
                return Erromessage;
            }

            var inscritos = _inscricoesRepository.ObterUsuariosInscritosEvento(id_evento);

            if (inscritos == null)
            {
                var Erromessage = new ErroMessage("Não existe inscritos no evento digitado");
                return Erromessage;
            }

            return inscritos;
        }

        #endregion

        #region Minicurso
        public async Task<dynamic> InscricaoMinicrusoService(int IdMinicurso, string token)
        {


            var id = TokenService.GetIdFromToken(token);
            var minicurso = await _minicursoRepository.GetMinicursoById(IdMinicurso);

            if (minicurso == null)
            {
                var Erromessage = new ErroMessage("Evento não Encontrado, por favor digite outro ID");
                return Erromessage;
            }

            var usuario = await _usuarioRepository.GetUsuarioById(int.Parse(id));
            var usuariosEvento = _inscricoesRepository.ObterUsuariosInscritosEvento(minicurso.EventoId);
            var usuariosMinicurso = _inscricoesRepository.ObterUsuariosInscritosMinicurso(IdMinicurso);
            var inscricaoEvento = await _inscricoesRepository.GetInscricao(minicurso.EventoId, usuario.Id);

            if (!usuariosEvento.Any(u => u.Id == usuario.Id))
            {
                var erromessage = new ErroMessage("usuário precisa ta inscrito no evento");
                return erromessage;
            }

            if (usuariosMinicurso.Any(u => u.Id == usuario.Id))
            {
                var erromessage = new ErroMessage("usuário Já inscrito no minicurso");
                return erromessage;
            }

            if (minicurso.LimiteInscricao < DateTime.Now)
            {
                var Erromessage = new ErroMessage("Prazo para a inscrição já passou");
                return Erromessage;
            }
            if(usuariosMinicurso.Count >= minicurso.QuantidadeDeVagas )
            {
                var Erromessage = new ErroMessage("Não é possivel se inscrever no Minicurso pois não tem vagas disponiveis");
                return Erromessage;
            }

            var inscricao = new InscricaoMinicurso
            {
                MinicursoId = minicurso.Id,
                UsuarioId = usuario.Id,
                Telefone = inscricaoEvento.Telefone,
                email = usuario.Email

            };

            var inscrito = await _inscricoesRepository.CriarInscricaoMinicurso(inscricao);
            return inscrito;

        }

        public async Task<dynamic> RemoveIncricaoMinicurso(int Idminicurso, int id_usuario)
        {
            var minicurso = await _minicursoRepository.GetMinicursoById(Idminicurso);
            var usuarios = _inscricoesRepository.ObterUsuariosInscritosMinicurso(Idminicurso);
            var usuario = usuarios.FirstOrDefault(u => u.Id == id_usuario);

            if (minicurso == null)
            {
                var Erromessage = new ErroMessage("Não foi encontrado minicurso com o id digitado");
                return Erromessage;
            }
            if (usuario == null)
            {
                var Erromessage = new ErroMessage("Usuário não está cadastrado no minicurso");
                return Erromessage;
            }
            if (minicurso.DataInicio.AddDays(1) <= DateTime.Now)
            {
                var Erromessage = new ErroMessage("O cancelamento da inscrição deve ser feita com 24 horas de antecedência");
                return Erromessage;
            }

            var inscricaoRemovida = await _inscricoesRepository.RemoverInscricaoMinicurso(minicurso.Id, usuario.Id);

            if (inscricaoRemovida == false)
            {
                var Erromessage = new ErroMessage("Não foi possível remover a inscrição");
                return Erromessage;
            }
            return inscricaoRemovida;

        }

        public async Task<dynamic> GetTodosOsInscritosMinicurso(int id_minicurso)
        {
            var minicurso = await _minicursoRepository.GetMinicursoById(id_minicurso);

            if (minicurso == null)
            {
                var Erromessage = new ErroMessage("Não existe minicurso com o Id digitado");
                return Erromessage;
            }

            var inscritos = _inscricoesRepository.ObterUsuariosInscritosMinicurso(id_minicurso);

            if (inscritos == null)
            {
                var Erromessage = new ErroMessage("Não existe inscritos no evento digitado");
                return Erromessage;
            }

            return inscritos;
        }
        #endregion

        #region Palestra
        public async Task<dynamic> InscricaoPalestraService(int IdPalestra, string token)
        {

            var id = TokenService.GetIdFromToken(token);
            var palestra = await _palestraRepository.GetPalestrasById(IdPalestra);
            var usuario = await _usuarioRepository.GetUsuarioById(int.Parse(id));
            var usuariosEvento = _inscricoesRepository.ObterUsuariosInscritosEvento(palestra.EventoId);
            var usuariosPalestra = _inscricoesRepository.ObterUsuariosInscritosPalestra(IdPalestra);
            var inscricaoEvento = await _inscricoesRepository.GetInscricao(palestra.EventoId, usuario.Id);

            if (!usuariosEvento.Any(u => u.Id == usuario.Id))
            {
                var erromessage = new ErroMessage("usuário precisa ta inscrito no evento");
                return erromessage;
            }

            if (usuariosPalestra.Any(u => u.Id == usuario.Id))
            {
                var erromessage = new ErroMessage("usuário Já inscrito na Palestra");
                return erromessage;
            }

            if (palestra == null)
            {
                var Erromessage = new ErroMessage("Palestra não Encontrada, por favor digite outro ID");
                return Erromessage;
            }           

            var inscricao = new InscricaoPalestra
            {
                PalestraId = palestra.Id,
                UsuarioId = usuario.Id,
                Telefone = inscricaoEvento.Telefone,
                email = usuario.Email

            };

            var inscrito = await _inscricoesRepository.CriarInscricaoPalestra(inscricao);
            return inscrito;

        }

        public async Task<dynamic> RemoveIncricaoPalestra(int Idpalestra, int id_usuario)
        {
            var palestra = await _palestraRepository.GetPalestrasById(Idpalestra);
            var usuarios = _inscricoesRepository.ObterUsuariosInscritosPalestra(Idpalestra);
            var usuario = usuarios.FirstOrDefault(u => u.Id == id_usuario);

            if (palestra == null)
            {
                var Erromessage = new ErroMessage("Não foi encontrado Palestra com o id digitado");
                return Erromessage;
            }
            if (usuario == null)
            {
                var Erromessage = new ErroMessage("Usuário não está cadastrado na Palestra");
                return Erromessage;
            }
            if (palestra.DataInicio.AddDays(1) <= DateTime.Now)
            {
                var Erromessage = new ErroMessage("O cancelamento da inscrição deve ser feita com 24 horas de antecedência");
                return Erromessage;
            }

            var inscricaoRemovida = await _inscricoesRepository.RemoverInscricaoPalestra(palestra.Id, usuario.Id);

            if (inscricaoRemovida == false)
            {
                var Erromessage = new ErroMessage("Não foi possível remover a inscrição");
                return Erromessage;
            }
            return inscricaoRemovida;

        }

        public async Task<dynamic> GetTodosOsInscritosPalestra(int id_palestra)
        {
            var palestra = await _palestraRepository.GetPalestrasById(id_palestra);

            if (palestra == null)
            {
                var Erromessage = new ErroMessage("Não existe palestra com o Id digitado");
                return Erromessage;
            }

            var inscritos = _inscricoesRepository.ObterUsuariosInscritosPalestra(id_palestra);

            if (inscritos == null)
            {
                var Erromessage = new ErroMessage("Não existe inscritos na palestra digitado");
                return Erromessage;
            }

            return inscritos;
        }
        #endregion
    }
}