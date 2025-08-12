using minutes90v8.Data;
using minutes90.Interfaces;

namespace minutes90.Repository
{
    public class UnitOfWorkRepo(AppDbContext context) : IUnitOfWorkRepo
    {
        public async Task<bool> Complete()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
