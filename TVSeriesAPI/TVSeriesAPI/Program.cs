using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using TVSeriesAPI.Authentication;
using TVSeriesAPI.Controllers.ErrorHandler;
using TVSeriesAPI.DAL;
using TVSeriesAPI.DAL.Repositories;
using TVSeriesAPI.DAL.Repositories.Interfaces;
using TVSeriesAPI.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

#region JWT Auth

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddSingleton<IJwtAuth>(new JwtAuth(builder.Configuration["Jwt:Key"]));

#endregion

builder.Services.AddSingleton<IJwtAuth>(new JwtAuth(builder.Configuration["Jwt:Key"]));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TVSeriesAPI",
        Description = "An ASP.NET Core Web API for managing a database of series",
        Version = "v1"
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddDbContext<TVSeriesDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:TVSeriesDbConnection"]));

builder.Services.AddScoped<IRepositoryJoin<CastMember>, CastMemberRepository>();
builder.Services.AddScoped<IRepositoryJoin<EpisodeCast>, EpisodeCastRepository>();
builder.Services.AddScoped<IRepositoryJoin<Genre>, GenreRepository>();
builder.Services.AddScoped<IRepositoryJoin<Serie>, SerieRepository>();
builder.Services.AddScoped<IRepositoryJoin<Season>, SeasonRepository>();
builder.Services.AddScoped<IRepositoryJoin<Episode>, EpisodeRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMvc(options =>
    {
        options.Filters.Add(new ErrorHandlingFilter());
    });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TVSeriesAPI v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
