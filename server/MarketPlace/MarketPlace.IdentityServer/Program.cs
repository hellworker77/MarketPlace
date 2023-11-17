using MarketPlace.IdentityServer.Common;
using MarketPlace.IdentityServer.Extensions;
using Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityCors();
builder.Services.AddPersistenceLayerForIdentityServer(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureIdentityServerContexts(builder.Configuration);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.InitializeDatabase();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCorsWithPolicy();

app.UseHttpsRedirection();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

app.MapControllers();

app.Run();