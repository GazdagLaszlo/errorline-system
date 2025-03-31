using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ErrorlineSystem.DataContext.Context;
using ErrorlineSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Filename=errorlinedb.db"));

builder.Services.AddScoped<IFacilityService, FacilityService>();
builder.Services.AddScoped<IEquipmentService, EquipmentService>();
builder.Services.AddScoped<IEquipmentOrderService, EquipmentOrderService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IIssueTypeService, IssueTypeService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
 
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:5091",
            ValidAudience = "https://localhost:5091",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("randomSztring12345_x2____randomSztring12345_x2")),
        };
    });



// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ErrorlineSystem API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ErrorlineSystem API v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
