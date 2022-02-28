using System.Net;
using Microsoft.AspNetCore.Builder;

namespace redux.ExtendMethods
{
    public static class AppExtends
    {
        public static void AddStatusCodePage(this IApplicationBuilder app)
        {
            app.UseStatusCodePages(appError =>
            {
                appError.Run(async context =>
                {
                    var reponse = context.Response;
                    var code = reponse.StatusCode;

                    var content = @$"
        <head>
        <meta charset='UTF-8'>
        <body>
        <p>Co loi xat ra ${code}- {(HttpStatusCode)code}</p>
        </body>
        </head>

        ";
                    await reponse.WriteAsync(content);
                });
            }); // code tu loi 400
        }
    }
}