using JobFinder.Helper;
using JobFinder.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using PatternRepository.Application.Helper;
using PatternRepository.Application.IdentityModels;
using PatternRepository.Application.Interface.Repository;
using PatternRepository.Application.Interface.Service;
using PatternRepository.Application.Shared;
using PatternRepositroy.Infrastructure.Data;
using PatternRepositroy.Infrastructure.Repository;
using PatternRepositroy.Infrastructure.Service;
using PatternRepositroy.Infrastructure.Shared;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));


builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient(typeof(IRepositoryUser<>), typeof(RepositoryUser<>));
builder.Services.AddTransient(typeof(IUserService<>), typeof(UserService<>));
builder.Services.AddTransient<IMovieService, MovieService>();
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ICurrentUser, CurrentUser>();
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.Configure<EmailSetting>(builder.Configuration.GetSection("EmailSetting"));


builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
 .AddJwtBearer(o =>
 {
     o.RequireHttpsMetadata = false;
     o.SaveToken = false;
     o.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuerSigningKey = true,
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidIssuer = builder.Configuration["JWT:Issuer"],
         ValidAudience = builder.Configuration["JWT:Audience"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),

     };
 });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
