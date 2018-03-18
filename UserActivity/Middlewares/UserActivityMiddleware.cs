using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using UserActivity.Configs;
using UserActivity.Domain.Entities;

namespace UserActivity.Middlewares
{
    public class UserActivityMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RabbitMQConfigs _rabbitMQConfigs;

        public UserActivityMiddleware(RequestDelegate next, RabbitMQConfigs rabbitMQConfigs)
        {
            _next = next;
            _rabbitMQConfigs = rabbitMQConfigs;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var userSessionId = String.Empty;
            if(httpContext.Request.Cookies.ContainsKey("UserSessionId") == false) {
                
                userSessionId = httpContext.Session.Id;
                httpContext.Response.Cookies.Append("UserSessionId", userSessionId);
            }else{
                userSessionId = httpContext.Request.Cookies["UserSessionId"];
            }
                

            var activity = new Activity {
                SessionId = userSessionId,
                Path = httpContext.Request.Path,
                IP = httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString(),
                CreatedAt = DateTime.Now
            };

            var message = JsonConvert.SerializeObject(activity);

            var publisher = new Publisher(_rabbitMQConfigs);
            publisher.send(message);

            await _next.Invoke(httpContext);
        }
    }
}