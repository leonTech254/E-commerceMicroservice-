using Microsoft.EntityFrameworkCore;
using OrderModel_namespace;

namespace DBconnection_namespace
{
	public class DBconn:DbContext
	{
		public DBconn(DbContextOptions<DBconn> options) : base(options)
		{

		}

		public DbSet<OrderModel> orders { get; set; }

	}
}