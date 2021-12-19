using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOPASU.Infrastructure.Data;
using OOPASU.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace TestProject1
{
    internal class TestHelper
    {
        private readonly Context _context;
        public TestHelper()
        {
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseInMemoryDatabase(databaseName: "ProjectDB");

            var dbContextOptions = builder.Options;
            _context = new Context(dbContextOptions);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }

        public Context Context
        {
            get
            {
                return _context;
            }
        }

        public IntellegentWorkRepository IntellegentWorkRepository
        {
            get
            {
                return new IntellegentWorkRepository(_context);
            }
        }
    }
}
