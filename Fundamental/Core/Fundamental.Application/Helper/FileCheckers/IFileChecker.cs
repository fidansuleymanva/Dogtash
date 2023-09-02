using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Application.Helper.FileCheckers
{
    public interface IFileChecker
    {
        void CheckImageFile(IFormFile file, List<string> contentTypes);
        bool CheckVideoFile();
        bool CheckAudioFile();
    }
}
