using Microsoft.AspNetCore.Http;

namespace Identity.Services {
    public interface IFileService {
        string Create(IFormFile file);
        bool Remove(string Name);
    }
}
