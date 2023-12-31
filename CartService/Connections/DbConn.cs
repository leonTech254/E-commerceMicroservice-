using CartModel_namespace;
using Microsoft.EntityFrameworkCore;

namespace DBconnection_namespace
{
	public class DBconn:DbContext
	{
		public DBconn(DbContextOptions<DBconn> options) : base(options)
		{

		}

		public DbSet<CartModel> cart { get; set; }

	}
}