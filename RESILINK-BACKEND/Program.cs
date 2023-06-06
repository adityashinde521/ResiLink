using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Data;
using DataAccessLayer.Repository;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using HSPA_BACKEND.Mappings;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using System.Text;  
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RESILINK_BACKEND.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
 option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
 {
     Name = "Authorization",
     Type = SecuritySchemeType.ApiKey,
     Scheme = "Bearer",
     BearerFormat = "JWT",
     In = ParameterLocation.Header,
     Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
 }));
builder.Services.AddSwaggerGen(option =>
 option.AddSecurityRequirement(new OpenApiSecurityRequirement
 {
     {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
      }
 }
 ));


//var jwtSettings = Configuration.GetSection("JwtSettings");
//key = Encoding.ASCII.GetBytes(jwtSettings["Secret"]);

builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringDb")));


//authentication
builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
 .AddDefaultTokenProviders()
 .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var tokenKey = Encoding.ASCII.GetBytes("1234567890!@#$%^&*()ASDFGHJKLqwertyuiop");

    o.TokenValidationParameters = new TokenValidationParameters
    {
        //ValidIssuer = builder.Configuration["Jwt:Issuer"],
        //ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});



builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringDb")));

builder.Services.AddScoped<ICityBll, CityRepository>();
builder.Services.AddScoped<IFurnishingTypeRepository, FurnishingTypeRepository>();
builder.Services.AddScoped<IPropertyTypeRepository, PropertyTypeRepository>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();

IServiceCollection serviceCollection = builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWTAuthDemo v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
