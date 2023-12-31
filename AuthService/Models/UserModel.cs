using Microsoft.AspNetCore.Identity;

namespace USerModelNamespace
{
	public class UserModel: IdentityUser
	{
		public String Firstname {  get; set; }
	}
}