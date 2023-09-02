using Fundamental.Application.Helper.FileCheckers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundamental.Infrastructure.Helper.FileCheckers
{
    public class FileChecker : IFileChecker
    {
        public bool CheckAudioFile()
        {
            throw new NotImplementedException();
        }

        public void CheckImageFile(IFormFile file, List<string> contentTypes)
        {
            bool check = false;

            foreach (var contentType in contentTypes)
            {
                if (file.ContentType.ToLower().Equals(contentType.ToLower()) == true)
                {
                    check = true;
                    break;
                }
            }

            string message = "type must be ";
            for (int i = 0; i < contentTypes.Count; i++)
            {
                if (i == contentTypes.Count - 1)
                {
                    message += contentTypes[i];
                }
                else
                {
                    message += contentTypes[i] + " or ";
                }
            }


            if (check == false)
            {
                throw new Exception();
            }

        }

        public bool CheckVideoFile()
        {
            throw new NotImplementedException();
        }
    }
}
