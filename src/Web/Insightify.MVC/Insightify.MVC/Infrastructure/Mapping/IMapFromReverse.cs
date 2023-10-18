namespace Insightify.MVC.Infrastructure.Mapping
{
    using AutoMapper;

    public interface IMapFromReverse<T>
    {
        void Mapping(Profile mapper) => mapper.CreateMap(typeof(T), GetType()).ReverseMap();
    }
}
