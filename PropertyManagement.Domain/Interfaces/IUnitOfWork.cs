namespace PropertyManagement.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPropertyRepository PropertyRepository { get; }
        Task CommitAsync();
    }

}
