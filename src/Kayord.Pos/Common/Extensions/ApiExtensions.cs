using FastEndpoints.Swagger;
using Kayord.Pos.Common.Extensions.Swagger;
using Scalar.AspNetCore;

namespace Kayord.Pos.Common.Extensions;

public static class ApiExtensions
{
    public static void ConfigureApi(this IServiceCollection services)
    {
        services.AddFastEndpoints()
        .SwaggerDocument(o =>
        {
            o.DocumentSettings = s =>
            {
                s.Title = AppDomain.CurrentDomain.FriendlyName;
                s.Version = "v1";
                s.MarkNonNullablePropsAsRequired();
                s.OperationProcessors.Add(new CustomOperationsProcessor());
                s.SchemaSettings.SchemaNameGenerator = new CustomSchemaNameGenerator(false);
            };
        });
    }

    public static IApplicationBuilder UseApi(this WebApplication app)
    {
        app.UseDefaultExceptionHandler()
            .UseFastEndpoints(c =>
            {
                c.Serializer.Options.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                c.Endpoints.Configurator = ep =>
                {
                    ep.Options(x => x.Produces<InternalErrorResponse>(500));
                };

                // Exclude test and sensitive endpoints in production
                if (!app.Environment.IsDevelopment())
                {
                    c.Endpoints.Filter = ep =>
                    {
                        if (ep.Routes.First().StartsWith("/test"))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    };
                }
            });

        // Only Have docs available in development
        if (app.Environment.IsDevelopment())
        {
            app.UseOpenApi(c => c.Path = "/openapi/{documentName}.json");
            app.MapScalarApiReference(string.Empty, options =>
            {
                options.TagSorter = TagSorter.Alpha;
            });
        }
        return app;
    }
}