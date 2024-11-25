using GerencidorDeEventos.Model;
using GerencidorDeEventos.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GerencidorDeEventos.Repository
{
    public class InscricoesRepository : IInscricoesRepository
    {
        private readonly DataBaseContext _dbcontext;
        private readonly IUsuarioRepository _usuarioRepository;

        public InscricoesRepository(DataBaseContext context, IUsuarioRepository usuarioRepository)
        {
            _dbcontext = context;
            _usuarioRepository = usuarioRepository;
        }

        #region Evento
        public async Task<InscricaoEvento> CriarInscricaoEvento(InscricaoEvento inscricaoEvento)
        {
            var response = await _dbcontext.InscricoesEvento.AddAsync(inscricaoEvento);
            await _dbcontext.SaveChangesAsync();
            return inscricaoEvento;

        }

        public async Task<bool> RemoverInscricaoEvento(int eventoId, int usuarioId)
        {
            var inscricao = _dbcontext.InscricoesEvento
             .FirstOrDefault(i => i.EventoId == eventoId && i.UsuarioId == usuarioId);

            if (inscricao == null)
            {
                return false; 
            }

            _dbcontext.InscricoesEvento.Remove(inscricao);
            _dbcontext.SaveChanges();
            return true;
        }

        public List<Usuario> ObterUsuariosInscritosEvento(int eventoId)
        {
            var evento = _dbcontext.Eventos
            .Include(e => e.Inscricoes) 
            .ThenInclude(i => i.Usuario) 
            .FirstOrDefault(e => e.Id == eventoId);

            return evento?.Inscricoes.Select(i => i.Usuario).ToList() ?? new List<Usuario>();
        }

        public bool EventoTemParticipante(int eventoId)
        {
            return _dbcontext.InscricoesEvento.Any(i=> i.EventoId == eventoId);
        }
        public bool EventoTemMinicurso(int eventoId)
        {
            return _dbcontext.Minicursos.Any(m => m.EventoId == eventoId);
        }
        public bool EventoTemPalestra(int eventoId)
        {
            return _dbcontext.Palestras.Any(m => m.EventoId == eventoId);
        }
        public async Task<InscricaoEvento> GetInscricao(int eventoId, int usuarioId)
        {
            return await _dbcontext.InscricoesEvento.FirstOrDefaultAsync(iv => iv.EventoId == eventoId && iv.UsuarioId == usuarioId);
        }
        #endregion

        #region Minicurso
        public async Task<InscricaoMinicurso> CriarInscricaoMinicurso(InscricaoMinicurso inscricaoMinicurso)
        {
            var response = await _dbcontext.InscricoesMinicurso.AddAsync(inscricaoMinicurso);
            await _dbcontext.SaveChangesAsync();
            return inscricaoMinicurso;
        }

        public async Task<bool> RemoverInscricaoMinicurso(int minicursoId, int usuarioId)
        {
            var inscricao = _dbcontext.InscricoesMinicurso
             .FirstOrDefault(i => i.MinicursoId == minicursoId && i.UsuarioId == usuarioId);

            if (inscricao == null)
            {
                return false;
            }

            _dbcontext.InscricoesMinicurso.Remove(inscricao);
            _dbcontext.SaveChanges();
            return true;
        }

        public bool MinicursoTemParticipante(int minicursoId)
        {
            return _dbcontext.InscricoesMinicurso.Any(i => i.MinicursoId == minicursoId);
        }

        public List<Usuario> ObterUsuariosInscritosMinicurso(int minicursoid)
        {
            var minicurso = _dbcontext.Minicursos
            .Include(e => e.Inscricoes)
            .ThenInclude(i => i.Usuario)
            .FirstOrDefault(e => e.Id == minicursoid);

            return minicurso?.Inscricoes.Select(i => i.Usuario).ToList() ?? new List<Usuario>();
        }
        #endregion

        #region Palestra
        public async Task<InscricaoPalestra> CriarInscricaoPalestra(InscricaoPalestra inscricaoPalestra)
        {
            var response = await _dbcontext.InscricoesPalestra.AddAsync(inscricaoPalestra);
            await _dbcontext.SaveChangesAsync();
            return inscricaoPalestra;
        }

        public async Task<bool> RemoverInscricaoPalestra(int palestraId, int usuarioId)
        {
            var inscricao = _dbcontext.InscricoesPalestra
             .FirstOrDefault(i => i.PalestraId == palestraId && i.UsuarioId == usuarioId);

            if (inscricao == null)
            {
                return false;
            }

            _dbcontext.InscricoesPalestra.Remove(inscricao);
            _dbcontext.SaveChanges();
            return true;
        }

        public bool PalestraTemParticipante(int palestraId)
        {
            return _dbcontext.InscricoesPalestra.Any(i => i.PalestraId == palestraId);
        }

        public List<Usuario> ObterUsuariosInscritosPalestra(int palestraId)
        {
            var palestra = _dbcontext.Palestras
            .Include(e => e.Inscricoes)
            .ThenInclude(i => i.Usuario)
            .FirstOrDefault(e => e.Id == palestraId);

            return palestra?.Inscricoes.Select(i => i.Usuario).ToList() ?? new List<Usuario>();
        }

        #endregion


    }
}
