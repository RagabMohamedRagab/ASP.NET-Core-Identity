using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Identity.Services {
    public class FileService : IFileService {
        private readonly IWebHostEnvironment _hosting;

        public FileService(IWebHostEnvironment hosting)
        {
            _hosting = hosting;
        }

        public string Create(IFormFile file)
        {
            if(file != null)
            {
                string Folder = Path.Combine(_hosting.WebRootPath, "Images");
                string FileName=file.FileName;
                string FullPath=Path.Combine(Folder, FileName);
                if (!Directory.Exists(Folder))
                {
                    Directory.CreateDirectory(Folder);
                }
                FileStream stream = new FileStream(FullPath, FileMode.Create);
                file.CopyTo(stream);
                stream.Close();
                return FileName;
            }
            return null;
        }

        public bool Remove(string Name)
        {
            if (Name != null)
            {
                string Folder = Path.Combine(_hosting.WebRootPath, "Images");
                string FullPath = Path.Combine(Folder, Name);
                if (File.Exists(FullPath))
                {
                    File.Delete(FullPath);
                    return true;
                }
            }
            return false;
        }
    }
}
