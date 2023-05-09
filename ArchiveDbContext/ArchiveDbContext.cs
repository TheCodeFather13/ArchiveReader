using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archive.Sql.Repositories
{
    public class ArchiveDbContext : DbContext
    {
        public ArchiveDbContext(DbContextOptions options) : base(options)
        {
            
        }

        //public DbSet<FilePackage> FilePackages { get; set; }
        //public DbSet<CategoryFilePackage> CategoryFiles { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer(@"Data Source=Michael\Michael;Initial Catalog=FilePackage;Integrated Security=True;Encrypt=False;");
        //}
    }
}
