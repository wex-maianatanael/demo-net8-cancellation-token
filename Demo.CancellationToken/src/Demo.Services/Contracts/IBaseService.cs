namespace Demo.Services.Contracts
{
    public interface IBaseService
    {
        public int GetUserAge(int birthYear, CancellationToken cancellationToken);
        public Task<object> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    }
}
