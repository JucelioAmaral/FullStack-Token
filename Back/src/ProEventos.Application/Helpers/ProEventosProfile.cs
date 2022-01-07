using AutoMapper;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Domain.Identity;

namespace ProEventos.Application.Helpers
{
    public class ProEventosProfile : Profile
    {

        public ProEventosProfile()
        {
            //LE-SE: Todas as vezes que um dado vier de Evento, eu quero que vc mapeie para EventoDto.
            CreateMap<Evento, EventoDto>().ReverseMap();
            //CreateMap<EventoDto, Evento>(); Poderia ser esse e o de cima, mas basta adicionar o ".ReverseMap()" que é feito o invérso.
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto > ().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
        }
    }
}
