using DotnetSdk.Api;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices(builder.Configuration);

var app = builder.Build();

app.UseServices();

app.Run();