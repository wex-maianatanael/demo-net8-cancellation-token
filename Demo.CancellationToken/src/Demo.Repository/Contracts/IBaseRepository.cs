namespace Demo.Repository.Contracts
{
    public interface IBaseRepository
    {
        Task<object> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    }
}
