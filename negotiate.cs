using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebPubSubTest;

public class negotiate
{
    private readonly ILogger<negotiate> _logger;

    public negotiate(ILogger<negotiate> logger)
    {
        _logger = logger;
    }

    [Function("negotiate")]
    public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
        [WebPubSubConnectionInput(Hub = "simplechat", UserId = "{headers.x-ms-client-principal-name}")] WebPubSubConnection connectionInfo)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.WriteAsJsonAsync(connectionInfo);
        return response;
    }
}
