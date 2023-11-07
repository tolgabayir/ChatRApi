using Microsoft.EntityFrameworkCore;
using SignalR101.Models.Dto;
using SignalR101.Repository.Abstract;

namespace SignalR101.Repository.Concrete.EfCore
{
	public class EfMessageRepository:EfGenericRepository<MessageDto>,IMessageRepository
	{
       
        public EfMessageRepository(ApplicationDbContext context):base(context)
		{
          

        }
 
    }
}

