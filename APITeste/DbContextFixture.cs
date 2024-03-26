using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITeste {
    public class DbContextFixture {
        public DbContext DbContext { get; private set; }

        public DbContextFixture() {
            var options = new DbContextOptionsBuilder<DbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;

            DbContext = new DbContext(options);

            // Seed the database with test data if needed
            // DbContext.SeedTestData();
            }
        }
    }
