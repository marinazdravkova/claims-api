using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Claims.Auditing;

public class AuditBackgroundService : BackgroundService
{
    private readonly AuditQueue _queue;
    private readonly IServiceScopeFactory _scopeFactory;

    public AuditBackgroundService(AuditQueue queue, IServiceScopeFactory scopeFactory)
    {
        _queue = queue;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var auditItem = await _queue.DequeueAsync(stoppingToken);

            using var scope = _scopeFactory.CreateScope();
            var auditor = scope.ServiceProvider.GetRequiredService<Auditer>();

            if (auditItem is (string id, string httpRequestType, string auditType))
            {
                if (auditType == "Claim")
                {
                    auditor.AuditClaim(id, httpRequestType);
                }
                else if (auditType == "Cover")
                {
                    auditor.AuditCover(id, httpRequestType);
                }
            }

            await Task.CompletedTask;
        }
    }
}