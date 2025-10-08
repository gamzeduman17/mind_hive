using MindHive.Application.ApiServices;
using MindHive.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Add Infrastructure services (DbContext, Repositories)
builder.Services.AddInfrastructure(builder.Configuration);

// Add Application services
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<JwtService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

// Map attribute-routed controllers (e.g., api/Auth/login)
app.MapControllers();

app.Run();
