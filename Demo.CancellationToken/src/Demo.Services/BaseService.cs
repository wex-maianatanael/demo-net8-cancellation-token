using Demo.Repository.Contracts;
using Demo.Services.Contracts;
using Microsoft.Extensions.Logging;

namespace Demo.Services
{
    public class BaseService : IBaseService
    {
		private readonly ILogger<BaseService> _logger;
        private readonly IBaseRepository _repository;

        public BaseService(ILogger<BaseService> logger, IBaseRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        #region ' 1st scenario: synchronous '

        public int GetUserAge(int birthYear, CancellationToken cancellationToken)
        {
            try
            {
                Thread.Sleep(5000); // 5s delay

                cancellationToken.ThrowIfCancellationRequested();

                return DateTime.Now.Year - birthYear;
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogInformation(ex.Message);
                return -1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while calculating user age.");
                return -1;
            }
        }

        #endregion

        #region ' 2nd scenario: asynchronous '

        public async Task<object> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _repository.GetUserByIdAsync(id, cancellationToken);
        }

        #endregion
    }
}
