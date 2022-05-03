
using arquetipo.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace arquetipo.Test
{
    public class BaseTest
    {
        protected BlogContext ConstruirContext(string nombrebd)
        {
            var option = new DbContextOptionsBuilder<BlogContext>()
                .UseInMemoryDatabase(nombrebd).Options;

            var dbContext = new BlogContext(option);
            return dbContext;
        }

    }
}
