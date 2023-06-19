using API.Extensions;
using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddSingleton(builder.Environment);
services.AddApplicationServices();
services.AddInfrastructureServices(configuration);
services.AddSignalR();

services.AddCors();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddHealthChecks();
services.AddSwaggerGenWithAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();