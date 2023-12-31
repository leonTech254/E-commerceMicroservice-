using Microsoft.EntityFrameworkCore;
using ProductModel_namespace;

namespace DBconnection_namespace
{
	public class DBconn:DbContext
	{
		public DBconn(DbContextOptions<DBconn> options) : base(options)
		{

		}

		public DbSet<ProductModel> products { get; set; }
	}
}