using GerencidorDeEventos.Dtos;
using GerencidorDeEventos.Filters;
using GerencidorDeEventos.Model;
using GerencidorDeEventos.Repository.Interface;
using GerencidorDeEventos.Service.inteface;
using GerencidorDeEventos.Service.Validations;

namespace GerencidorDeEventos.Service
{
    public class PalestraService : IPalestraService
    {
        private readonly IPalestraRepository _palestraRepository;
        private readonly IEventoRepository _eventoRepository;

        public PalestraService(IPalestraRepository palestraRepository, IEventoRepository eventoRepository)
        {
            _palestraRepository = palestraRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<dynamic> AtualizaPalestraService(int id, PalestraFilter plf)
        {
            var palestraRepository = await _palestraRepository.GetPalestrasById(id);
            var evento = await _eventoRepository.GetEventoById(plf.EventoId);

            var palestra = insertPalestra(palestraRepository, plf);

            if (palestraRepository == null)
            {
                var erromessage = new ErroMessage("Não foi encontrado nenhum Palestra com o ID digitado");
                return erromessage;
            }

            if (DateTime.Now == palestraRepository.DataInicio)
            {
                var erromessage = new ErroMessage("A atualização só pode ser feita antes do dia da Palestra");
                return erromessage;
            }
            if (ValidaHoraService.ValidarHora(plf.HoraInicio) == false || ValidaHoraService.ValidarHora(plf.HoraFim) == false)
            {
                var erromessage = new ErroMessage("Fomato do horário informado de forma errada  informe HH:mm");
                return erromessage;
            }
            var dataInicio = ValidaHoraService.AtualizarHora(plf.DataInicio, plf.HoraInicio);
            var dataFim = ValidaHoraService.AtualizarHora(plf.DataFim, plf.HoraFim);
            if (evento == null)
            {
                var erromessage = new ErroMessage("Não possui palestra com o Id digitado");
                return erromessage;
            }
            if (plf.DataInicio > plf.DataFim)
            {
                var erromessage = new ErroMessage("a data da palestra não pode ser retrocedente a data final");
                return erromessage;
            }
            else if (plf.DataInicio < DateTime.Now)
            {
                var erromessage = new ErroMessage("A data do palestra não pode ser menos que a data atual");
                return erromessage;
            }
            else if (dataInicio >= dataFim)
            {
                var erromessage = new ErroMessage("A horário de término não pode ser antecedente ao horário de inicio");
                return erromessage;
            }

            if (plf.Nome == "string" || plf.Descricao == "string" || plf.CurriculoPalestrante == "string" || plf.Palestrante == "string")
            {
                var erromessage = new ErroMessage("Nome, descrição, CurriculoPalestrante e palestrante não podem ser 'string' digite nomes válidos");
                return erromessage;
            }
            if (plf.DataInicio < evento.DataInicio)
            {
                var erromessage = new ErroMessage("Não possivel inscrever uma palestra que aconteca antes do evento");
                return erromessage;
            }
            if (plf.DataFim > evento.DataFim)
            {
                var erromessage = new ErroMessage("Não possivel inscrever uma palestra que acabe depois do evento");
                return erromessage;
            }
           
            palestra.DataInicio = dataInicio;
            palestra.DataFim = dataFim;
            palestra.Id = palestraRepository.Id;
            _palestraRepository.detached(palestraRepository);
            var plr = await _palestraRepository.AtualizarPalestra(palestra);
            var plrdto = new PalestraDto(plr.EventoId, plr.Id, plr.Nome, plr.Descricao, plr.DataInicio.ToString(), plr.DataInicio.ToString("HH.mm"), plr.DataFim.ToString("HH.mm"), plr.Palestrante, plr.CurriculoPalestrante);
            return plrdto;

        }

        public async Task<dynamic> CriarPalestraService(PalestraFilter plf)
        {

            var evento = await _eventoRepository.GetEventoById(plf.EventoId);

            if (ValidaHoraService.ValidarHora(plf.HoraInicio) == false || ValidaHoraService.ValidarHora(plf.HoraFim) == false)
            {
                var erromessage = new ErroMessage("Fomato do horário informado de forma errada  informe HH:mm");
                return erromessage;
            }
            var dataInicio = ValidaHoraService.AtualizarHora(plf.DataInicio, plf.HoraInicio);
            var dataFim = ValidaHoraService.AtualizarHora(plf.DataFim, plf.HoraFim);
            if (evento == null)
            {
                var erromessage = new ErroMessage("Não possui palestra com o Id digitado");
                return erromessage;
            }
            if (plf.DataInicio > plf.DataFim)
            {
                var erromessage = new ErroMessage("a data da palestra não pode ser retrocedente a data final");
                return erromessage;
            }
            else if (plf.DataInicio < DateTime.Now)
            {
                var erromessage = new ErroMessage("A data da palestra não pode ser menor que a data atual");
                return erromessage;
            }
            else if (dataInicio > dataFim)
            {
                var erromessage = new ErroMessage("A horário de término não pode ser antecedente ao horário de inicio");
                return erromessage;
            }

            if (plf.Nome == "string" || plf.Descricao == "string" || plf.CurriculoPalestrante == "string" || plf.Palestrante == "string")
            {
                var erromessage = new ErroMessage("Nome, descrição, CurriculoPalestrante e palestrante não podem ser 'string' digite nomes válidos");
                return erromessage;
            }
            if (plf.DataInicio < evento.DataInicio)
            {
                var erromessage = new ErroMessage("Não possivel inscrever uma palestra que aconteca antes do evento");
                return erromessage;
            }
            if (plf.DataFim > evento.DataFim)
            {
                var erromessage = new ErroMessage("Não possivel inscrever um palestra que acabe depois do evento");
                return erromessage;
            }

            var palestra = new Palestra(plf.EventoId, plf.Nome, plf.Descricao, plf.DataInicio, plf.DataFim, plf.Palestrante, plf.CurriculoPalestrante);
            palestra.DataInicio = dataInicio;
            palestra.DataFim = dataFim;
            var plr = await _palestraRepository.CriarPalestra(palestra);
            var plrdto = new PalestraDto(plr.EventoId, plr.Id, plr.Nome, plr.Descricao, plr.DataInicio.ToString(), plr.DataInicio.ToString("HH.mm"), plr.DataFim.ToString("HH.mm"), plr.Palestrante, plr.CurriculoPalestrante);
            return plrdto;
        }

        public async Task<dynamic> DeletarPalestraService(int id)
        {
            try
            {
                var palestra = await _palestraRepository.GetPalestrasById(id);

                if (palestra == null)
                {
                    var Erromessage = new ErroMessage("Palestra não encontrado com esse ID");
                    return Erromessage;
                }
                if (palestra.DataInicio == DateTime.Now)
                {
                    var Erromessage = new ErroMessage("O Palestra só pode ser removido antes da data de inicio");
                    return Erromessage;
                }
                //if (_palestraRepository.PalestraTemParticipante(palestra.Id))
                //{
                //    var Erromessage = new ErroMessage("a Palestra não pode ser removida pois tem participantes incritos");
                //    return Erromessage;

                //}
                var remove = _palestraRepository.RemoverPalestra(palestra);

                if (!remove.IsCompletedSuccessfully == false)
                {
                    var Erromessage = new ErroMessage("Palestra não removida");
                    return Erromessage;
                }
                return remove;
            }
            catch (Exception)
            {
                throw new Exception("Palestra não Encontrada, não pôde ser removido!");
            }
        }

        public async Task<List<PalestraDto>> GetPalestras()
        {
            var palestras = await _palestraRepository.GetPalestras();
            var palestraDtos = new List<PalestraDto>();

            foreach (var mc in palestras)
            {
                var mcdto = new PalestraDto(mc.EventoId, mc.Id,  mc.Nome, mc.Descricao, mc.DataInicio.ToString(), mc.DataInicio.ToString("HH.mm"), mc.DataFim.ToString("HH.mm"), mc.Palestrante, mc.CurriculoPalestrante);
                palestraDtos.Add(mcdto);
            }
            return palestraDtos;
        }

        public Palestra insertPalestra(Palestra palestra, PalestraFilter insert)
        {

            if (!string.IsNullOrEmpty(insert.Descricao))
            {
                palestra.Descricao = insert.Descricao;
            }
            if (!string.IsNullOrEmpty(insert.CurriculoPalestrante))
            {
                palestra.CurriculoPalestrante = insert.CurriculoPalestrante;
            }
            if (!(insert.DataFim == DateTime.MinValue))
            {
                palestra.DataFim = insert.DataFim;
            }
            if (!(insert.DataInicio == DateTime.MinValue))
            {
                palestra.DataInicio = insert.DataInicio;
            }
            if (!string.IsNullOrEmpty(insert.Nome))
            {
                palestra.Nome = insert.Nome;
            }
            if (!string.IsNullOrEmpty(insert.Palestrante))
            {
                palestra.Palestrante = insert.Palestrante;
            }
            return palestra;
        }
    }
}
