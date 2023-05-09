using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArchiveReader.Model;

namespace ArchiveReader
{
    public class CategoryFilePackage
    {
        public string Company { get; set; }
        public string SocialMedia { get; set; }
        public List<FilePackage> FilePackages { get; set; }
    }
}
