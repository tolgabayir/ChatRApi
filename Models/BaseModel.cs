using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR101.Models
{
	public abstract class BaseModel
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
		public DateTime createTime { get; set; }
		public DateTime updateTime { get; set; }

	}
}

