using minutes90v8.Data;
using minutes90v8.Interfaces;

namespace minutes90v8.Repository
{
    public class UnitOfWorkRepo(AppDbContext context) : IUnitOfWorkRepo
    {
        public async Task<bool> Complete()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
