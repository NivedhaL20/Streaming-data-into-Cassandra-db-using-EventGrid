using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.WomenInTech.Services.Interface
{
    public interface IBlobStorageService
    {
        Task UploadStreamIntoBlobAsync(string blobPath, Stream stream);
    }
}
