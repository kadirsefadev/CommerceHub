using CommerceHub.Application.Exceptions;
using System.Text.Json;

namespace CommerceHub.Api.Middleware
{
    public class GlobalExceptionMiddlaware
    {


        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddlaware> _logger;

        public GlobalExceptionMiddlaware(RequestDelegate next, ILogger<GlobalExceptionMiddlaware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İşlenmeyen hata:{Message}",ex.Message);
                await HandleExceptionAsync(context, ex);

               
            }
        
        }


        private static async Task HandleExceptionAsync(HttpContext context,Exception exception)
        {

            context.Response.ContentType = "application/json";
            var (statusCode, message, errors) = exception switch
            {

                Application.Exceptions.ValidationException ve => (422,ve.Message, ve.ValidationErrors),
                NotFoundException ne =>     (404, ne.Message, new List<string>()),
                UnauthorizedException ue => (403, ue.Message, new List<string>()),
                AppException ae =>          (ae.StatusCode,ae.Message, new List<string>()),
                _ =>                        (500,  "Beklenmeyen Hata", new List<string>())

            };
            context.Response.StatusCode = statusCode;
            var response = new
            {
                success = false,
                message,
                errors
            };
            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await context.Response.WriteAsync(json);


        }


        /*
         Middlaware: Http istegi geldiginde ve response donmeden önce araya giren bir katmandır .

        İstek => Middleware 1 => Middleware 2 => Middleware 3 => Controller(Response Donecek)

        İstek=> GlobalException => Controller(Response Donecek)

         
        Bir güvenlik görevlisi gibi ziyaretci binaya girmeden önce görevli kontrol eder cıkarkende kontrol eder. 
         
         */

    }
}
