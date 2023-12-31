using Azure;
using Azure.Messaging.ServiceBus;
using DatabaseConnection;
using JwTNameService;
using LogInDTONameSpace;
using MessageServiceNamespace;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Registration_Namespace;
using System.Threading.Tasks;
using USerModelNamespace;

namespace USerServices_namespace
{
	public class USUSerServiceser
	{
		private readonly IConfiguration _configuration;
		private readonly UserManager<UserModel> _userManager;
		private readonly DbConn _dbConn;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly MessageService _messageService;
		private readonly Jwt _jwt;

		public USUSerServiceser(IConfiguration configuration, UserManager<UserModel> userManager ,DbConn dbConn,RoleManager<IdentityRole> roleManager, MessageService messageService,Jwt jwt)
		{
			_configuration = configuration;
			_userManager = userManager;
			_dbConn=dbConn;
			_roleManager= roleManager;
			_messageService = messageService;
			_jwt = jwt;
		}
		public async Task<ActionResult> Login(LoginDTO logins)
		{
			var user = _dbConn.users.FirstOrDefault(e => e.UserName == logins.username);
			if (user == null) return null; // Return null or handle as appropriate

			var isValid = await _userManager.CheckPasswordAsync(user, logins.password);

			if (!isValid) return null; 

			//=================GENERATING THE TOKEN FOFR THE USER==========================
			var token = _jwt.GenerateToken(user);

			Console.WriteLine("Login Successfully");
			return new OkObjectResult(new { msg = "Login successfully", usertoker = token });
		}
		public async Task<bool> RegisterUser(RegisterDTO registerDTO)
		{
			UserModel userModel = new UserModel()
			{
				Email = registerDTO.Email,
				Firstname = registerDTO.Firstname,
				UserName = registerDTO.username
				
			};

			var result = await _userManager.CreateAsync(userModel, registerDTO.Password);

			if (result.Succeeded)
			{

				//=============================ADD USE TO THE QUEUE===========================================
				Console.WriteLine("Addeding user to the Queue");
				await _messageService.AddUSerToQueue(registerDTO);
				Console.WriteLine("Added user to the Queue");
				return true;
			}
			else
			{
				foreach (var error in result.Errors)
				{
					// Log the error or handle it as needed
					Console.WriteLine($"Error: {error.Code}, Description: {error.Description}");
				}
				// Handle errors, log them, etc.
				// result.Errors contains information about the failure
				return false;
			}
		}
	}
}
