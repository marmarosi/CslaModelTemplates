using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace CslaModelTemplates.Endpoints.SimpleEndpoints
{
    /// <summary>
    /// Base class for simple endpoints.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    [Route("/api/simple")]
    public abstract class SimpleAsyncEndpoint<TRequest, TResult> : BaseAsyncEndpoint
        .WithRequest<TRequest>
        .WithResponse<TResult>
    { }
}
