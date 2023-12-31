using DatabaseConnection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using JwTNameService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using USerModelNamespace;
using policyConfigurations_pnamespace;
using USerServices_namespace;
using Registration_Namespace;
using Azure.Messaging.ServiceBus;
using MessageServiceNamespace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DbConn>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//==============================REGISTERING SERVICES=============================================
builder.Services.AddScoped<Jwt>();
builder.Services.AddScoped<USUSerServiceser>();
builder.Services.AddScoped<MessageService>();

//=========================AUTHORIZATION=========================================================
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
			ValidAudience = builder.Configuration["JwtOptions:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Secret_Key"]))
		};
	});

builder.Services.AddAuthorization(options =>
{
	PolicyConfiguration.ConfigurePolicies(options);
});

//=============================ADD IDENTITY SERVICES==========================================
builder.Services.AddIdentity<UserModel, IdentityRole>()
	.AddEntityFrameworkStores<DbConn>()
	.AddDefaultTokenProviders();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Management API", Version = "v1" });

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
