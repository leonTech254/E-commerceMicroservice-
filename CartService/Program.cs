using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

using Azure.Messaging.ServiceBus;
using DBconnection_namespace;
using JwTNameService;
using policyConfigurations_pnamespace;
using Cartservice_namespace;
using ExternalApiData_namespace;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DBconn>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//==============================REGISTERING SERVICES=============================================
builder.Services.AddScoped<Jwt>();
builder.Services.AddScoped<MycartService>();
builder.Services.AddScoped<ExternalAPI>();
/*builder.Services.AddScoped<ProductService>();*/
//=========================AUTHORIZATION=========================================================
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		var issuer = builder.Configuration["JwtOptions:Issuer"];
		var audience = builder.Configuration["JwtOptions:Audience"];
		var secretKey = builder.Configuration["JwtOptions:secrete_Key"];

		if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(secretKey))
		{
			// Log or handle the case where configuration values are missing or null
			throw new ApplicationException("JWT configuration values are missing or null.");
		}

		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = issuer,
			ValidAudience = audience,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
		};
	});

builder.Services.AddAuthorization(options =>
{
	PolicyConfiguration.ConfigurePolicies(options);
});



builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cart Service", Version = "v1" });

	// Add JWT Authentication support in Swagger UI
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "JWT Authorization header using the Bearer scheme",
		Type = SecuritySchemeType.Http,
		Scheme = "bearer"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
			new string[] { }
		}
	});
});

// Add the following line to register controller services
builder.Services.AddControllers();

var app = builder.Build();

// ===============================ALLOW CORS=======================================
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
    options.AllowAnyMethod();
});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API");
	});
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Add this line to enable authentication
app.UseAuthorization(); // Add this line to enable authorization

app.MapControllers();

app.Run();