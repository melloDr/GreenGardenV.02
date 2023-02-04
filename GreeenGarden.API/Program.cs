using AutoMapper;
//using GreeenGarden.Business.Interface;
using GreeenGarden.Business.Service;
using GreeenGarden.Business.Mapper;
//using GreeenGarden.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Azure.Identity;
using GreeenGarden.Business.Service.SecretServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen();

/*// Database
builder.Services.AddDbContext<GreenGardenDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));


// Add services to interface
//builder.Services.AddScoped<ICustomerExample, CustomerExample>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
*/
//Swagger


// builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
// var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));

string test = KeyVaultServices.GetConnectionString();
Console.WriteLine("test:" + test);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GreeenGarden.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    /*var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);*/
});


//Authentication JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

//Mapper
builder.Services.AddSingleton(new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
}).CreateMapper());

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}



app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
