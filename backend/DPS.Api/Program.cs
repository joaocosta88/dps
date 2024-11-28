using DPS.Api;
using DPS.Data;
using DPS.Data.Entities;
using DPS.Data.Interceptors;
using DPS.Service.Listings;
using DPS.Service.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using DPS.Email;
using DPS.Email.Helpers;
using DPS.Service.Auth;
using DPS.Service.Common;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
    options.UseNpgsql(connectionString);
   // options.UseSnakeCaseNamingConvention();
});

var tokenSettings = builder.Configuration.GetSection("TokenSettings").Get<TokenSettings>() ?? default!;
builder.Services.AddSingleton(tokenSettings);

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromSeconds(tokenSettings.RefreshTokenExpireSeconds);
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = tokenSettings.ValidateIssuer,
        ValidateAudience = tokenSettings.ValidateAudience,
        ValidateLifetime = tokenSettings.ValidateLifetime,
        ValidateIssuerSigningKey = tokenSettings.ValidateIssuerSigningKey,
        RequireExpirationTime = tokenSettings.RequireExpirationTime,
        ValidIssuer = tokenSettings.Issuer,
        ValidAudience = tokenSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecretKey)),
        ClockSkew = TimeSpan.FromSeconds(0)
    };
});

var cookieSettings = builder.Configuration.GetSection("CookieSettings").Get<CookieSettings>() ?? default!;
builder.Services.AddSingleton(cookieSettings);

var emailConfig = builder.Configuration.GetSection("EmailConfig").Get<EmailConfig>() ?? default!;
builder.Services.AddSingleton(emailConfig);

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddSignInManager()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddScoped<ApplicationDbContextInitialiser>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ListingService>();
builder.Services.AddScoped<IUser, CurrentUser>();
builder.Services.AddScoped<HtmlRenderer>();
builder.Services.AddSingleton<EmailSender>();
builder.Services.AddSingleton<EmailBodyFactory>(m
    => new EmailBodyFactory(m.GetService<HtmlRenderer>() ?? throw new InvalidOperationException()));
builder.Services.AddSingleton(m =>
    new UrlFactory(builder.Configuration.GetValue<string>("BaseUrl")!));
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ISaveChangesInterceptor, BaseEntityInterceptor>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("webAppRequests", policyBuilder =>
    {
        var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

        policyBuilder.AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins(allowedOrigins)
            .AllowCredentials();
    });
});

builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo() { Title = "App Api", Version = "v1" });
    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    config.AddSecurityRequirement(
        new OpenApiSecurityRequirement
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var scope = app.Services.CreateScope();
    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
    await initialiser.InitialiseAsync();
    await initialiser.SeedAsync();
}

app.UseHttpsRedirection();
app.UseCors("webAppRequests");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();