using LogInDTONameSpace;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Registration_Namespace;
using System;
using USerServices_namespace;

namespace USerController_Namespace
{
	[ApiController]
	[Route("api/v1/authentication/")]
	public class USerController
	{
		private readonly USUSerServiceser _uSUSerServiceser;
		public USerController(USUSerServiceser uSUSerServiceser)
		{
			_uSUSerServiceser = uSUSerServiceser;

		}

		[HttpPost("login/")]
		public async Task<ActionResult> Login([FromBody] LoginDTO logins)
		{
			var token=await _uSUSerServiceser.Login(logins);
			return new OkObjectResult(token);

		}

		[HttpPost("register/")]
		public async Task<ActionResult> RegisterUser([FromBody] RegisterDTO registerDTO)
		{
			bool result = await _uSUSerServiceser.RegisterUser(registerDTO);

			if (result)
			{
				return new OkObjectResult(new { Message = "Registration successful" });
			}

			return new BadRequestObjectResult(new { Message = "Registration failed" });
		}

	}

}
