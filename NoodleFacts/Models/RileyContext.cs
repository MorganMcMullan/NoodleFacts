using Microsoft.EntityFrameworkCore;

namespace NoodleFacts.Models
{
	public class RileyContext : DbContext
	{
		public DbSet<Fact> Facts { get; set; }

		public RileyContext() : base()
		{

		}
		public RileyContext(DbContextOptions<RileyContext> options) : base(options)
		{ }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}
	}
}
