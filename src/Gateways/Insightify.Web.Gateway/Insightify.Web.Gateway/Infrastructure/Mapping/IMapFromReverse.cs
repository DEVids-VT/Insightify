namespace Insightify.Web.Gateway.Infrastructure.Mapping
{
    using AutoMapper;

    public interface IMapFromReverse<T>
    {
        void Mapping(Profile mapper) => mapper.CreateMap(typeof(T), this.GetType()).ReverseMap();
    }
}
