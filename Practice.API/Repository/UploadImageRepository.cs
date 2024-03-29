using Practice.API.Data;
using Practice.API.Models.Domain;

namespace Practice.API.Repository
{
    public class UploadImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly PracticeDbContext dbContext;

        // IWebHostingEnvironment is used to get the path of the machine where Api is running
        // 
        public UploadImageRepository(IWebHostEnvironment webHostEnvironment ,
            IHttpContextAccessor httpContextAccessor,
            PracticeDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> Upload(Image image)
        {
            //Here we can get the path of machine where api is running
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
                                                $"{image.FileName}{image.FileExtension}");
            //Upload Image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // http://localhost:1234/images/image.png
            // To get the image url path where image is uploaded

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://" +  //http
                              $"{httpContextAccessor.HttpContext.Request.Host}" +       //localhost
                              $"{httpContextAccessor.HttpContext.Request.PathBase}" +   //1234
                              $"/Images/{image.FileName}{image.FileExtension}";         //Images/image.png

            image.FilePath = urlFilePath;
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();
            return image;
        }
    }
}
