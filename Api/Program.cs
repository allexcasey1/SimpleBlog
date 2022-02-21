using Persistence;
using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.WebHost.ConfigureServices(options => options.AddApplicationServices());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("CorsPolicy");
}


// app.UseHttpsRedirection();

app.MapControllers();

app.Run();
