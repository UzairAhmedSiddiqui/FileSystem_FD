using FileSystem_Finance_Department_.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FileSystem_Finance_Department_.Context
{
    public class FileSystemContext : DbContext
    {
        public  DbSet<FileData> FileDatas { get; set; }
        public  DbSet<Login> Logins { get; set; }
        public  DbSet<FileDataDetail> FileDataDetails { get; set; }
    }
}