using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SignalR101.Models;
using SignalR101.Models.Dto;
using SignalR101.Repository.Concrete.EfCore;

namespace SignalR101.Repository
{
	public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
	{
        public DbSet<MessageDto> Message { get; set; }
        public DbSet<MessageNotification> MessageNotification { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
		{
		}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           /* modelBuilder.Entity<MessageDto>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.SenderId);*/

           
        }

    }
}

