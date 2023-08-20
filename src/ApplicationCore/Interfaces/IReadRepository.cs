using Ardalis.Specification;

namespace VendingIntravision.ApplicationCore.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
}
