using InteractiveWhiteboard_back.Extensions;
using InteractiveWhiteboard_back.Extensions.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.OpenApi.Models;

JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(openApiOptions =>
{
    openApiOptions.AddDocumentTransformer((document, context, ct) =>
    {

        var requirements = new Dictionary<string, OpenApiSecurityScheme>
        {
            [JwtBearerDefaults.AuthenticationScheme] = new()
            {
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme.ToLower(), // "bearer" in minuscolo è fondamentale
                In = ParameterLocation.Header,
            }
        };
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = requirements;

        // Applica la sicurezza globalmente a tutti gli endpoint
        document.SecurityRequirements.Add(new OpenApiSecurityRequirement
        {
            [
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                }
            ] = []
        });
        
        return Task.CompletedTask;
    });
});

builder.Services.AddSignalR();

builder.Services.AddJwtAuthentication(configuration);
builder.Services.AddAuthorization();
builder.Services.ConfigureCors(configuration);

builder.Services.AddControllers();
var app = builder.Build();

//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("allowWhiteBoard");

app.UseAuthentication();
app.UseAuthorization();


app.MapOpenApi();
app.UseSwaggerUI(swaggerOptions =>
{
    swaggerOptions.SwaggerEndpoint("/openapi/v1.json", "Documentation V1");
});


app.MapSignalrHubs();
app.MapControllers();

app.Run();