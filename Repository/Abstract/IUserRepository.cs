using System;
using SignalR101.Models;
using SignalR101.Repository.Concrete.EfCore;

namespace SignalR101.Repository.Abstract
{
	public interface IUserRepository:IGenericRepository<ApplicationUser>
	{
		
	}
}

