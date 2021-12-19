using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using OOPASU.Domain;

namespace OOPASU.Infrastructure.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {

        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Indexation> Indexations { get; set; }
        public DbSet<KeyWord> KeyWords { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<IntellegentWork> IntellegentWorks { get; set; }
        public DbSet<RegistrationSertificate> RegistrationSertificates { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<EducationalProgram> EducationalPrograms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IntellegentWork>().ToTable("IntellegentWorks");
            modelBuilder.Entity<Publication>().ToTable("Publications");
            modelBuilder.Entity<Article>().ToTable("Articles");
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<RegistrationSertificate>().ToTable("RegistrationSertificates");
        }
    }
}
