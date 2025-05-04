using Test.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSingleton<IService,Service>();
builder.Services.AddControllers();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapGet("/", () => "Test");
app.MapControllers();
app.Run();

