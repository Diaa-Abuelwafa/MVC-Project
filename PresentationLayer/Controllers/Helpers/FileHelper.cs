using PresentationLayer.Models.ViewModels;

namespace PresentationLayer.Controllers.Helpers
{
    public class FileHelper
    {
        private readonly IWebHostEnvironment Env;

        public FileHelper(IWebHostEnvironment Env)
        {
            this.Env = Env;
        }
        public FileViewModel StoreFile(IFormFile File, string FolderName)
        {
            string FolderPath = Path.Combine(Env.WebRootPath, FolderName);

            string FileNameGuid = Guid.NewGuid().ToString() + "_" + File.FileName;

            string FilePath = Path.Combine(FolderPath, FileNameGuid);

            using(var Stream = new FileStream(FilePath, FileMode.Create))
            {
                File.CopyTo(Stream);

                Stream.Close();
            }

            FileViewModel FileInfo = new FileViewModel();
            // This Will Store In DB
            FileInfo.FileNameWithGuid = FileNameGuid;
            FileInfo.FileName = File.FileName;

            return FileInfo;
        }

        public void DeleteFile(string ImageNameWithGuid, string FolderName)
        {
            string FolderPath = Path.Combine(Env.WebRootPath, FolderName);

            string FilePath = Path.Combine(FolderPath, ImageNameWithGuid);

            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }
    }
}
