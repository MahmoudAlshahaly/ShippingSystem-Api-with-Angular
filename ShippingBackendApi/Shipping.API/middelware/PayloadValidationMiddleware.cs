using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Text;

namespace Shipping.API.middelware
{
    public class PayloadValidationMiddleware
    {
    //    private readonly RequestDelegate _next;
    //    private readonly IMemoryCache _cache;
    //    public PayloadValidationMiddleware(RequestDelegate next, IMemoryCache cache)
    //    {
    //        _next = next;
    //        _cache = cache;
    //    }

    //    public async Task InvokeAsync(HttpContext context)
    //    {
    //        // Check if the request involves payload (e.g., POST or PUT)
    //        if (IsPayloadInvolved(context.Request.Method))
    //        {
    //            // Extract payload from the request
    //            string payload = await GetRequestBody(context.Request);

    //            // Generate a unique cache key based on requester, payload, and method
    //            string cacheKey = $"{context.Request.Host.Value}_{context.Request.Path}_{context.Request.Method}_{payload}";

    //            if (_cache.TryGetValue(cacheKey, out _))
    //            {
    //                // Payload has been used within the specified time frame
    //                context.Response.StatusCode = StatusCodes.Status400BadRequest;
    //                await context.Response.WriteAsync("Duplicate payload within 10 minutes.");
    //                return;
    //            }

    //            // Cache the payload key with a sliding expiration of 10 minutes
    //            _cache.Set(cacheKey, DateTime.Now, TimeSpan.FromMinutes(10));
    //        }

    //        // Continue processing the request
    //        await _next(context);
    //    }
    //    private async Task<string> GetRequestBody(HttpRequest request)
    //    {
    //        using var streamReader = new System.IO.StreamReader(request.Body, Encoding.UTF8);
    //        return await streamReader.ReadToEndAsync();
    //    }
    //    private bool IsPayloadInvolved(string method)
    //    {
    //        // Check if the request method involves a payload (e.g., POST or PUT)
    //        return string.Equals(method, "POST", StringComparison.OrdinalIgnoreCase)
    //        || string.Equals(method, "PUT", StringComparison.OrdinalIgnoreCase);
    //    }
    //    private string GeneratePayloadKey(string payload)
    //    {
    //        // Implement a method to generate a unique key based on payload content
    //        // Example: using a hash function
    //        using var sha256 = System.Security.Cryptography.SHA256.Create();
    //        byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(payload));
    //        return Convert.ToBase64String(hashBytes);
    //    }
    }


}

