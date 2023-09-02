using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Application.Storages
{
    public interface IStorage
    {
        Task<List<(string fileName, string pathOrContainerName)>> UploadRangeAsync(string pathOrContainerName, List<IFormFile> files);
        Task<(string fileName, string pathOrContainerName)> UploadAsync(string pathOrContainerName, IFormFile file);
        
        Task<bool> DeleteAsync(string pathOrContainerName, string fileName);
        Task<bool> DeleteRangeAsync(string pathOrContainerName, List<string> fileNames);

        bool HasFile(string pathOrContainerName, string fileName);
        string GetUrl(string pathOrContainerName, string fileName);
    }
}
