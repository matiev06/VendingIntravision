using Ardalis.Specification.EntityFrameworkCore;
using VendingIntravision.ApplicationCore.Interfaces;

namespace Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IRepository<T>, IReadRepository<T> where T : class
{
    public EfRepository(BeverageSalesContext dbContext) : base(dbContext)
    {
    }
}
