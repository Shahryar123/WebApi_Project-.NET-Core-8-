using Practice.API.Models.Domain;

namespace Practice.API.Repository
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image); 
    }
}
