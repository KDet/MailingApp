using System.Threading.Tasks;

namespace MailApp.Core.Service
{
    /// <summary>
    /// </summary>
    public interface IFileSystemRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<string> ReadAllTextAsync(string path);

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="contents"></param>
        /// <returns></returns>
        Task WriteAllTextAsync(string path, string contents);
    }
}