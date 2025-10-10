using MindHive.Application.ApiServiceInterfaces;
using MindHive.Application.ApiServices;
using MindHive.Infrastructure.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// 🧩 Configuration
ConfigurationManager configuration = builder.Configuration;

// 🧱 Add Controllers, Swagger, and Endpoints
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🏗️ Add Infrastructure Layer (DB, Repositories, etc.)
builder.Services.AddInfrastructure(configuration);

// 💡 Add Application Services
builder.Services.AddApplicationServices();

// 🌍 Enable CORS (Allow all for development)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()      // iOS / Android / Web erişimi için açık bırak
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// 🔑 Authentication Middleware (JWT için)
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:Secret"] ?? "your-super-secret-key-that-is-at-least-32-characters-long")
            ),
            ValidateLifetime = true
        };
    });

// 🧩 Build the App
var app = builder.Build();

// 🧠 Allow iOS Simulator / Network Devices to Access API
app.Urls.Add("http://0.0.0.0:5014"); // <--- 🔥 Çok önemli: dış ağdan erişim sağlar

// 🚀 Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ⚙️ HTTPS yönlendirmeyi etkin bırakabilirsin ama mobilde sorun olursa kapatılabilir
app.UseHttpsRedirection();

// 🌍 CORS (her zaman Authentication'dan önce!)
app.UseCors("AllowAll");

// 🔑 JWT Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// 🎯 Map Controllers
app.MapControllers();

// ▶️ Run the App
app.Run();
