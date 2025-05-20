//using AutoMapper;
//using Domain.Models;
//using TmdbAdapter.Clients;

//namespace Adapter.TmdbAdapter
//{
//    public class TmdbMapperProfile : Profile
//    {
//        public TmdbMapperProfile()
//        {
//            CreateMap<TmdbSearchMoviesGetResult.ResultItem, Filme>()
//                .ForMember(destino => destino.Descricao,
//                    opt => opt.MapFrom(origem => origem.Overview))
//                .ForMember(destino => destino.Nome,
//                    opt => opt.MapFrom(origem => origem.Title))
//                .ForMember(destino => destino.DataLancamento,
//                    opt => opt.MapFrom(origem => origem.ReleaseDate));

//            CreateMap<Search, TmdbSearchMoviesGet>()
//                .ForMember(destino => destino.Query,
//                    opt => opt.MapFrom(origem => origem.SearchTerm))
//                .ForMember(destino => destino.Year,
//                    opt => opt.MapFrom(origem => origem.ReleaseYear));
//        }
//    }
//}
