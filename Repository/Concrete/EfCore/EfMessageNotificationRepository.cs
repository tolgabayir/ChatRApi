using System;
using SignalR101.Models;
using SignalR101.Models.Dto;
using SignalR101.Repository.Abstract;

namespace SignalR101.Repository.Concrete.EfCore
{
	public class EfMessageNotificationRepository : EfGenericRepository<MessageNotification>, IMessageNotificationRepository
    {

        public EfMessageNotificationRepository(ApplicationDbContext context) : base(context)
        {


        }
	
		
	}
}

