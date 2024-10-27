using DPS.Api;
using DPS.Data;
using DPS.Data.Entities;
using DPS.Data.Interceptors;
using DPS.Service.Listings;
using DPS.Service.User;
using DPS.Service.User.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
	options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
	options.UseNpgsql(connectionString);
});

var appSettings = builder.Configuration.GetSection("TokenSettings").Get<TokenSettings>() ?? default!;
builder.Services.AddSingleton(appSettings);

builder.Services.AddIdentityCore<ApplicationUser>()
			   .AddRoles<IdentityRole>()
			   .AddSignInManager()
			   .AddEntityFrameworkStores<ApplicationDbContext>()
			   .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("REFRESHTOKENPROVIDER");

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
	options.TokenLifespan = TimeSpan.FromSeconds(appSettings.RefreshTokenExpireSeconds);
});

builder.Services.AddAuthorization();


builder.Services.AddScoped<ApplicationDbContextInitialiser>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ListingService>();
builder.Services.AddScoped<IUser, CurrentUser>();

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
	.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ISaveChangesInterceptor, BaseEntityInterceptor>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
	options.AddPolicy("webAppRequests", builder =>
	{
		builder.AllowAnyHeader()
		.AllowAnyMethod()
		.WithOrigins(appSettings.Audience)
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
		new OpenApiSecurityRequirement{
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
});

var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Test"))
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
app.MapIdentityApi<ApplicationUser>();

app.MapControllers();
app.Run();