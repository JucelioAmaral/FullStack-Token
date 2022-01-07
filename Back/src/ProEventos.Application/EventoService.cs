using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;
using System;
using System.Threading.Tasks;

namespace ProEventos.Application
{

    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;
        private readonly IMapper _mapper;

        public EventoService(IGeralPersist geralPersist,
                             IEventoPersist eventoPersist,
                             IMapper mapper)
        {
            _geralPersist = geralPersist;
            _eventoPersist = eventoPersist;
            _mapper = mapper;
        }

        public async Task<EventoDto> AddEventos(int userId, EventoDto model)
        {
            try
            {               
                var evento = _mapper.Map<Evento>(model);//LE-SE: Vai pegar o "model" que é um "Dto", vai mapear ele para um "Evento" e atribuir a variável "var evento".
                evento.UserId = userId;
                _geralPersist.Add<Evento>(evento);
                if (await _geralPersist.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(userId, evento.Id, false);
                    //Faz o mapeamento ao contrário, adicionando o ".ReverseMap()" em ProEventosProfile, fazendo o invérso.
                    return _mapper.Map<EventoDto>(eventoRetorno);// LE-SE: Mapeado o evento de retorno para o meu Dto.
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<EventoDto> UpdateEventos(int userId, int eventoId, EventoDto model)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, false);
                if (evento == null) return null;

                model.Id = evento.Id;
                model.UserId = userId;
                _mapper.Map(model, evento);//Dto será mapeado para Evento.
                _geralPersist.Update<Evento>(evento);// ...e o Update recebe um objeto do tipo Evento.
                if (await _geralPersist.SaveChangesAsync())
                {
                    var eventoRetorno = await _eventoPersist.GetEventoByIdAsync(userId, evento.Id, false);
                    //Faz o mapeamento ao contrário, adicionando o ".ReverseMap()" em ProEventosProfile, fazendo o invérso.
                    return _mapper.Map<EventoDto>(eventoRetorno);// LE-SE: Mapeado o evento de retorno para o meu Dto.
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEvento(int userId, int eventoId)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, false);
                if (evento == null) throw new Exception("Evento para deletar n�o foi encontrado.");

                _geralPersist.Delete<Evento>(evento);
                return (await _geralPersist.SaveChangesAsync());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosAsync(int userId, bool includePalestrante = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosAsync(userId, includePalestrante);
                if (eventos == null) return null;

                //LE-SE: Eu quero mapear meu "evento" (var evento acima), dado meu objeto "EventoDto"
                //LE-SE TBM: Dado meu objeto "EventoDto" vou mapear meu "evento" (var "evento" acima), que é o retorno do meu repositório e atribuir ao "resultado" para retornar.
                var resultado = _mapper.Map<EventoDto[]>(eventos);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrante = false)
        {
            try
            {
                var eventos = await _eventoPersist.GetAllEventosByTemaAsync(userId, tema, includePalestrante);
                if (eventos == null) return null;

                //LE-SE: Eu quero mapear meu "evento" (var evento acima), dado meu objeto "EventoDto"
                //LE-SE TBM: Dado meu objeto "EventoDto" vou mapear meu "evento" (var "evento" acima), que é o retorno do meu repositório e atribuir ao "resultado" para retornar.
                var resultado = _mapper.Map<EventoDto[]>(eventos);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetEventoByIdAsync(int userId, int eventoId, bool includePalestrante = false)
        {
            try
            {
                var evento = await _eventoPersist.GetEventoByIdAsync(userId, eventoId, includePalestrante);
                if (evento == null) return null;

                //LE-SE: Eu quero mapear meu "evento" (var evento acima), dado meu objeto "EventoDto"
                //LE-SE TBM: Dado meu objeto "EventoDto" vou mapear meu "evento" (var "evento" acima), que é o retorno do meu repositório e atribuir ao "resultado" para retornar.
                var resultado = _mapper.Map<EventoDto>(evento);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}