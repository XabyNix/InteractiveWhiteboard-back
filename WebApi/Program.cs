using InteractiveWhiteboard_back.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSignalR();
builder.Services.ConfigureCors();

var app = builder.Build();

app.UseRouting();
app.UseCors("allowWhiteBoard");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.UseHttpsRedirection();

app.MapSignalrHubs();
app.Run();