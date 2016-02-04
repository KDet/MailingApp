using System.IO;
using System.Threading.Tasks;
using MailApp.Core.Service;

namespace EtoMailApp.Desktop.Service
{
    public class FileSystemRepository : IFileSystemRepository
    {
        public async  Task<string> ReadAllTextAsync(string path)
        {
            using (var reader = File.OpenText(path))
                return await reader.ReadToEndAsync();
        }
        public async Task WriteAllTextAsync(string path, string contents)
        {
            using (var reader = File.CreateText(path))
                await reader.WriteAsync(contents);
        }
    }
}