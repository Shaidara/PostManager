using PostManager.Bussiness.Helpers;
using PostManager.Bussiness.Services;
using PostManager.Core.Helpers;
using PostManager.Core.Interfaces;
using PostManager.Core.Interfaces.Services;

var builder = WebApplication.CreateBuilder(args);
string AllowLocalhostCors = "AllowLocalhost";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(cfg => 
    cfg.AddPolicy(AllowLocalhostCors, builder =>
            {
                builder.AllowAnyOrigin();
            })
);

var endpoint = builder.Configuration.GetSection("PostBaseEndpoint").Value;

builder.Services.AddHttpClient("DataRepositoryAccess", c =>
  {
      c.BaseAddress = new Uri(endpoint);
      c.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
  }
);

RegisterServices();

void RegisterServices()
{
    builder.Services.AddScoped<IDataRepositoryAccess, DataRepositoryAccess>();
    builder.Services.AddScoped<IPostService, PostService>();
    builder.Services.AddScoped<IPostServiceHelper, PostServiceHelper>();
};

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(AllowLocalhostCors);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
