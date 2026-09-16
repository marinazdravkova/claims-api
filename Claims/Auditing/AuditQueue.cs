using System.Threading.Channels;

namespace Claims.Auditing;

public class AuditQueue
{
    private readonly Channel<object> _queue = Channel.CreateUnbounded<object>();

    public ValueTask QueueAuditAsync(object auditItem)
    {
        return _queue.Writer.WriteAsync(auditItem);
    }

    public ValueTask<object> DequeueAsync(CancellationToken cancellationToken)
    {
        return _queue.Reader.ReadAsync(cancellationToken);
    }
}
