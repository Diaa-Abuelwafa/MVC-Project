namespace PresentationLayer.Controllers.Helpers
{
    public class FileHelper
    {
        private readonly IWebHostEnvironment Env;
        public FileHelper(IWebHostEnvironment Env)
        {
            // Injection
            this.Env = Env;
        }

        public string StoreFile(IFormFile File, string FolderName)
        {
            string FolderPath = Path.Combine(Env.WebRootPath, FolderName);

            string FileName = Guid.NewGuid().ToString() + "_" + File.FileName;

            string FilePath = Path.Combine(FolderPath, FileName);

            using(var Stream = new FileStream(FilePath, FileMode.Create))
            {
                File.CopyTo(Stream);

                Stream.Close();
            }

            return FileName;
        }

        public void DeleteFile(string FileName, string FolderName)
        {
            string FolderPath = Path.Combine(Env.WebRootPath, FolderName);

            string FilePath = Path.Combine(FolderPath, FileName);

            if(File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
