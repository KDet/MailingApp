using System.Threading.Tasks;
using MailApp.Core.Service;

namespace EtoMailApp.Mobile.Service
{
    public class FileSystemRepository : IFileSystemRepository
    {
        public async  Task<string> ReadAllTextAsync(string path)
        {
            return "";
        }
        public async Task WriteAllTextAsync(string path, string contents)
        {
            
        }
    }
}