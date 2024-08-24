using Microsoft.Extensions.Logging;

namespace vehicle_calculation.common.ErrorHandling
{
    public interface ILogManager
    {
        public void SetLogType(Type type);
        (string reference, string msg) LogError(Exception? ex, string? message);
        (string reference, string msg) LogError(string? message);
    }

    public class LogManager(ILoggerFactory factory) : ILogManager
    {
        private readonly ILoggerFactory _factory = factory;
        private ILogger _logger = factory.CreateLogger<LogManager>();

        public (string reference, string msg) LogError(Exception? ex, string? message)
        {
            var (reference, msg) = ComposeReferencedMessage(message);
            _logger.LogError(ex, msg);
            return (reference, msg);
        }

        public (string reference, string msg) LogError(string? message)
        {
            var (reference, msg) = ComposeReferencedMessage(message);
            _logger.LogError(msg);
            return (reference, msg);
        }

        public void SetLogType(Type type)
        {
            _logger = _factory.CreateLogger(type);
        }

        private (string reference, string msg) ComposeReferencedMessage(string? message)
        {
            var reference = Guid.NewGuid().ToString("N");
            var referencePart = $"Reference: {reference}";
            if (string.IsNullOrWhiteSpace(message))
            {
                return (reference, referencePart);
            }

            return (reference, $"{message} {referencePart}");
        }
    }
}
