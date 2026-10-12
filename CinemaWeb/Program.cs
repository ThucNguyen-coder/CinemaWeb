using CinemaWeb;
using CinemaWeb.Database;
using CinemaWeb.IServices;
using CinemaWeb.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cấu hình CSDL SQL Server
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
builder.Services.AddDbContext<CinemaDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Đăng ký Dependency Injection cho các Services
builder.Services.AddScoped<IPhimService, PhimService>();
builder.Services.AddScoped<IRapService, RapService>();
builder.Services.AddScoped<ISuatChieuService, SuatChieuService>();
builder.Services.AddScoped<IDatVeService, DatVeService>();

// Đăng ký Global Exception Handler và ProblemDetails
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Cấu hình CORS để cho phép kết nối từ mọi giao diện Frontend
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Sử dụng Global Exception Handler
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
