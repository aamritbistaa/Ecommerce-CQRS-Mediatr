using System;

namespace Application.Common;

public class ServiceResult<T>
{
    public StatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}
public class Messages
{
    // HTTP status code messages
    public static readonly string Success = "Request was successful.";
    // Use when a request completes successfully, typically with a 200 OK status.

    public static readonly string Created = "Resource was successfully created.";
    // Use when a new resource has been created, typically with a 201 Created status.

    public static readonly string NoContent = "No content to display.";
    // Use when a request completes successfully, but there is no content to return, typically with a 204 No Content status.

    public static readonly string PermanentRedirect = "Resource has been permanently moved.";
    // Use when a resource has been permanently moved to a new URL, typically with a 301 Moved Permanently status.

    public static readonly string TemporaryRedirect = "Resource has been temporarily moved.";
    // Use when a resource is temporarily available at a different URL, typically with a 302 Found status.

    public static readonly string NotModified = "Resource has not been modified.";
    // Use when the requested resource has not changed since the last request, typically with a 304 Not Modified status.

    public static readonly string BadRequest = "The request was invalid.";
    // Use when the server cannot process the request due to a client error, typically with a 400 Bad Request status.

    public static readonly string UnAuthorized = "You are not authorized to access this resource.";
    // Use when authentication is required and has failed or not been provided, typically with a 401 Unauthorized status.

    public static readonly string Forbidden = "Access to this resource is forbidden.";
    // Use when the client does not have permission to access the resource, typically with a 403 Forbidden status.

    public static readonly string NotFound = "Resource not found.";
    // Use when the requested resource could not be found on the server, typically with a 404 Not Found status.

    public static readonly string MethodNotAllowed = "This method is not allowed for the requested resource.";
    // Use when the request method is not supported for the requested resource, typically with a 405 Method Not Allowed status.

    public static readonly string TimeOut = "The request timed out.";
    // Use when the server takes too long to respond, typically with a 408 Request Timeout status.

    public static readonly string RequestClash = "There was a conflict with the request.";
    // Use when there is a conflict with the current state of the resource, typically with a 409 Conflict status.

    public static readonly string ServerError = "An internal server error occurred.";
    // Use when an unexpected condition was encountered, typically with a 500 Internal Server Error status.

    public static readonly string BadGateway = "Received an invalid response from the upstream server.";
    // Use when the server acting as a gateway or proxy receives an invalid response from the upstream server, typically with a 502 Bad Gateway status.

    public static readonly string ServerBusy = "The server is currently busy. Please try again later.";
    // Use when the server is temporarily unable to handle the request due to being overloaded or down for maintenance, typically with a 503 Service Unavailable status.

    public static readonly string ServerTimeOut = "The server took too long to respond.";
    // Use when the server acting as a gateway or proxy did not receive a timely response from the upstream server, typically with a 504 Gateway Timeout status.
}
public enum StatusCode
{
    Success = 200,
    Created = 201,
    NoContent = 204,
    PermanentRedirect = 301,
    TemporaryRedirect = 302,
    NotModified = 304,
    BadRequest = 400,
    UnAuthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    MethodNotAllowed = 405,
    TimeOut = 408,
    RequestClash = 409,
    ServerError = 500,
    BadGateway = 502,
    ServerBusy = 503,
    ServerTimeOut = 504
}