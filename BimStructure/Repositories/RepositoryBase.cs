using System.Data;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace BimStructure.Repositories;

public abstract class RepositoryBase
{
    protected readonly IAccessQueryExecutor QueryExecutor;
    protected readonly ILogger Logger;

    protected RepositoryBase(
        IAccessQueryExecutor queryExecutor,
        ILogger logger)
    {
        QueryExecutor = queryExecutor;
        Logger = logger;
    }

    protected async Task<IReadOnlyList<T>> ExecuteQueryAsync<T>(
        string databasePath,
        string query,
        Func<IDataRecord, T> map,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(databasePath))
            throw new ArgumentException("Database path is required.", nameof(databasePath));

        cancellationToken.ThrowIfCancellationRequested();

        var repositoryName = GetType().Name;
        var stopwatch = Stopwatch.StartNew();

        try
        {
            Logger.LogDebug(
                "[{Repository}] Executing query: {Query}",
                repositoryName,
                query);

            var result = await QueryExecutor.QueryAsync(
                databasePath,
                query,
                map,
                cancellationToken: cancellationToken);

            stopwatch.Stop();

            Logger.LogInformation(
                "[{Repository}] Returned {Count} rows in {Elapsed} ms",
                repositoryName,
                result.Count,
                stopwatch.ElapsedMilliseconds);

            return result;
        }
        catch (OperationCanceledException)
        {
            Logger.LogWarning(
                "[{Repository}] Query was cancelled",
                repositoryName);

            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(
                ex,
                "[{Repository}] Failed to execute query: {Query}",
                repositoryName,
                query);

            throw;
        }
    }
    
    protected async Task<T> ExecuteSingleAsync<T>(
        string databasePath,
        string query,
        Func<IDataRecord, T> map,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(databasePath))
            throw new ArgumentException("Database path is required.", nameof(databasePath));

        cancellationToken.ThrowIfCancellationRequested();

        var repositoryName = GetType().Name;

        try
        {
            Logger.LogDebug("[{Repository}] Executing single query: {Query}",
                repositoryName, query);

            var result = await QueryExecutor.QuerySingleOrDefaultAsync(
                databasePath,
                query,
                map,
                cancellationToken: cancellationToken);

            if (result is null)
                throw new InvalidOperationException($"[{repositoryName}] Query returned no result.");

            Logger.LogInformation("[{Repository}] Single query succeeded");

            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex,
                "[{Repository}] Failed to execute single query: {Query}",
                repositoryName, query);

            throw;
        }
    }
}