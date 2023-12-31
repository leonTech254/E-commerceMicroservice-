using Microsoft.EntityFrameworkCore;
using RewardModel_namespace;

namespace DBconnection_namespace
{
	public class DBconn:DbContext
	{
		public DBconn(DbContextOptions<DBconn> options) : base(options)
		{

		}

		public DbSet<RewardModel> rewards { get; set; }

	}
}