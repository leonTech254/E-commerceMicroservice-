namespace policyConfigurations_pnamespace
{
	using Microsoft.AspNetCore.Authorization;

	public static class PolicyConfiguration
	{
		public static void ConfigurePolicies(AuthorizationOptions options)
		{
			options.AddPolicy("RequireLoggedIn", policy => policy.RequireAuthenticatedUser());

			options.AddPolicy("RequireAdminRole", policy =>
			{
				policy.RequireAuthenticatedUser();
				policy.RequireRole("ADMIN");
			});

			options.AddPolicy("RequireUserRole", policy =>
			{
				policy.RequireAuthenticatedUser();
				policy.RequireRole("USER");
			});
		}
	}

}