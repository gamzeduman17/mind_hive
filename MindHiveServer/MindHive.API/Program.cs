using MindHive.Application.ApiServices;
using MindHive.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
//configuration
ConfigurationManager configuration = builder.Configuration;
//add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// infrastructure
builder.Services.AddInfrastructure(configuration);
//layer services
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<JwtService>();
//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});
//build app
var app = builder.Build();
// middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();