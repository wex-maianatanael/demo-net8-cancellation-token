using Demo.Repository.Contracts;
using Microsoft.Extensions.Logging;

namespace Demo.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly ILogger<BaseRepository> _logger;

        public BaseRepository(ILogger<BaseRepository> logger)
        {
            _logger = logger;
        }

        #region ' 2nd scenario: asynchronous '

        public async Task<object> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken.IsCancellationRequested)
                    cancellationToken.ThrowIfCancellationRequested();

                return await Task.Run(() =>
                {
                    Thread.Sleep(2000);

                    var currentYear = DateTime.Now.Year;
                    var randomNumber = new Random();
                    var birthYear = randomNumber.Next(1950, DateTime.Now.Year - 1);

                    var firstName = GetRandomFirstName();
                    var middleName = GetRandomMiddleName();

                    return new { Id = id, Name = string.Join(" ", firstName, middleName), BirthDate = birthYear, Age = DateTime.Now.Year - birthYear };
                });
            }
            catch (OperationCanceledException)
            {
                var className = GetType().Name;
                _logger.LogInformation("{ClassName} | Operation was canceled - Repository method not executed.", className);

                throw;
            }
        }

        private string GetRandomFirstName()
        {
            var firstNameList = new List<string>()
                {
                    "Paul",
                    "Ozzy",
                    "Zakk",
                    "Randy",
                    "Angus",
                };

            var randomNumber = new Random();
            var index = randomNumber.Next(0, firstNameList.Count);

            return firstNameList[index];
        }

        private string GetRandomMiddleName()
        {
            var middleNameList = new List<string>()
                {
                    "McCartney",
                    "Osbourne",
                    "Wylde",
                    "Blythe",
                    "Young",
                };

            var randomNumber = new Random();
            var index = randomNumber.Next(0, middleNameList.Count);

            return middleNameList[index];
        }
        #endregion
    }
}
