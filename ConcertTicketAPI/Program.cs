using System.Text.Json.Serialization;
using ConcertTicketAPI;
using ConcertTicketAPI.Converter;
using ConcertTicketAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidIssuer = TestDevAuthOptions.Issuer,
            ValidateAudience = false,
            ValidAudience = TestDevAuthOptions.Audience,
            ValidateLifetime = false,
            RequireSignedTokens = false,
            IssuerSigningKey = TestDevAuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = false,
            ValidateActor = false,
        };
    });


builder.Services
    .AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(connectionString));

builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        // var enumConverter = new JsonStringEnumConverter();
        // o.JsonSerializerOptions.Converters.Add(enumConverter);

        o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        o.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        o.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors(cors =>
{
    cors.AllowAnyHeader();
    cors.WithMethods(HttpMethod.Put.Method, HttpMethod.Delete.Method, HttpMethod.Patch.Method);
    cors.WithOrigins("http://localhost:3000", "http://localhost:4200");
});

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