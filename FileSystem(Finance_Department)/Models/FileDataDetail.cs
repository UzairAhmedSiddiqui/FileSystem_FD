using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSystem_Finance_Department_.Models
{
    public class FileDataDetail
    {
        public int FileDataDetailId { get; set; }

        public int FileDataId { get; set; }
        public FileData FileData { get; set; }

        public string FilePath { get; set; }
        
        public string Status { get; set; }

        public string UploadTime { get; set; }

    }
}