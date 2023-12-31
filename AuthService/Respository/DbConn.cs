using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using USerModelNamespace;

namespace DatabaseConnection
{
	public class DbConn : IdentityDbContext<UserModel>
	{
		public DbConn(DbContextOptions<DbConn> options): base(options)
		{
			
		}
		public DbSet<UserModel> users { get; set; }
	}
}
