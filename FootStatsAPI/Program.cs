using FootStatsAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// ====================
//   Config DbContext
// ====================

builder.Services.AddDbContext<FootDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// ===================
//  Configuração JWT
// ===================

//Lê a sessão do jwt do app settings
var jwtSettings = builder.Configuration.GetSection("Jwt");

//Obtem a chave secreta e converte para bytes
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

//Registra o servico de autenticação
builder.Services.AddAuthentication(options =>
{
    //Define que o padrão de autenticação sera JwtBeare
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{

    options.TokenValidationParameters = new TokenValidationParameters
    {
        //valida se o token foi assinado da forma correta
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        //valida quem emitiu o token
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],

        //valida quem vai receber o token
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],

        //valida se o token expirou
        ValidateLifetime = true,

        //Remove tolerancia de tempo extra
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    //Define o esquema de segurança JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Informe o token jwt dessa forma: Bearer seu_token"
    });

    //Exige o token para endpoints protegidos
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
