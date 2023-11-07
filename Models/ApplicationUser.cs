using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using SignalR101.Models;
using SignalR101.Models.Dto;

namespace SignalR101.Repository.Concrete.EfCore
{
	public class ApplicationUser:IdentityUser
	{
       

        /*  public ApplicationUser()
          {
              Messages = new HashSet<MessageDto>();
          }

         public virtual ICollection<MessageDto> Messages { get; set; }*/

    }

   
}

