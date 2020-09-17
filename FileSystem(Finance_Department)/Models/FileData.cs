using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileSystem_Finance_Department_.Models
{
    public class FileData
    {
        public int FileDataId { get; set; }

        public string Date { get; set; }        
   
        public string Filename { get; set; }

        public string Filenumber { get; set; }
    
        public string Subject { get; set; }

        public string Type { get; set; }

        public string Givennumber { get; set; }

        public int Pages { get; set; }

        public string Addressee { get; set; }

        public string Sectionoforigin { get; set; }

        public string Receivedby { get; set; }

        public string Status { get; set; }

        public string Pdfdirectory { get; set; }
    }
}