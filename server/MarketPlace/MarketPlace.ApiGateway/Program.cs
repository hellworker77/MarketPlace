using Application.Extension;
using Infrastructure.Extensions;
using Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration, "api");
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.DbInitialize();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();