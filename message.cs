using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebPubSubTest;

public class message
{
    private readonly ILogger<message> _logger;

    public message(ILogger<message> logger)
    {
        _logger = logger;
    }

    [Function("message")]
    [WebPubSubOutput(Hub = "simplechat")]
    public SendToAllAction Run(
        [WebPubSubTrigger("simplechat", WebPubSubEventType.User, "message")] UserEventRequest request)
    {
        return new SendToAllAction
        {
            Data = BinaryData.FromString($"[{request.ConnectionContext.UserId}] {request.Data.ToString()}"),
            DataType = request.DataType
        };
    }
}
