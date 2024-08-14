// See https://aka.ms/new-console-template for more information

using Demo.Repository;
using Demo.Repository.Contracts;
using Demo.Services;
using Demo.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var serviceProvider = new ServiceCollection()
    .AddLogging(configure => configure.AddConsole())
    .AddTransient<IBaseService, BaseService>()
    .AddTransient<IBaseRepository, BaseRepository>()
    .BuildServiceProvider();

var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

logger.LogInformation("Starting console application...\n");

var appService = serviceProvider.GetRequiredService<IBaseService>();

// we're handling the token source manually here, but in a web application it's handled by the framework and comes from the HTTP context

/*

    In a web application, when a request is received by the server, the framework automatically creates a token source
    and associates it with the current HTTP context. This token source can then be used to create cancellation tokens
    that allow for the cancellation of asynchronous operations.

 */

#region ' 1st scenario: synchronous '

//var cancellationTokenSource1 = new CancellationTokenSource();
//var cancellationToken1 = cancellationTokenSource1.Token;

//Console.WriteLine("Enter birth year: ");
//var birthYear = Convert.ToInt32(Console.ReadLine());

//var age = appService!.GetUserAge(birthYear, cancellationToken1);
//Console.WriteLine($"You are {age} years old.");

#endregion

#region ' 2nd scenario: asynchronous '

for (int i = 1; i < 11; i++)
{
    var cancellationTokenSource2 = new CancellationTokenSource();
    var cancellationToken2 = cancellationTokenSource2.Token;

    var randomNumber = new Random();
    if (i == randomNumber.Next(i, 10))
        cancellationTokenSource2.Cancel();

    var user = await appService.GetUserByIdAsync(i, cancellationToken2);
    Console.WriteLine(user);

    if (!cancellationTokenSource2.IsCancellationRequested) // comment this line to see the unexpected behavior
        Console.WriteLine("---> processing the logic that comes after the cancellation token propagation...");
}

#endregion

Console.ReadKey();

logger.LogInformation("Ending console application...");