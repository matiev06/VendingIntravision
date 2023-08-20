using Ardalis.Specification;

namespace VendingIntravision.ApplicationCore.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T: class
{
}
