using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveReader.Model
{
    public class FilePackage
    {
        public string Version { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
