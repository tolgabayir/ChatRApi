using System;
using SignalR101.Models;
using SignalR101.Repository.Abstract;

namespace SignalR101.Repository.Concrete.EfCore
{
	public class EfUserRepository:EfGenericRepository<ApplicationUser>,IUserRepository
	{
		public EfUserRepository(ApplicationDbContext context):base(context)
		{
		}
	}
}

